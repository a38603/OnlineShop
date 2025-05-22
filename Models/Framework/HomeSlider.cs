using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Framework
{
    public class HomeSlider
    {
        public int SliderId { get; set; }
        public string SliderTitle { get; set; }
        public string SliderSubTitle { get; set; }
        public string SliderDescription { get; set; }
        public string SliderImage { get; set; }
        public bool IsActive { get; set; }

    }
}
