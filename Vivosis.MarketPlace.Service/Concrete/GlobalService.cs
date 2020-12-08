using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;

namespace Vivosis.MarketPlace.Service.Concrete
{
    public class GlobalService :IGlobalService
    {
        MySqlConnection _connection;
        public GlobalService(IHttpContextAccessor httpContextAccessor)
        {
            _connection = new MySqlConnection(httpContextAccessor.HttpContext.Request.Cookies["VivosisConnectionString"]);
        }
        public IEnumerable<Product> GetProducts(IEnumerable<int> idList = null)
        {
            _connection.Open();
            var command = _connection.CreateCommand();
            if(idList?.Any() ?? false)
                command.LoadScript("SelectProductsByIdList_Included_Description_Category", string.Join(',', idList));
            else
                command.LoadScript("SelectProducts_Included_Description_Category");
            var dataReader = command.ExecuteReader();
            var products = dataReader.HasRows ? new List<Product>() : null;
            while(dataReader.Read())
            {
                var productId = (int)dataReader["product_id"];
                var product = products.FirstOrDefault(p => p.product_id == productId);
                var isProductExist = product != null;
                if(!isProductExist)
                {
                    product = new Product();
                    product.product_id = productId;
                    product.quantity = (int)dataReader["quantity"];
                    product.name = (string)dataReader["name"];
                    product.price = (decimal)dataReader["price"];
                    product.model = (string)dataReader["model"];
                    product.description = (string)dataReader["description"];
                }
                if(dataReader["category_id"] != DBNull.Value)
                {
                    var productCategory = new ProductCategory { category_id = (int)dataReader["category_id"], product_id = productId };
                    product.ProductCategories ??= new List<ProductCategory>();
                    product.ProductCategories.Add(productCategory);
                }
                if(!isProductExist)
                    products.Add(product);
            }
            _connection.Close();
            command.Dispose();
            dataReader.Dispose();
            return products;
        }
        public IEnumerable<Category> GetCategories(IEnumerable<int> idList = null)
        {
            _connection.Open();
            var command = _connection.CreateCommand();
            if(idList?.Any() ?? false)
                command.LoadScript("SelectCategoriesByIdList_Included_Description_Product", string.Join(',', idList));
            else
                command.LoadScript("SelectCategories_Included_Description_Product");
            var dataReader = command.ExecuteReader();
            var categories = dataReader.HasRows ? new List<Category>() : null;
            while(dataReader.Read())
            {
                var categoryId = (int)dataReader["category_id"];
                var category = categories.FirstOrDefault(p => p.category_id == categoryId);
                var isCategoryExist = category != null;
                if(!isCategoryExist)
                {
                    category = new Category();
                    category.category_id = categoryId;
                    category.name = (string)dataReader["name"];
                    category.description = (string)dataReader["description"];
                    categories.Add(category);
                }
            }
            _connection.Close();
            command.Dispose();
            dataReader.Dispose();
            return categories;
        }
    }
}
