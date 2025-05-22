using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using System.Web.Mvc;
using Models.Framework;
using Models.ViewModels;
using static Models.ViewModels.CategoryMenuViewModel;

namespace OnlineShop.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult CategoryMenu()
        {
            var model = BuildCategoryMenu(); // Lấy menu đã chuyển đổi đúng kiểu ViewModel
            return PartialView("_CategoryMenu", model); // Truyền sang partial view
        }

        public List<CategoryMenuViewModel> BuildCategoryMenu()
        {
            var rawData = new ProductModel().ListAll(); // gọi stored procedure lấy product + brand + category
            
            var menu = rawData
                .GroupBy(p => new { p.CategoryId, p.CategoryName })
                .Select(categoryGroup => new CategoryMenuViewModel
                {
                    CategoryId = categoryGroup.Key.CategoryId,
                    CategoryName = categoryGroup.Key.CategoryName,
                    Brands = categoryGroup
                        .GroupBy(p => new { p.BrandId, p.BrandName })
                        .Select(brandGroup => new BrandViewModel
                        {
                            BrandId = brandGroup.Key.BrandId,
                            BrandName = brandGroup.Key.BrandName,
                            Products = brandGroup
                                .Select(p => new ProductViewModel
                                {
                                    ProductId = p.ProductId,
                                    ProductName = p.ProductName,
                                    Price = p.Price,
                                    ProductImg = p.ProductImg,
                                    Stock = p.Stock,
                                    Rating = (decimal)p.Rating,
                                    CreatedAt = (DateTime)p.CreatedAt,
                                    BrandId = p.BrandId,
                                    CategoryId = p.CategoryId,
                                    BrandName = p.BrandName,
                                    CategoryName = p.CategoryName
                                }).ToList()
                        }).ToList()
                }).ToList();

            return menu;
        }
        


    }
}