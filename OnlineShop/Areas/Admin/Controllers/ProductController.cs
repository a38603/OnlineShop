using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Framework;
using Models.ViewModels;

namespace OnlineShop.Areas.Admin.Controllers
{
    
    public class ProductController : Controller
    {
        private OnlineShopDbContext context = new OnlineShopDbContext();
        private ProductModel productModel = new ProductModel();
        // GET: Admin/Product
        public ActionResult Index()
        {
            var model = new ProductModel();
            var productList = model.ListAll(); // Lấy danh sách thương hiệu từ database

            // Kiểm tra nếu không có dữ liệu hoặc danh sách null
            if (productList == null || !productList.Any())
            {
                // Có thể xử lý thêm trường hợp không có dữ liệu
                ViewBag.Message = "Không có dữ liệu thương hiệu.";
                return View(new List<Product>()); // Trả về danh sách trống nếu không có dữ liệu
            }
            return View(productList);
            
        }
        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            var vm = new ProductCreateViewModel
            {
                CreatedAt = DateTime.Now,
                BrandList = new SelectList(context.Brands, "BrandId", "BrandName"),
                CategoryList = new SelectList(context.Categories, "CategoryId", "CategoryName")
            };
            return View(vm);
        }

        // POST: Admin/Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreateViewModel model, HttpPostedFileBase ProductImg)
        {
            var productModel = new ProductModel();

            if (ModelState.IsValid)
            {
                string fileName = "default_logo.png";  // tên file ảnh mặc định

                if (ProductImg != null && ProductImg.ContentLength > 0)
                {
                    fileName = Path.GetFileName(ProductImg.FileName);
                    var folder = Server.MapPath("~/Assets/Img_Products");
                    Directory.CreateDirectory(folder);
                    ProductImg.SaveAs(Path.Combine(folder, fileName));
                }

                int res = productModel.Create(
                    model.ProductName,
                    model.Price,
                    fileName,
                    model.CategoryId,
                    model.BrandId,
                    model.Stock,
                    model.Rating,
                    model.CreatedAt
                );

                if (res > 0)
                    return RedirectToAction("Index");

                ModelState.AddModelError("", "Lỗi khi thêm sản phẩm vào cơ sở dữ liệu.");
            }

            model.BrandList = new SelectList(context.Brands, "BrandId", "BrandName", model.BrandId);
            model.CategoryList = new SelectList(context.Categories, "CategoryId", "CategoryName", model.CategoryId);
            return View(model);
        }
        // GET: Admin/Product/Edit
        public ActionResult Edit(int id)
        {
            var product = productModel.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var brand = context.Brands.Find(product.BrandId);
            var category = context.Categories.Find(product.CategoryId);

            var model = new ProductEditViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                ProductImg = product.ProductImg,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                Stock = product.Stock,
                Rating = (decimal)product.Rating,
                CreatedAt = (DateTime)product.CreatedAt,

                BrandList = new SelectList(context.Brands.ToList(), "BrandId", "BrandName", product.BrandId),
                CategoryList = new SelectList(context.Categories.ToList(), "CategoryId", "CategoryName", product.CategoryId),

                BrandName = brand?.BrandName,
                CategoryName = category?.CategoryName
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductEditViewModel model, HttpPostedFileBase ProductImg)
        {
           
            
            if (ModelState.IsValid)
            {
                string fileName = model.OldProductImg;

                if (ProductImg != null && ProductImg.ContentLength > 0)
                {
                    fileName = Path.GetFileName(ProductImg.FileName);
                    var folder = Server.MapPath("~/Assets/Img_Products");
                    Directory.CreateDirectory(folder);
                    ProductImg.SaveAs(Path.Combine(folder, fileName));
                }

                var productModel = new ProductModel();
               
                int res = productModel.Update(
                    model.ProductId,
                    model.ProductName,
                    model.Price,
                    fileName,
                    model.CategoryId,
                    model.BrandId,
                    model.Stock,
                    model.Rating ,
                    model.CreatedAt
                );

                if (res > 0)
                    return RedirectToAction("Index");

                ModelState.AddModelError("", "Lỗi khi cập nhật sản phẩm.");
            }

            model.BrandList = new SelectList(context.Brands, "BrandId", "BrandName", model.BrandId);
            model.CategoryList = new SelectList(context.Categories, "CategoryId", "CategoryName", model.CategoryId);
            return View(model);
        }
        // POST: Admin/Brand/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            
            
            var model = new ProductModel();
            int res = model.DeleteProduct(id); // Gọi hàm xóa brand theo ID
            if (res > 0)
            {
                TempData["Message"] = "Xóa thành công";
                return RedirectToAction("Index"); // Quay lại danh sách nếu xóa thành công
            }
            else
            {
                ModelState.AddModelError("", "Lỗi xóa sản phẩm.");
            }
            
            
            return RedirectToAction("Index"); // Quay lại danh sách dù có lỗi
        }



    }

}