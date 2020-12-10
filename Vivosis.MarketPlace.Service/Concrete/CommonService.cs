using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        public CommonService(MarketPlaceDbContext dbContext, IHttpContextAccessor httpContextAccessor, UserManager<SystemUser> userManager)
        {
            _dbContext = dbContext;
            var user = userManager.FindByNameAsync(httpContextAccessor.HttpContext.User.Identity.Name).Result;
            var connectionString = $"Server={user.Server}; Database={user.DbName}; Uid={user.DbUserName}; Pwd={user.DbPassword};";
            _connection = new MySqlConnection(connectionString);
        }

        public void SyncLocalOptions()
        {
            _connection.Open();
            var command = _connection.CreateCommand();
            command.LoadScript("SelectOptions_Included_OptionValue_ProductOption_ProductOptionValue");
            var dataReader = command.ExecuteReader();
            while(dataReader.Read())
            {
                var optionId = (int)dataReader["option_id"];
                var option = _dbContext.Options.FirstOrDefault(o => o.option_id == optionId);
                var isOptionExist = option != null;
                if(!isOptionExist)
                {
                    option = new Option();
                    option.option_id = optionId;
                    option.type = (string)dataReader["option_type"];
                    option.name = (string)dataReader["option_name"];
                    option.sort_order = (int)dataReader["option_sort_order"];
                }
                if(dataReader["product_option_id"] != DBNull.Value)
                {
                    var productOption = _dbContext.ProductOptions.FirstOrDefault(po => po.product_option_id == (int)dataReader["product_option_id"]);
                    if(productOption != null)
                    {
                        productOption.is_required = (bool)dataReader["product_option_required"];
                        productOption.value = (string)dataReader["product_option_value"];
                        _dbContext.ProductOptions.Update(productOption);
                    }
                    else
                    {
                        productOption = new ProductOption
                        {
                            product_option_id = (int)dataReader["product_option_id"],
                            option_id = optionId,
                            product_id = (int)dataReader["product_option_product_id"],
                            is_required = (bool)dataReader["product_option_required"],
                            value = (string)dataReader["product_option_value"]
                        };
                        _dbContext.ProductOptions.Add(productOption);
                    }
                }
                if(dataReader["option_value_id"] != DBNull.Value)
                {
                    var optionValue = _dbContext.OptionValues.FirstOrDefault(ov => ov.option_value_id == (int)dataReader["option_value_id"]);
                    if(optionValue != null)
                    {
                        optionValue.name = (string)dataReader["option_value_name"];
                        optionValue.sort_order = (int)dataReader["option_value_sort_order"];
                        _dbContext.OptionValues.Update(optionValue);
                    }
                    else
                    {
                        optionValue = new OptionValue
                        {
                            option_value_id = (int)dataReader["option_value_id"],
                            option_id = optionId,
                            name = (string)dataReader["option_value_name"],
                            sort_order = (int)dataReader["option_value_sort_order"]
                        };
                        _dbContext.OptionValues.Add(optionValue);
                    }
                }
                if(dataReader["product_option_value_id"] != DBNull.Value)
                {
                    var productOptionValue = _dbContext.ProductOptionValues.FirstOrDefault(po => po.product_option_value_id == (int)dataReader["product_option_value_id"]);
                    if(productOptionValue != null)
                    {
                        productOptionValue.option_value_id = (int)dataReader["option_value_id"];
                        productOptionValue.product_option_id = (int)dataReader["product_option_id"];
                        productOptionValue.quantity = (int)dataReader["product_option_value_quantity"];
                        productOptionValue.point = (int)dataReader["product_option_value_points"];
                        productOptionValue.price = (decimal)dataReader["product_option_value_price"];
                        productOptionValue.weight = (decimal)dataReader["product_option_value_weight"];
                        _dbContext.ProductOptionValues.Update(productOptionValue);
                    }
                    else
                    {
                        productOptionValue = new ProductOptionValue()
                        {
                            product_option_value_id = (int)dataReader["product_option_value_id"],
                            option_value_id = (int)dataReader["option_value_id"],
                            product_option_id = (int)dataReader["product_option_id"],
                            quantity = (int)dataReader["product_option_value_quantity"],
                            point = int.Parse((string)dataReader["product_option_value_points"]),
                            price = decimal.Parse((string)dataReader["product_option_value_price"]),
                            weight = decimal.Parse((string)dataReader["product_option_value_weight"])
                        };
                        _dbContext.ProductOptionValues.Add(productOptionValue);
                    }
                }
                if(isOptionExist)
                    _dbContext.Options.Update(option);
                else
                    _dbContext.Options.Add(option);
                _dbContext.SaveChanges();
            }
            _connection.Close();
            command.Dispose();
            dataReader.Dispose();
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
