using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Models.Framework;
using Models.ViewModels;

namespace OnlineShop.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        private OnlineShopDbContext context = new OnlineShopDbContext();
        public ActionResult Index()
        {
            var blogs = context.Database.SqlQuery<Blog>("Sp_Blogs_ListAll").ToList();

            return View(blogs);
        }
        public ActionResult Detail(int id)
        {
            var blog = context.Database.SqlQuery<Blog>("EXEC Sp_Blog_GetById @BlogId", new SqlParameter("@BlogId", id)).FirstOrDefault();

            if (blog == null)
            {
                return HttpNotFound();
            }

            return View(blog);
        }
    }
}