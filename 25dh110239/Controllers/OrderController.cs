using System;
using System.Linq;
using System.Web.Mvc;
using _25dh110239.Models;
using _25dh110239.Models.ViewModel;

namespace _25dh110239.Controllers
{
    public class OrderController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // GET: Order/Checkout
        [Authorize]
        public ActionResult Checkout()
        {
            var cart = Session["Cart"] as Cart;
            if (cart == null || !cart.Items.Any())
            {
                return RedirectToAction("Index", "Home");
            }

            var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
            if (user == null) return RedirectToAction("Login", "Account");

            var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
            if (customer == null) return RedirectToAction("Login", "Account");

            var model = new CheckoutVM
            {
                CartItems = cart.Items.ToList(),
                TotalAmount = cart.TotalValue(),
                OrderDate = DateTime.Now,
                ShippingAddress = customer.CustomerAddress,
                CustomerID = customer.CustomerID,
                Username = customer.Username
            };

            return View(model);
        }

        // POST: Order/Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Checkout(CheckoutVM model)
        {
            if (ModelState.IsValid)
            {
                var cart = Session["Cart"] as Cart;
                if (cart == null || !cart.Items.Any()) return RedirectToAction("Index", "Home");

                var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
                var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);

                string paymentStatus = "Chưa thanh toán";
                switch (model.PaymentMethod)
                {
                    case "Tiền mặt": paymentStatus = "Thanh toán tiền mặt"; break;
                    case "Paypal": paymentStatus = "Thanh toán paypal"; break;
                    case "Mua trước trả sau": paymentStatus = "Trả góp"; break;
                }

                var order = new Order
                {
                    CustomerID = customer.CustomerID,
                    OrderDate = model.OrderDate,
                    TotalAmount = model.TotalAmount,
                    PaymentStatus = paymentStatus,
                    PaymentMethod = model.PaymentMethod,
                    DeliveryMethod = model.ShippingMethod,
                    ShippingAddress = model.ShippingAddress,
                    OrderDetails = cart.Items.Select(item => new OrderDetail
                    {
                        ProductID = item.ProductID,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    }).ToList()
                };

                db.Orders.Add(order);
                db.SaveChanges();

                Session["Cart"] = null; // Xóa giỏ hàng sau khi đặt thành công
                return RedirectToAction("OrderSuccess", new { id = order.OrderID });
            }

            return View(model);
        }

        // GET: Order/OrderSuccess
        // GET: Order/OrderSuccess
        // GET: Order/OrderSuccess
        public ActionResult OrderSuccess(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Home");

            var order = db.Orders.Include("OrderDetails").SingleOrDefault(o => o.OrderID == id);
            if (order == null) return HttpNotFound();

            return View(order);
        }
    }
}