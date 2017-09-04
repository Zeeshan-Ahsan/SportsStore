using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository _prodRep;

        public NavigationMenuViewComponent(IProductRepository rep)
        {
            _prodRep = rep;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.CurrentCategory = RouteData?.Values["category"];
            
            return View(_prodRep.Products
                        .Select(p => p.Category)
                        .Distinct()
                        .OrderBy(p => p));
        }
    }
}