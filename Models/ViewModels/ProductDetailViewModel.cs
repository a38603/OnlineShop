using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ProductDetailViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public string ProductImg { get; set; }
        public int Stock { get; set; }
        public decimal Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int? ImageId { get; set; }
        public string ImageName { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
