using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Framework;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        // GET: Admin/Brand
        public ActionResult Index()
        {
            var model = new BrandModel();
            var brandList = model.ListAll(); // Lấy danh sách thương hiệu từ database

            // Kiểm tra nếu không có dữ liệu hoặc danh sách null
            if (brandList == null || !brandList.Any())
            {
                // Có thể xử lý thêm trường hợp không có dữ liệu
                ViewBag.Message = "Không có dữ liệu thương hiệu.";
                return View(new List<Brand>()); // Trả về danh sách trống nếu không có dữ liệu
            }

            return View(brandList);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Brand collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Tạo một đối tượng BrandModel 
                    var model = new BrandModel();

                    // Kiểm tra nếu có tệp logo được tải lên
                    if (Request.Files["Logo"] != null && Request.Files["Logo"].ContentLength > 0)
                    {
                        var file = Request.Files["Logo"];
                        var fileName = Path.GetFileName(file.FileName);
                        var folderPath = Server.MapPath("~/Assets/Img_Brands"); // Khớp với đường dẫn public
                        var fullPath = Path.Combine(folderPath, fileName);

                        // Lưu file logo vào thư mục
                        file.SaveAs(fullPath);

                        // Lưu đường dẫn của logo vào thuộc tính Logo của Brand (hoặc Brand)
                        collection.Logo = "~/Assets/Img_Brands/" + fileName;
                    }

                    // Gọi phương thức Create của model để lưu Brand vào cơ sở dữ liệu
                    int res = model.Create(collection.BrandName, collection.Logo); // Chú ý thêm logo vào nếu cần

                    if (res > 0)
                    {
                        // Nếu thành công, chuyển hướng đến trang Index
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Nếu lỗi, thêm thông báo lỗi vào ModelState
                        ModelState.AddModelError("", "Lỗi khi thêm Brand vào cơ sở dữ liệu.");
                    }
                }

                // Nếu ModelState không hợp lệ, trả về view với dữ liệu hiện tại
                return View(collection);
            }
            catch (Exception ex)
            {
                // Nếu có lỗi xảy ra trong quá trình xử lý, thêm lỗi vào ModelState
                ModelState.AddModelError("", "Đã xảy ra lỗi: " + ex.Message);
                return View();
            }
        }
        // GET: Admin/Brand/Edit/{id}
        public ActionResult Edit(int id)
        {
            var model = new BrandModel();
            var brand = model.GetById(id); // Lấy Brand theo ID

            if (brand == null)
            {
                return HttpNotFound();
            }

            return View(brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brand collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new BrandModel();

                    // Xử lý file logo nếu được upload mới
                    var file = Request.Files["Logo"];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var folderPath = Server.MapPath("~/Assets/Img_Brands");

                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        var fullPath = Path.Combine(folderPath, fileName); // ✅ đúng chỗ cần lưu
                        file.SaveAs(fullPath);

                        // Chỉ lưu tên file, không lưu đường dẫn đầy đủ
                        collection.Logo = fileName;
                    }


                    // Gọi hàm Update
                    int res = model.Update(collection.BrandId, collection.BrandName, collection.Logo);

                    if (res > 0)
                    {
                        return RedirectToAction("Index");
                    }

                    ModelState.AddModelError("", "Cập nhật không thành công.");
                }

                return View(collection);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi: " + ex.Message);
                return View(collection);
            }
        }
        // POST: Admin/Brand/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var model = new BrandModel();
                int res = model.DeleteBrand(id); // Gọi hàm xóa brand theo ID
                if (res > 0)
                {
                    TempData["Message"] = "Xóa thành công";
                    return RedirectToAction("Index"); // Quay lại danh sách nếu xóa thành công
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi xóa thương hiệu.");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Không thể xóa thương hiệu đang được sử dụng.";
                ModelState.AddModelError("", "Đã xảy ra lỗi: " + ex.Message);
            }
            return RedirectToAction("Index"); // Quay lại danh sách dù có lỗi
        }



    }
}