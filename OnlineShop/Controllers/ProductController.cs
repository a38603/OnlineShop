using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Framework;
using Models.ViewModels;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private OnlineShopDbContext context = new OnlineShopDbContext();
        private ProductModel productModel = new ProductModel();
        // GET: Product
        public ActionResult ProductsByCategory(int categoryId)
        {
            // Kiểm tra categoryId nhận được
            Debug.WriteLine($"CategoryId nhận được: {categoryId}");
            var products = productModel.GetProductsByCategory(categoryId);

            return View(products);
        }
        public ActionResult Detail(int id)
        {
            // Gọi store procedure và map ra danh sách flat model
            var productData = context.Database.SqlQuery<ProductDetailFlatModel>(
                "EXEC Sp_Product_Detail @ProductId",
                new SqlParameter("@ProductId", id)
            ).ToList();

            if (!productData.Any())
            {
                return HttpNotFound(); // hoặc xử lý khác nếu không có dữ liệu
            }

            // Lấy dòng đầu tiên để lấy thông tin sản phẩm chính
            var first = productData.First();

            // Tạo ViewModel và gán dữ liệu
            var productDetail = new ProductDetailViewModel
            {
                ProductId = first.ProductId,
                ProductName = first.ProductName,
                Price = first.Price,
                ProductImg = first.ProductImg,
                Stock = first.Stock,
                Rating = first.Rating,
                CreatedAt = first.CreatedAt,
                BrandId = first.BrandId,
                BrandName = first.BrandName,
                ProductImages = productData
                    .Where(x => x.ImageId.HasValue)
                    .Select(x => new ProductImage
                    {
                        ImageId = x.ImageId.Value,
                        ProductId = x.ProductId,
                        ImageName = x.ImageName
                    }).ToList()
            };

            return View(productDetail);
        }
        public ActionResult Search(string searchString)
        {
            // Nếu searchString null hoặc rỗng thì truyền DBNull.Value để stored procedure hiểu đúng
            var param = string.IsNullOrEmpty(searchString) ? (object)DBNull.Value : searchString;

            var products = context.Database.SqlQuery<ProductViewModel>("EXEC Sp_Product_SearchByName @p0", param).ToList();

            ViewBag.Keyword = searchString;
            ViewBag.ResultCount = products.Count;

            return View(products);
        }




    }
}