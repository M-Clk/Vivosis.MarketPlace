using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Service.Abstract;
using N11CategoryService;
using Microsoft.EntityFrameworkCore;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Data;
using System.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace Vivosis.MarketPlace.Service.Concrete
{
    public class N11Service :IN11Service
    {
        Authentication _auth;
        AccountDbContext _accountDbContext;
        StoreUser _store;
        public N11Service(IStoreService storeService, AccountDbContext accountDbContext)
        {
            //TODO burasi hatali
            _store = storeService.GetBoughtStores().FirstOrDefault(su => su.Store.name.ToLower().Equals("n11"))/* ?? throw new InvalidOperationException("N11 sisteminizde kayitli degil.")*/;
            _auth = new Authentication
            {
                appKey = _store?.api_key,
                appSecret = _store?.secret_key
            };
            _accountDbContext = accountDbContext;
        }

        public bool CheckApiConnection()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryFromStore> GetTopCategories()
        {
            var topCategories = _accountDbContext.CategoryFromStores.Where(c => c.ParentId == 0);
            if(topCategories.Any())
                return topCategories;

            CategoryServicePortClient proxy = new CategoryServicePortClient();
            var request = new GetTopLevelCategoriesRequest();
            request.auth = _auth;
            var req = new GetTopLevelCategoriesRequest1(request);
            req.GetTopLevelCategoriesRequest.auth = _auth;

            var categories = proxy.GetTopLevelCategoriesAsync(request).Result;

            var categoryList = new List<CategoryFromStore>();
            foreach(var category in categories.GetTopLevelCategoriesResponse.categoryList)
            {
                var newCategory = new CategoryFromStore
                {
                    Id = category.id,
                    Name = category.name,
                    ParentId = 0,
                    StoreId = _store.store_id
                };
                categoryList.Add(newCategory);
            }
            _accountDbContext.CategoryFromStores.AddRange(categoryList);
            _accountDbContext.SaveChanges();
            return categoryList;
        }
        public IEnumerable<CategoryFromStore> GetSubCategories(int categoryId)
        {
            var localSubCategories = _accountDbContext.CategoryFromStores.Where(c => c.ParentId == categoryId);
            if(localSubCategories.Any())
                return localSubCategories;
            var categoryList = new List<CategoryFromStore>();
            CategoryServicePortClient proxy = new CategoryServicePortClient();
            var request = new GetSubCategoriesRequest();
            request.auth = _auth;
            request.categoryId = categoryId;

            var subCategories = proxy.GetSubCategoriesAsync(request).Result;
            if(subCategories.GetSubCategoriesResponse.category?.FirstOrDefault().subCategoryList == null)
                return categoryList;
            foreach(var category in subCategories.GetSubCategoriesResponse.category.SelectMany(c => c.subCategoryList))
            {
                var newCategory = new CategoryFromStore
                {
                    Id = category.id,
                    Name = category.name,
                    ParentId = categoryId,
                    StoreId = _store.store_id
                };
                categoryList.Add(newCategory);
            }
            _accountDbContext.CategoryFromStores.AddRange(categoryList);
            _accountDbContext.SaveChanges();
            return categoryList;
        }
        public StoreCategory GetCategoryWithParentsName(long categoryId)
        {
            var category = new StoreCategory();
            CategoryServicePortClient proxy = new CategoryServicePortClient();
            var request = new GetParentCategoryRequest();
            request.auth = _auth;
            while(true)
            {
                request.categoryId = categoryId;
                var parentCategory = proxy.GetParentCategoryAsync(request).Result;
                if(parentCategory.GetParentCategoryResponse.category == null)
                    return null;
                if(string.IsNullOrEmpty(category.matched_category_code))
                {
                    category.matched_category_code = parentCategory.GetParentCategoryResponse.category.id.ToString();
                    category.matched_category_name = parentCategory.GetParentCategoryResponse.category.name;
                }
                if(parentCategory.GetParentCategoryResponse.category?.parentCategory?.name == null)
                    break;
                category.matched_category_name = $"{parentCategory.GetParentCategoryResponse.category.parentCategory.name} > {category.matched_category_name}";
                categoryId = parentCategory.GetParentCategoryResponse.category.id;
            }
            return category;
        }

        public IEnumerable<CategoryFromStoreAttribute> GetCategoryOptisons(long categoryId)
        {
            var categoryOptions = _accountDbContext.CategoryToAttributeFromStores.Where(c => c.CategoryId == categoryId).Include(c => c.Attribute);
            if(categoryOptions.Any())
                return categoryOptions.Select(c => c.Attribute);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.n11.com/ws/CategoryService.wsdl");
            httpWebRequest.ContentType = "text/xml";
            httpWebRequest.Method = "POST";

            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sch=\"http://www.n11.com/ws/schemas\">" +
            "<soapenv:Header/>" +
            "<soapenv:Body>" +
            "<sch:GetCategoryAttributesRequest>" +
            "<auth>" +
            "<appKey>" + _auth.appKey + "</appKey>" +
            "<appSecret>" + _auth.appSecret + "</appSecret>" +
            "</auth>" +
            "<categoryId>" + categoryId + "</categoryId>" +
            "</sch:GetCategoryAttributesRequest>" +
            "</soapenv:Body>" +
            "</soapenv:Envelope>");

            using(var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                soapEnvelopeXml.Save(streamWriter);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using(var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                XmlDocument responseXml = new XmlDocument();
                responseXml.LoadXml(streamReader.ReadToEnd());
                var attributeList = responseXml.GetElementsByTagName("attributeList");
                if(attributeList != null && attributeList.Count > 0)
                {
                    var categoryOptionsList = new List<CategoryFromStoreAttribute>();

                    foreach(XmlNode attribute in attributeList[0])
                    {
                        var categoryOption = new CategoryFromStoreAttribute();
                        categoryOption.IsRequired = bool.Parse(attribute["mandatory"].InnerText);
                        categoryOption.Name = attribute["name"].InnerText;
                        categoryOption.Id = long.Parse(attribute["id"].InnerText);
                        if(!_accountDbContext.CategoryFromStoreAttributes.Any(a => a.Id == categoryOption.Id))
                        {
                            categoryOption.AttributeValues = new List<CategoryFromStoreAttributeValue>();
                            categoryOption.CategoryToAttributeFromStores = new List<CategoryToAttributeFromStore>
                                { new CategoryToAttributeFromStore{ CategoryId = categoryId}};
                            foreach(XmlNode attributeValue in attribute["valueList"])
                            {
                                var newCategoryOptionValue = new CategoryFromStoreAttributeValue
                                {
                                    Id = long.Parse(attributeValue["id"].InnerText),
                                    Name = attributeValue["name"].InnerText
                                };
                                categoryOption.AttributeValues.Add(newCategoryOptionValue);
                            }
                            _accountDbContext.CategoryFromStoreAttributes.Add(categoryOption);
                        }
                        categoryOptionsList.Add(categoryOption);
                    }
                    _accountDbContext.SaveChanges();
                    return categoryOptionsList;
                }
                return null;
            }
        }
    }
}