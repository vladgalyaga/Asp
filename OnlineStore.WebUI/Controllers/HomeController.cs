using OnlineStore.Domain;
using OnlineStore.Domain.Interfaces;
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

        private IRepository<Products, int> m_productRepository;

        private IRepository<Categories, int> m_categoryRepository;

        string s = Directory.GetCurrentDirectory();
        byte[] m_photo = System.IO.File.ReadAllBytes(@"C: \Users\zviad\OneDrive\Documents\Visual Studio 2015\Projects\OnlineStore\OnlineStore.WebUI\Content\Resurs\emblem.png");
        public HomeController(IUnitOfWork unitOfWork)
        {
            m_productRepository = unitOfWork.GetRepository<Products, int>();
            m_categoryRepository = unitOfWork.GetRepository<Categories, int>();
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
            Categories category =  m_categoryRepository.GetFirstOrDefaultAsync(x => x.CategoryName.ToLower() == categoryName.ToLower());

            return View(category.Products);
        }
        public async Task<FileContentResult> GetImageProductById(int productId)
        {
            // Products prod = m_storeRepository.GetProduct(productId);
            Products prod = await m_productRepository.FindByIdAsync(productId);
            if (prod.Photo != null)
            {
                return new FileContentResult(prod.Photo, "image/jpeg");
            }
            else
            {
                return new FileContentResult(m_photo, "image/jpeg");
            }
        }
        public async Task<ActionResult> Product(int productId)
        {
            var product = await m_productRepository.FindByIdAsync(productId);
            return View(product);
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