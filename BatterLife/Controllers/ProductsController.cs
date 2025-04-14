using BatterLife.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BatterLife.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(string? category, string? sortBy)
        {
            var products = await _productService.GetAllProductsAsync(category, sortBy);
            ViewBag.SelectedCategory = category ?? "all";
            ViewBag.SortBy = sortBy ?? "name-asc";
            return View(products);
        }
    }
}