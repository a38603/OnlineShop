using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc; 
using System.Data.Entity;
using Models.Framework;
using System.ComponentModel;

namespace Models.ViewModels
{
    public class ProductEditViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ProductImg { get; set; }
        public string OldProductImg { get; set; }
        [Display(Name = "Category Name")]
        public int CategoryId { get; set; }
        [Display(Name = "Brand Name")]
        public int BrandId { get; set; }
        public int Stock { get; set; }
        public decimal Rating { get; set; }
        public DateTime CreatedAt { get; set; }

        public SelectList BrandList { get; set; }
        public SelectList CategoryList { get; set; }

        public string BrandName { get; set; } // để hiển thị (nếu cần)
        public string CategoryName { get; set; } // để hiển thị (nếu cần)
    }

}
