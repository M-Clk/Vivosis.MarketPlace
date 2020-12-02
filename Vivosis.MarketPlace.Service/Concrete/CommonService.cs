using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;

namespace Vivosis.MarketPlace.Service.Concrete
{
    public class CommonService :ICommonService
    {
        MarketPlaceDbContext _dbContext;
        MySqlConnection _connection;
        public CommonService(MarketPlaceDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            var connectionString = configuration.GetConnectionString("RemoteDatabase");
            _connection = new MySqlConnection(connectionString);
        }
        public void SyncLocalProducts()
        {
            _connection.Open();
            var command = _connection.CreateCommand();
            command.LoadScript("SelectProducts_Included_Description_Category_Description");
            var dataReader = command.ExecuteReader();
            while(dataReader.Read())
            {
                var productId = (int)dataReader["p_id"];
                var product = _dbContext.Products.Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).FirstOrDefault(p => p.product_id == productId);
                var isProductExist = product != null;
                if(!isProductExist)
                {
                    product = new Product();
                    product.product_id = productId;
                    product.quantity = (int)dataReader["p_quantity"];
                    product.name = (string)dataReader["p_name"];
                    product.price = (decimal)dataReader["p_price"];
                    product.image_url = (string)dataReader["p_image"];
                }
                if(dataReader["c_id"] != DBNull.Value)
                {
                    var pc = product.ProductCategories.FirstOrDefault(pc => pc.category_id == (int)dataReader["c_id"] && pc.product_id == productId);
                    if(pc != null)
                    {
                        pc.Category.name = (string)dataReader["c_name"];
                    }
                    else
                    {
                        var productCategory = new ProductCategory { category_id = (int)dataReader["c_id"], product_id = productId };
                        productCategory.Category = new Category
                        {
                            category_id = (int)dataReader["c_id"],
                            name = (string)dataReader["c_name"]
                        };
                    }
                }
                if(isProductExist)
                    _dbContext.Products.Update(product);
                else
                    _dbContext.Products.Add(product);
            }
            _connection.Close();
            command.Dispose();
            dataReader.Dispose();
            _dbContext.SaveChanges();
        }
    }
}
