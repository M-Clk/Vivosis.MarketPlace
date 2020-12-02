using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;

namespace Vivosis.MarketPlace.Service.Concrete
{
    public class CommonService :ICommonService
    {
        MarketPlaceDbContext _dbContext;
        MySqlConnection _connection;
        public CommonService(MarketPlaceDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            var userName = httpContextAccessor.HttpContext.User.Identity.Name;
            var connectionString = UserConnectionStringPairs.UserConnectionString[userName];
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
                var product = _dbContext.Products.FirstOrDefault(p => p.product_id == productId); //TODO Optimize et
                var isProductExist = product != null;
                if(!isProductExist)
                {
                    product = new Product();
                    product.product_id = productId;
                    product.quantity = (int)dataReader["p_quantity"];
                    product.name = (string)dataReader["p_name"];
                    product.price = (decimal)dataReader["p_price"];
                    product.image_url = (string)dataReader["p_image"];
                    product.date_added = DateTime.Now;
                }
                if(dataReader["c_id"] != DBNull.Value)
                {
                    var category = _dbContext.Categories.Include(c => c.CategoryProducts).FirstOrDefault(pc => pc.category_id == (int)dataReader["c_id"]);
                    if(category != null)
                    {
                        category.name = (string)dataReader["c_name"];
                        if(!(category.CategoryProducts?.Any(cp => cp.product_id == productId) ?? false))
                            category.CategoryProducts.Add(new ProductCategory { category_id = (int)dataReader["c_id"], product_id = productId });
                        _dbContext.Categories.Update(category);
                    }
                    else
                    {
                        category = new Category
                        {
                            category_id = (int)dataReader["c_id"],
                            name = (string)dataReader["c_name"]
                        };
                        _dbContext.Categories.Add(category);
                    }
                }
                if(isProductExist)
                    _dbContext.Products.Update(product);
                else
                    _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
            }
            _connection.Close();
            command.Dispose();
            dataReader.Dispose();
        }
    }
}
