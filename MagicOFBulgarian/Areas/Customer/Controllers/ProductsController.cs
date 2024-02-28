using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MagicOFBulgarian.Data;
using MagicOFBulgarian.Models;
using MagicOFBulgarian.Models.NewModelView;
using System.Net;
using Microsoft.AspNetCore.Hosting;

namespace MagicOFBulgarian.Customer.Admin.Controllers
{
    [Area("Customer")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        
        public async Task<IActionResult> Index(string nameSearch)
        {
            ViewData["NameSearch"] = nameSearch;

            // Get all products with optional filtering by name
            IQueryable<Product> query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.FolkloreArea)
                .Include(p => p.GenderClothes);

            if (!string.IsNullOrEmpty(nameSearch))
            {
                query = query.Where(p => p.Title.Contains(nameSearch));
            }

            List<Product> productList = await query.ToListAsync();

            return View(productList);
        }
        [HttpGet]
        public async Task<IActionResult> SearchByPrice(double? minPrice, double? maxPrice)
        {
            ViewData["MinPrice"] = minPrice;
            ViewData["MaxPrice"] = maxPrice;

            // Get all products with optional filtering by price range
            IQueryable<Product> query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.FolkloreArea)
                .Include(p => p.GenderClothes);

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            List<Product> productList = await query.ToListAsync();

            return View(productList);
        }
        [HttpGet]
        public async Task<IActionResult> SearchByCategory(int? categoryId)
        {
            ViewData["CategoryId"] = categoryId;

            // Get all products with optional filtering by category
            IQueryable<Product> query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.FolkloreArea)
                .Include(p => p.GenderClothes);

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            List<Product> productList = await query.ToListAsync();

            // Get the list of categories and pass it to the view
            ViewData["Categories"] = await _context.Categories.ToListAsync();

            return View(productList);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.FolkloreArea)
                .Include(p => p.GenderClothes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

       
        
        
    }
}
