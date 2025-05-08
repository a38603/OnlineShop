using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Framework;
using Models;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            var iplCate = new CategoryModel();
            var model =iplCate.ListAll();
            return View(model);
        }

        // GET: Admin/Category/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: Admin/Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category collection)
        {
            // TODO: Add insert logic here
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new CategoryModel();
                    int res = model.Create(collection.Name,collection.Alias, collection.ParentID, collection.Order, collection.Status);
                    if (res > 0)
                    {
                       
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "lỗi insert");
                    }
                    
                }
                return View(collection);
                

                
            }
            catch(Exception ex)
            {

     
                ModelState.AddModelError("", "Đã xảy ra lỗi: " +ex.Message);
                return View();
            }
        }
       
    }
}
