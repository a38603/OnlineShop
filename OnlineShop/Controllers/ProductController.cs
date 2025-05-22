using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Framework;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private ProductModel productModel = new ProductModel();
        // GET: Product
        public ActionResult ProductsByCategory(int categoryId)
        {
            // Kiểm tra categoryId nhận được
            Debug.WriteLine($"CategoryId nhận được: {categoryId}");
            var products = productModel.GetProductsByCategory(categoryId);

            return View(products);
        }
       
    }
}