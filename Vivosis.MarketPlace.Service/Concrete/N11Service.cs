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
using N11ProductService;

namespace Vivosis.MarketPlace.Service.Concrete
{
    public class N11Service :IN11Service
    {
        N11CategoryService.Authentication _authCategory;
        N11ProductService.Authentication _authProduct;
        AccountDbContext _accountDbContext;
        StoreUser _store;
        public N11Service(IStoreService storeService, AccountDbContext accountDbContext)
        {
            //TODO burasi hatali
            _store = storeService.GetBoughtStores().FirstOrDefault(su => su.Store.name.ToLower().Equals("n11"))/* ?? throw new InvalidOperationException("N11 sisteminizde kayitli degil.")*/;
            _authCategory = new N11CategoryService.Authentication
            {
                appKey = _store?.api_key,
                appSecret = _store?.secret_key
            };
            _authProduct = new N11ProductService.Authentication
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
            request.auth = _authCategory;
            var req = new GetTopLevelCategoriesRequest1(request);
            req.GetTopLevelCategoriesRequest.auth = _authCategory;

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
            request.auth = _authCategory;
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
        public CategoryFromStore GetCategoryWithParents(long categoryId)
        {
            var category = _accountDbContext.CategoryFromStores.FirstOrDefault(c => c.Id == categoryId);
            category = LoadParentCategories(category);
            return category;
        }

        public IEnumerable<CategoryFromStoreAttribute> GetCategoryOptions(long categoryId)
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
            "<appKey>" + _authCategory.appKey + "</appKey>" +
            "<appSecret>" + _authCategory.appSecret + "</appSecret>" +
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

        public IEnumerable<CategoryFromStoreAttributeValue> GetCategoryOptionValues(long categoryOptionId)
        {
            var categoryAttributeValues = _accountDbContext.CategoryFromStoreAttributeValues.Where(cav => cav.AttributeId == categoryOptionId);
            return categoryAttributeValues;
        }
        public bool SendProduct(Data.Entities.Product productFromDb)
        {
            var storeProduct = productFromDb.ProductStores.First();
            var proxy = new ProductServicePortClient();
            var saveProductRequest = new SaveProductRequest();
            saveProductRequest.auth = _authProduct;
            var newProductRequest = new ProductRequest();
            newProductRequest.productSellerCode = "MarketPlace" + productFromDb.product_id;
            newProductRequest.title = productFromDb.name;
            newProductRequest.description = string.IsNullOrEmpty(storeProduct.description) ? productFromDb.description : storeProduct.description;
            newProductRequest.subtitle = productFromDb.name;
            newProductRequest.category = new CategoryRequest
            {
                id = long.Parse(productFromDb.ProductCategories.FirstOrDefault().Category.CategoryStores.FirstOrDefault(cs => cs.store_id == storeProduct.store_id).matched_category_code)
            };
            newProductRequest.price = storeProduct.sale_price > 0 ? storeProduct.sale_price : productFromDb.price;
            newProductRequest.currencyType = storeProduct.currency;
            newProductRequest.images = new N11ProductService.ProductImage[]
            {
                new N11ProductService.ProductImage
                {
                    url = productFromDb.image_url, order = "1"
                }
            };
            newProductRequest.productCondition = "1";//TODO 1 = yeni, 2 = ikinci el anlaminda.
            newProductRequest.preparingDay = "3";
            newProductRequest.shipmentTemplate = "";//TODO sablon da temin edilecek sekilde guncellencek.
            var categoryOptions = productFromDb.ProductCategories.FirstOrDefault().Category.CategoryStores.FirstOrDefault(cs => cs.store_id == storeProduct.store_id).CategoryOptions;
            var attributeRequest = new ProductAttributeRequest[categoryOptions.Count];
            for(int i = 0; i < attributeRequest.Length; i++)
            {
                foreach(var optionValue in categoryOptions[i].CategoryOptionValues)
                    attributeRequest[i] = new ProductAttributeRequest { name = categoryOptions[i].matched_store_option_name, value = optionValue.store_category_value_name };
            }
            newProductRequest.stockItems = new ProductSkuRequest[]
                {
                    new ProductSkuRequest
                    {
                        quantity = ((int)productFromDb.quantity).ToString(),
                        attributes = attributeRequest,
                        optionPrice = newProductRequest.price
                    }
                };
            newProductRequest.attributes = attributeRequest;
            newProductRequest.shipmentTemplate = "RENASS";
            saveProductRequest.product = newProductRequest;
            var response = proxy.SaveProductAsync(saveProductRequest).Result;
            var product = response.SaveProductResponse.product;
            return string.IsNullOrEmpty(response.SaveProductResponse.result.errorMessage);
        }
        private CategoryFromStore LoadParentCategories(CategoryFromStore category)
        {
            if(category != null)
                category.ParentCategory = LoadParentCategories(_accountDbContext.CategoryFromStores.FirstOrDefault(c => c.Id == category.ParentId));
            return category;
        }
    }
}