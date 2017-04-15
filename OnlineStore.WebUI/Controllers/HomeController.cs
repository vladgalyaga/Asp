using OnlineStore.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        
        private IStoreRepository m_storeRepository;

        string s = Directory.GetCurrentDirectory();
        byte[] m_photo = System.IO.File.ReadAllBytes(@"C: \Users\zviad\OneDrive\Documents\Visual Studio 2015\Projects\OnlineStore\OnlineStore.WebUI\Content\Resurs\emblem.png");
        public HomeController(IStoreRepository storeRepository)
        {
            m_storeRepository = storeRepository;

        }

        //public ActionResult GetCollection()
        //{
        //    var t = m_storeRepository.GetAllCategories();
        //    return View(t);
        //}
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("Products", "Home", new { categoryName = "Confections" });
        }

        public ActionResult Products(string categoryName = "Confections")
        {
            try
            {
                var category = m_storeRepository.GetCategoryByName(categoryName);
                var products = category.Products;
                return View(products);
            }
            catch { }

            return View();

        }
        public FileContentResult GetImage(int productId)
        {
            Products prod = m_storeRepository.GetProduct(productId);
            if (prod.Photo != null)
            {
                return new FileContentResult(prod.Photo, "image/jpeg");
            }
            else
            {
                return new FileContentResult(m_photo, "image/jpeg");
            }
        }
        //public PartialViewResult GetCategories()
        //{
        //    var categories = new List<Categories>();
        //    categories.Add(m_storeRepository.GetCategoryByName("Confections"));
        //    categories.Add(m_storeRepository.GetCategoryByName("Beverages"));

        //    return PartialView("_CategoriesView", categories);

        //}

        //public ActionResult LoadCategory(string name)
        //{
        //    return null;
        //}
    }
}