using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ProductTopRatingViewModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string ProductImg { get; set; }

        public int Stock { get; set; }

        public decimal Rating { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}
