using MagicOFBulgarian.Data;
using MagicOFBulgarian.Data.Domain;
using MagicOFBulgarian.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;


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
        public ActionResult Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var cart = _context.ShoppingCarts.Where(u=> u.CustomerId == userId).FirstOrDefault();
            if (userId==null)
            {
                RedirectToRoute("~/Customer/Home");
            }
            var cp = _context.CartProducts.Where(u=>u.CartId.Equals(cart.Id)).AsEnumerable();
            var ids = (from prod in cp select prod.ProductId).AsEnumerable();
            if (ids == null) RedirectToAction("sskdad");
            
            var products = new Dictionary<Product, CartProduct>();
            if(products.Count!=0)
            foreach(var id in ids)
            {
                var product = _context.Products.Where(x=>x.Id.Equals(id)).FirstOrDefault();
                var cartp = cp.Where(x=>x.ProductId.Equals(id)).FirstOrDefault();
                products.Add(product,cartp);
            }
            return View(products);
        }

        

        // GET: CartController/Create
        public  ActionResult Create(int? product ,int? quantity)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var cart = _context.ShoppingCarts.Where(u => u.CustomerId == userId).FirstOrDefault();
            if(userId == default||cart==null)
            {
                RedirectToRoute("~/Customer/Home");

            }
            CartProduct prod = _context.CartProducts
                .Where(x => x.ProductId.Equals(product) && x.CartId.Equals(cart.Id))
                .FirstOrDefault();
            if(prod is null)
            {
                CartProduct newProd = new CartProduct {
                    Cart = cart,
                    CartId = cart.Id,
                    ProductId = (int)product,
                    Product = _context.Products.Where(x => x.Id.Equals(product)).FirstOrDefault()

                
                };
            }
            else
            {
                prod.Quantity += (int)quantity;
            }
             _context.SaveChanges();

            return View();
        }

        // POST: CartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
