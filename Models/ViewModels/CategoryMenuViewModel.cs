using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Models.ViewModels
{
    public class CategoryMenuViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<BrandViewModel> Brands { get; set; } = new List<BrandViewModel>();
    }

}
