
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers
{
    public class OrderReturnController : Controller
    {
        public ViewResult ListCanceledOrders() => View();
    }
}
