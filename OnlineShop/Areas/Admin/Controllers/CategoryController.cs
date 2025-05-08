using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Framework;
using Models;
using System.Data.Entity;

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

        // GET: Admin/Category/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new CategoryModel();
            var category = model.GetById(id); // Lấy Category theo ID
            if (category == null)
            {
                return HttpNotFound();
            }
            return View("Edit",category);
        }

        // POST: Admin/Category/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new CategoryModel();
                    int res = model.Update(collection.ID, collection.Name, collection.Alias, collection.ParentID, collection.Order, collection.Status);
                    if (res > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Lỗi update");
                    }
                }
                return View("Edit",collection);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi: " + ex.Message);
                return View("Edit", collection);
            }
        }

        // POST: Admin/Category/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var model = new CategoryModel();
                int res = model.DeleteCategory(id); // Xóa theo ID
                if (res > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi xóa");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi: " + ex.Message);
            }
            return RedirectToAction("Index");
        }


    }
}
