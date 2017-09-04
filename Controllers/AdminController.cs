
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductRepository _rep;

        public AdminController(IProductRepository repository)
        {
            _rep = repository;
        }

        public ViewResult Index() => View(_rep.Products);

        public ViewResult AddProduct() => View(nameof(EditProduct), new Product());
        public ViewResult EditProduct(int productId) => View(_rep.Products.FirstOrDefault(p => p.ProductID == productId));

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _rep.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved!";
                return RedirectToAction(nameof(Index));
            }
            return View(product);   
        }

        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {            
            Product deletedProduct = _rep.DeleteProduct(productId);
            if(deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} has been deleted!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}