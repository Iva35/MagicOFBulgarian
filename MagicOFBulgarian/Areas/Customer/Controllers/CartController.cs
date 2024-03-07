using MagicOFBulgarian.Data;
using MagicOFBulgarian.Data.Domain;
using MagicOFBulgarian.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Security.Claims;


namespace MagicOFBulgarian.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        // GET: CartController
        private readonly ApplicationDbContext _context;
        private readonly UserManager<CustomerUser> _userManager;
        

        public CartController(ApplicationDbContext context, UserManager<CustomerUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<ActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var cart = _context.ShoppingCarts.Where(u=> u.CustomerId == userId).FirstOrDefault();
            if (userId==null)
            {
                RedirectToRoute("~/Customer/Home");
            }
            var cp = _context.CartProducts.Where(u=>u.CartId.Equals(cart.Id)).AsEnumerable();
            var ids = (from prod in cp select prod.ProductId).ToHashSet();
            if (ids.Count == 0) return NotFound();            
            var products = new Dictionary<Product, CartProduct>();
            if (ids.Count != 0)
            {
                foreach(var id in ids)
                {
                    var product = _context.Products.Where(x=>x.Id.Equals(id)).FirstOrDefault();
                    var cartp = cp.Where(x=>x.ProductId.Equals(id)).FirstOrDefault();
                    products.Add(product,cartp);
                }

            } 
            return View(products);
        }

        

        // GET: CartController/Create
        public  ActionResult Create()
        {
          

            return RedirectToAction(nameof(Index));
        }

        // POST: CartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int product, IFormCollection form)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            Console.WriteLine(userId);
            var cart = _context.ShoppingCarts.Where(u => u.CustomerId == userId).FirstOrDefault();
            if (userId is null || cart is null)
            {
                return RedirectToAction(nameof(Index));
            }

            int? quantity = int.TryParse(form["quantity"], out int parsedQuantity) ? parsedQuantity : null;
            if (quantity is null)
            {
                // Обработка на сценария, когато quantity е null
                return RedirectToAction(nameof(Index));
            }

            var prod = await _context.CartProducts.Where(x => x.ProductId == product && x.CartId == cart.Id).
                FirstOrDefaultAsync();
            var productToAdd = _context.Products.FirstOrDefault(x => x.Id == product);
            if (prod is null)
            {
                var newProd = new CartProduct
                {
                    Cart = cart,
                    CartId = cart.Id,
                    ProductId = product,
                    Product = productToAdd,
                    Quantity=(int)quantity,
                    Price = productToAdd.Price*(int)quantity,
                    Included = true
                };
                cart.Price += newProd.Price;
                _context.CartProducts.Add(newProd);
            }
            else
            {
                prod.Quantity += (int)quantity;
                prod.Price = productToAdd.Price * prod.Quantity;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: CartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
