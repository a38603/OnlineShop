using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Models.Framework;

namespace Models.ViewModels
{
    public class ProductCreateViewModel
    {
        public int ProductId { get; set; } // dùng cho Edit
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public string ProductImg { get; set; }
        [Display(Name = "Category Name")]
        public int CategoryId { get; set; }
        [Display(Name = "Brand Name")]
        public int BrandId { get; set; }

        public int Stock { get; set; }

        public decimal Rating { get; set; }
        public DateTime CreatedAt { get; set; }

        // Dropdown list
        public SelectList BrandList { get; set; }
        public SelectList CategoryList { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
    }
}
