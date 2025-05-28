using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;

namespace Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<HomeSlider> Sliders { get; set; }
        public IEnumerable<ProductViewModel> TopProducts { get; set; }
        public IEnumerable<ProductTopRatingViewModel> TopRatings {  get; set; }
        public List<BrandViewModel> ProductByBrand { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
