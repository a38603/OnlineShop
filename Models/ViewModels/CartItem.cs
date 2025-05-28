using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ProductImg { get; set; }
        public int Quantity { get; set; }
        public string BrandName { get; set; }

        public string CategoryName { get; set; }
        public decimal Total => Price * Quantity;
    }
}
