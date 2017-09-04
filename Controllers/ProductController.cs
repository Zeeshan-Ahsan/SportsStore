using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private const int _pageSize = 4;
        private readonly IProductRepository repository;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page) => View(new ProductsListViewModel
        {
            Products = repository.Products.Where(p => category == null || p.Category == category)
                                          .OrderBy(p => p.ProductID)
                                          .Skip((page - 1) * _pageSize)
                                          .Take(_pageSize),
                                            PagingInfo = new PagingInfo
                                            {
                                                CurrentPage = page,
                                                ItemsPerPage = _pageSize,
                                                TotalItems = category == null ? repository.Products.Count()
                                                                              : repository.Products.Where(r =>
                                                                                r.Category == category).Count()
                                            },
                                            CurrentCategory = category
        });        
    }
}