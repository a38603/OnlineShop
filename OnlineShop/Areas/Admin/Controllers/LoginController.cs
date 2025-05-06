using System.Web.Mvc;
using OnlineShop.Areas.Admin.Code;
using OnlineShop.Areas.Admin.Models;
using System.Web.Security;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            //var result = new AccountModel().Login(model.UserName, model.Password);
            {
                if (Membership.ValidateUser(model.UserName, model.Password) && ModelState.IsValid)
                {
                    //SessionHelper.SetSession(new UserSession { UserName = model.UserName});
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }


                else
                {
                    ModelState.AddModelError("", " Tên đăng nhập hoặc mật khảu không đúng.");
                }

                return View(model);
            }
        }
        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "login");
        }
    }
}