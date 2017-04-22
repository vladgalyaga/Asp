using OnlineStore.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
            var category = m_storeRepository.GetCategoryByName(categoryName);

                return View(category.Products);
        }
        public  FileContentResult GetImageProductById(int productId)
        {
            Products prod =  m_storeRepository.GetProduct(productId);
            if (prod.Photo != null)
            {
                return new FileContentResult(prod.Photo, "image/jpeg");
            }
            else
            {
                return new FileContentResult(m_photo, "image/jpeg");
            }
        }
        public ActionResult Product(int productId)
        {
            return View(m_storeRepository.GetProduct(productId));
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