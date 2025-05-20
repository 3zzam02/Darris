using BestStoreMVC.Models;
using BestStoreMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BestStoreMVC.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly int pageSize = 8;

        public StoreController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index(int pageIndex = 1, string? search = "", string? category = "", string? sort = "newest")
        {
            IQueryable<Product> query = context.Products;

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(p => p.Category == category);
            }

            switch (sort)
            {
                case "hours_asc":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "hours_desc":
                    query = query.OrderByDescending(p => p.Price);
                    break;
                default:
                    query = query.OrderByDescending(p => p.Id);
                    break;
            }

            if (pageIndex < 1) pageIndex = 1;

            int count = query.Count();
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var products = query.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            ViewBag.Products = products;
            ViewBag.PageIndex = pageIndex;
            ViewBag.TotalPages = totalPages;

            var model = new StoreSearchModel
            {
                Search = search,
                Category = category,
                Sort = sort
            };

            return View(model);
        }


        public IActionResult Details(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Store");
            }
          

            return View(product);
        }
        public IActionResult Tree(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Store");
            }
            context.Products
                          .Include(p => p.Images)  // ضروري لتحميل الصور مع المنتج
                          .FirstOrDefault(p => p.Id == id);
            return View(product);
        }
    }
}
