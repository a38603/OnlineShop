 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Framework;
using Models.ViewModels;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private OnlineShopDbContext context = new OnlineShopDbContext();
       

        // GET: Admin/Home
       public ActionResult Index(int? brandId)
       {
            // Load danh sách brand để hiển thị bên sidebar
            var brands = context.Database.SqlQuery<BrandViewModel>("Sp_Brand_ListAll").ToList();

            // Load sản phẩm theo brandId nếu có, hoặc load mặc định (ví dụ sản phẩm mới nhất)
            List<ProductViewModel> products;
            if (brandId.HasValue)
            {
                products = context.Database.SqlQuery<ProductViewModel>("EXEC Sp_Product_GetByBrand @BrandId", new SqlParameter("@BrandId", brandId.Value)).ToList();
            }
            else
            {
                // Lấy tất cả sản phẩm với đầy đủ thông tin BrandName, CategoryName
                products = context.Database.SqlQuery<ProductViewModel>("EXEC Sp_Product_listAll").ToList();
            }

            var sliders = context.Database.SqlQuery<HomeSlider>("Sp_HomeSlider_ListAll").ToList();
            var topProducts = context.Database.SqlQuery<ProductViewModel>("Sp_Product_ListTop5Newest").ToList();
            var topRating = context.Database.SqlQuery<ProductTopRatingViewModel>("Sp_Product_GetTop5ByRating").ToList();
            var blogs = context.Database.SqlQuery<Blog>( "SELECT TOP 3 * FROM Blogs ORDER BY PublishedDate DESC").ToList();
            var viewModel = new HomeIndexViewModel
           {
               Sliders = sliders,
               TopProducts = topProducts,
               TopRatings = topRating,
               ProductByBrand = brands,  // danh sách brand hiện sẵn bên trái
               Products = products,
               Blogs= blogs
           };

           return View(viewModel);
           }

        public ActionResult About()
        {
          
            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
        public ActionResult Privacy()
        {
            return View();
        }
        private ProductModel productModel = new ProductModel();
        public ActionResult GetProductsByBrand(int? brandId)
        {
            List<ProductViewModel> products;

            using (var context = new OnlineShopDbContext())
            {
                if (brandId.HasValue)
                {
                    products = context.Database.SqlQuery<ProductViewModel>(
                        "EXEC Sp_Product_GetByBrand @BrandId",
                        new SqlParameter("@BrandId", brandId.Value)
                    ).ToList();
                }
                else
                {
                    // Gọi stored procedure lấy tất cả sản phẩm
                    products = context.Database.SqlQuery<ProductViewModel>(
                        "EXEC Sp_Product_listAll"
                    ).ToList();
                }
            }

            return PartialView("_ProductListPartial", products);
        }




    }
}