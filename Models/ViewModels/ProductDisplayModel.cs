using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Models.ViewModels
{
    public class ProductDisplayModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string ProductImg { get; set; }

        public string OldProductImg { get; set; }  // Dùng để lưu ảnh cũ khi edit

        [Display(Name = "Category Name")]
        public int CategoryId { get; set; }

        [Display(Name = "Brand Name")]
        public int BrandId { get; set; }

        public int Stock { get; set; }

        public decimal Rating { get; set; }

        public DateTime CreatedAt { get; set; }

        // Dùng cho dropdown list chọn Brand, Category trong form Create/Edit
        public SelectList BrandList { get; set; }
        public SelectList CategoryList { get; set; }

        // Để hiển thị tên Brand, Category bên ngoài (ví dụ trong list)
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
    }
}
