
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRep;
        private Cart _cart;

        public OrderController(IOrderRepository repository, Cart cartService)
        {
            _orderRep = repository;
            _cart = cartService;
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if(_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty");
            }

            if(ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                _orderRep.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }

        public ViewResult List() => View(_orderRep.Order.Where(o => !o.Shipped));

        [HttpPost]
        public IActionResult MarkShipped(int orderId)
        {
            Order order = _orderRep.Order.FirstOrDefault(o => o.OrderID == orderId);
            if(order != null)
            {
                order.Shipped = true;
                _orderRep.SaveOrder(order);
            }
            return RedirectToAction(actionName: nameof(List));
        }
    }
}