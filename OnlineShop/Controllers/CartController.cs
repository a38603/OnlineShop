using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Models.ViewModels;
using System.Data.SqlClient;
using Models.Framework;
using System; // đổi thành namespace của bạn chứa class Cart, Product

public class CartController : Controller
{
    private OnlineShopDbContext context = new OnlineShopDbContext();

    // Hiển thị giỏ hàng
    public ActionResult Index()
    {
        var cart = Session["CartSession"] as List<CartItem>;
        if (cart == null) cart = new List<CartItem>();
        return View(cart);
    }

    [HttpPost]
    public ActionResult AddToCart(int productId, int quantity = 1)
    {
        try
        {
            var product = context.Database.SqlQuery<ProductViewModel>("EXEC Sp_Product_ListWithBrandCategory").FirstOrDefault(p => p.ProductId == productId);
                           
                           
            if (product == null)
            {
                return Json(new { success = false, message = "Sản phẩm không tồn tại." });
            }

            var cart = Session["CartSession"] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            var cartItem = cart.FirstOrDefault(x => x.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    ProductImg = product.ProductImg,
                    Quantity = quantity
                });
            }

            Session["CartSession"] = cart;

            return Json(new { success = true, message = "Đã thêm sản phẩm vào giỏ hàng!" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Lỗi: " + ex.Message });
        }
    }




    // Xóa sản phẩm khỏi giỏ hàng
    public ActionResult Remove(int productId)
    {
        var cart = Session["CartSession"] as List<CartItem>;
        if (cart != null)
        {
            var itemToRemove = cart.FirstOrDefault(x => x.ProductId == productId);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                Session["CartSession"] = cart;
            }
        }
        return RedirectToAction("Index");
    }

    // Xóa hết giỏ hàng
    public ActionResult Clear()
    {
        Session["CartSession"] = null;
        return RedirectToAction("Index");
    }
    [HttpGet]
    public ActionResult CartHeaderPartial()
    {
        var cart = Session["CartSession"] as List<CartItem> ?? new List<CartItem>();
        return PartialView("_CartHeaderPartial", cart);
    }
    public JsonResult GetCartCount()
    {
        var cart = Session["CartSession"] as List<CartItem> ?? new List<CartItem>();
        int totalItems = cart.Sum(x => x.Quantity);
        return Json(new { count = totalItems }, JsonRequestBehavior.AllowGet);
    }
    public ActionResult HeaderCart()
    {
        var cart = Session["CartSession"] as List<CartItem> ?? new List<CartItem>();
        return PartialView("_HeaderCartPartial", cart);
    }
}
