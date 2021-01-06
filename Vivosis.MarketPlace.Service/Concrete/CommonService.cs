using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        SystemUser _user;
        UserManager<SystemUser> _userManager;
        IConfiguration _configuration;
        public CommonService(MarketPlaceDbContext dbContext, IHttpContextAccessor httpContextAccessor, UserManager<SystemUser> userManager, IConfiguration configuration)
        {
            if(httpContextAccessor?.HttpContext?.User?.Identity?.Name == null)
                return;
            _dbContext = dbContext;
            _userManager = userManager;
            _user = userManager.FindByNameAsync(httpContextAccessor.HttpContext.User.Identity.Name).Result;
            var connectionString = $"Server={_user.Server}; Database={_user.DbName}; Uid={_user.DbUserName}; Pwd={_user.DbPassword};";
            _connection = new MySqlConnection(connectionString);
            _configuration = configuration;
        }

        void SyncLocalOptions()
        {
            _connection.Open();
            //Load Options(Values)
            var command = _connection.CreateCommand();
            command.LoadScript("SelectOptions_Included_OptionValue");
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
                if(isOptionExist)
                    _dbContext.Options.Update(option);
                else
                    _dbContext.Options.Add(option);
                _dbContext.SaveChanges();
            }
            //Load ProductOptions(Values)
            _connection.Close();
            _connection.Open();
            command = _connection.CreateCommand();
            command.LoadScript("SelectProductOptions_Included_ProductOptionValue");
            dataReader = command.ExecuteReader();
            while(dataReader.Read())
            {
                var productOptionId = (int)dataReader["product_option_id"];
                var productOption = _dbContext.ProductOptions.FirstOrDefault(o => o.product_option_id == productOptionId);
                var isProductOptionExsist = productOption != null;
                if(!isProductOptionExsist)
                {
                    productOption = new ProductOption
                    {
                        product_option_id = (int)dataReader["product_option_id"],
                        option_id = (int)dataReader["product_option_option_id"],
                        product_id = (int)dataReader["product_option_product_id"],
                        is_required = (bool)dataReader["product_option_required"],
                        value = (string)dataReader["product_option_value"]
                    };
                }
                else
                {
                    productOption.is_required = (bool)dataReader["product_option_required"];
                    productOption.value = (string)dataReader["product_option_value"];
                }
                if(dataReader["product_option_value_id"] != DBNull.Value)
                {
                    var productOptionValue = _dbContext.ProductOptionValues.FirstOrDefault(po => po.product_option_value_id == (int)dataReader["product_option_value_id"]);
                    if(productOptionValue != null && !productOptionValue.IsChanged)
                    {
                        productOptionValue.option_value_id = (int)dataReader["option_value_id"];
                        productOptionValue.product_option_id = productOptionId;
                        productOptionValue.quantity = (int)dataReader["product_option_value_quantity"];
                        productOptionValue.subtract = (bool)dataReader["product_option_value_subtract"];
                        productOptionValue.point = int.Parse((string)dataReader["product_option_value_points"]);
                        productOptionValue.price = decimal.Parse((string)dataReader["product_option_value_price"], CultureInfo.InvariantCulture);
                        productOptionValue.weight = decimal.Parse((string)dataReader["product_option_value_weight"], CultureInfo.InvariantCulture);
                        _dbContext.ProductOptionValues.Update(productOptionValue);
                    }
                    else
                    {
                        productOptionValue = new ProductOptionValue()
                        {
                            product_option_value_id = (int)dataReader["product_option_value_id"],
                            option_value_id = (int)dataReader["option_value_id"],
                            product_option_id = productOptionId,
                            quantity = (int)dataReader["product_option_value_quantity"],
                            subtract = (bool)dataReader["product_option_value_subtract"],
                            point = int.Parse((string)dataReader["product_option_value_points"]),
                            price = decimal.Parse((string)dataReader["product_option_value_price"], CultureInfo.InvariantCulture),
                            weight = decimal.Parse((string)dataReader["product_option_value_weight"], CultureInfo.InvariantCulture)
                        };
                        _dbContext.ProductOptionValues.Add(productOptionValue);
                    }
                }
                if(isProductOptionExsist)
                    _dbContext.ProductOptions.Update(productOption);
                else
                    _dbContext.ProductOptions.Add(productOption);
                _dbContext.SaveChanges();
            }
            _connection.Close();
            command.Dispose();
            dataReader.Dispose();
        }
        void SyncLocalCategories()
        {
            _connection.Open();
            var command = _connection.CreateCommand();
            command.LoadScript("SelectCategories_Included_Description");
            var dataReader = command.ExecuteReader();
            while(dataReader.Read())
            {
                var categoryId = (int)dataReader["c_id"];
                var category = _dbContext.Categories.FirstOrDefault(pc => pc.category_id == categoryId); //TODO Optimize et
                var categoryPathName = (string)dataReader["c_path_name"];
                var lastIndexOf = categoryPathName.LastIndexOf(" > ") + 3;
                var categoryName = lastIndexOf == 2 ? categoryPathName : categoryPathName.Substring(lastIndexOf, categoryPathName.Length - lastIndexOf);
                if(category != null)
                {
                    category.path_name = categoryPathName;
                    category.name = categoryName;
                    category.date_modified = DateTime.Now;
                    _dbContext.Categories.Update(category);
                }
                else
                {
                    category = new Category
                    {
                        category_id = categoryId,
                        name = categoryName,
                        date_added = DateTime.Now,
                        path_name = categoryPathName,
                        status = true
                    };
                    _dbContext.Categories.Add(category);
                }
            }
            _dbContext.SaveChanges();
            _connection.Close();
            command.Dispose();
            dataReader.Dispose();
        }
        void SyncLocalProducts()
        {
            _connection.Open();
            var command = _connection.CreateCommand();
            command.LoadScript("SelectProducts_Included_Description_ProductCategory");
            var dataReader = command.ExecuteReader();
            while(dataReader.Read())
            {
                var productId = (int)dataReader["p_id"];
                var product = _dbContext.Products.FirstOrDefault(p => p.product_id == productId); //TODO Optimize et
                var isProductExist = product != null;

                if(isProductExist)
                {
                    product.quantity = (int)dataReader["p_quantity"];
                    product.name = (string)dataReader["p_name"];
                    product.description = (string)dataReader["p_description"];
                    product.price = (decimal)dataReader["p_price"];
                    product.image_url = (string)dataReader["p_image"];
                    product.date_modified = DateTime.Now;
                }
                else
                {
                    product = new Product();
                    product.product_id = productId;
                    product.quantity = (int)dataReader["p_quantity"];
                    product.name = (string)dataReader["p_name"];
                    product.description = (string)dataReader["p_description"];
                    product.price = (decimal)dataReader["p_price"];
                    product.image_url = (string)dataReader["p_image"];
                    product.date_added = DateTime.Now;
                }
                if(dataReader["c_id"] != DBNull.Value && !(_dbContext.ProductCategories?.Any(pc => pc.category_id == (int)dataReader["c_id"] && pc.product_id == productId) ?? false))
                    _dbContext.ProductCategories.Add(new ProductCategory { category_id = (int)dataReader["c_id"], product_id = productId });
                if(dataReader["pi_id"] != DBNull.Value)
                {
                    var image = _dbContext.ProductImages?.FirstOrDefault(pi => pi.Id == (int)dataReader["c_id"] && pi.product_id == productId);
                    if(image == null)
                    {
                        image = new ProductImage();
                        image.order = (int)dataReader["pi_sort_order"];
                        image.url = (string)dataReader["pi_image"];
                        image.product_id = productId;
                        _dbContext.ProductImages.Add(image);
                    }
                    else
                    {
                        image.order = (int)dataReader["pi_sort_order"];
                        image.url = (string)dataReader["pi_image"];
                        image.product_id = productId;
                        _dbContext.ProductImages.Update(image);
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
        public void SyncDatabase()
        {
            SyncLocalCategories();
            SyncLocalProducts();
            SyncLocalOptions();
            var userSettings = _userManager.Users.Include(u => u.Settings).First(u => u.Id == _user.Id);
            userSettings.Settings.IsSynced = true;
            userSettings.Settings.LastSyncTime = DateTime.Now;
            _userManager.UpdateAsync(userSettings).Wait();
        }

        public Product GetProductToSendStore(StoreProduct productStore)
        { //Duzenlenecek 
            var productFromDb = _dbContext.Products.Where(p => p.product_id == productStore.product_id).Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).ThenInclude(c => c.CategoryStores).ThenInclude(cs => cs.CategoryOptions).ThenInclude(co => co.CategoryOptionValues).Include(p => p.ProductOptions).ThenInclude(po=>po.ProductOptionValues).ThenInclude(pov=>pov.OptionValue).Include(p=>p.ProductOptions).ThenInclude(po=>po.Option).Include(p=>p.ProductImages).FirstOrDefault();
            productFromDb.ProductStores = new List<StoreProduct> { productStore };
            return productFromDb;
        }

        public void SaveShipmentTemplates(IEnumerable<ShipmentTemplate> templates)
        {
            if(_dbContext.ShipmentTemplates.Any())
                _dbContext.ShipmentTemplates.UpdateRange(templates);
            else
                _dbContext.ShipmentTemplates.AddRange(templates);
            _dbContext.SaveChanges();
        }

        public IEnumerable<ShipmentTemplate> GetShipmentTemplate() => _dbContext.ShipmentTemplates;

        public void SyncDatabase(string userName)
        {
            _user = _userManager.FindByNameAsync(userName).Result;
            var connectionString = $"Server={_user.Server}; Database={_user.DbName}; Uid={_user.DbUserName}; Pwd={_user.DbPassword};";
            _connection = new MySqlConnection(connectionString);
            var unformatedConnectionString = _configuration.GetConnectionString("DynamicLocalDatabase");
            var connectionStringByUser = string.Format(unformatedConnectionString, $"db_{_user.UserName}");
            var options = new DbContextOptionsBuilder<MarketPlaceDbContext>().UseMySql(connectionStringByUser).Options;
            _dbContext = new MarketPlaceDbContext(options);
            SyncDatabase();
        }
    }
}
