using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Order_list()
        {
            return View();
        }
        public ActionResult Order_detail()
        {
            return View();
        }
        public ActionResult Add_address()
        {
            return View();
        }
    }
}