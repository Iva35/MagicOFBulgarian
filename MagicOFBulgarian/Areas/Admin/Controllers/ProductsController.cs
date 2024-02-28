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

namespace MagicOFBulgarian.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Products.Include(p => p.Category).Include(p => p.FolkloreArea).Include(p => p.GenderClothes);
        //    return View(await applicationDbContext.ToListAsync());
        //}
        // GET: Products
        // GET: Products
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

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["AreaId"] = new SelectList(_context.FolkloreAreas, "AreaId", "AreaName");
            ViewData["GenderId"] = new SelectList(_context.GenderClothes, "GenderId", "GenderName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductImage product)
        {
            if (product.ImageUrl == null)
            {
                ModelState.AddModelError("ImageUrl", "Снимката е задължителна при създаване");

                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
                ViewData["AreaId"] = new SelectList(_context.FolkloreAreas, "AreaId", "AreaName", product.AreaId);
                ViewData["GenderId"] = new SelectList(_context.GenderClothes, "GenderId", "GenderName", product.GenderId);
                return View(product);
            }

            string uniqueFileName = UploadFile(product.ImageUrl); // Corrected method name
            if (ModelState.IsValid)
            {
                Product productI = new Product
                {
                    Title = product.Title,
                    Description = product.Description,
                    Price = product.Price,
                    Price50 = product.Price50,
                    ImageUrl = uniqueFileName,
                    CategoryId = product.CategoryId, // Added to set the CategoryId
                    AreaId = product.AreaId, // Added to set the AreaId
                    GenderId = product.GenderId // Added to set the GenderId
                };

                _context.Add(productI);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["AreaId"] = new SelectList(_context.FolkloreAreas, "AreaId", "AreaName", product.AreaId);
            ViewData["GenderId"] = new SelectList(_context.GenderClothes, "GenderId", "GenderName", product.GenderId);
            return View(product);
        }

        private string UploadFile(IFormFile file)
        {
            string uniqueFileName = null;
            if (file != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
        //ViewData["AreaId"] = new SelectList(_context.FolkloreAreas, "AreaId", "AreaName", product.AreaId);
        //ViewData["GenderId"] = new SelectList(_context.GenderClothes, "GenderId", "GenderName", product.GenderId);




        private string UploadFile(ProductImage product)
        {
            string uniqueFileName = null;
            if (product.ImageUrl != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageUrl.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                Console.WriteLine(filePath);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.ImageUrl.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["AreaId"] = new SelectList(_context.FolkloreAreas, "AreaId", "AreaName", product.AreaId);
            ViewData["GenderId"] = new SelectList(_context.GenderClothes, "GenderId", "GenderName", product.GenderId);
            ProductImage productImage = new ProductImage
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                Price50 = product.Price50,
                CategoryId = product.CategoryId,
                AreaId = product.AreaId,
                GenderId = product.GenderId
            };
            return View(productImage);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductImage product)
        {
            if (ModelState.IsValid)
            {
                Product? productUpdate = _context.Products.Find(product.Id);

                if (productUpdate == null || productUpdate.Id != product.Id)
                {
                    return NotFound();
                }

                productUpdate.Title = product.Title;
                productUpdate.Price = product.Price;
                productUpdate.Price50 = product.Price50;
                productUpdate.AreaId = product.AreaId;
                productUpdate.GenderId = product.GenderId;
                productUpdate.Description = product.Description;
                productUpdate.CategoryId = product.CategoryId;

                if (product.ImageUrl != null)
                {
                    DeleteImage(productUpdate.ImageUrl);
                    productUpdate.ImageUrl = UploadFile(product);
                }

                _context.Products.Update(productUpdate);
                _context.SaveChanges();


                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["AreaId"] = new SelectList(_context.FolkloreAreas, "AreaId", "AreaName", product.AreaId);
            ViewData["GenderId"] = new SelectList(_context.GenderClothes, "GenderId", "GenderName", product.GenderId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            DeleteImage(product.ImageUrl);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        private void DeleteImage(string name)
        {
            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", name);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}
