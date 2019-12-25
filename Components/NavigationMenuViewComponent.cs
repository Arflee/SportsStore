using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IProductRepository productRepository;

        public NavigationMenuViewComponent(IProductRepository repo)
        {
            productRepository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(productRepository.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(p => p));
        }
    }
}
