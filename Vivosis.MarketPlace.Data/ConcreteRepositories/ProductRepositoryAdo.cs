using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Vivosis.MarketPlace.Data.AbstractRepositories;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Data.ConcreteRepositories
{
    public class ProductRepositoryAdo :IProductRepositoryAdo
    {
        MySqlConnection _connection;
        public ProductRepositoryAdo(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RemoteDatabase");
            _connection = new MySqlConnection(connectionString);
        }
        public IEnumerable<Product> GetAll()
        {
            _connection.Open();
            var command = _connection.CreateCommand();
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
                var type = dataReader["category_id"].GetType();
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

        public IEnumerable<Product> GetByIdList(IEnumerable<int> idList)
        {
            _connection.Open();
            var command = _connection.CreateCommand();
            command.LoadScript("SelectProductsByIdList_Included_Description_Category", string.Join(',', idList));
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
                var type = dataReader["category_id"].GetType();
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

        public int Update(IEnumerable<Product> products)
        {
            throw new NotImplementedException();
        }
    }
}
