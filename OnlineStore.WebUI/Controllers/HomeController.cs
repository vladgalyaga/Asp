using OnlineStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository m_storeRepository;
        public HomeController(IStoreRepository storeRepository)
        {
            m_storeRepository = storeRepository;

        }

        public ActionResult GetCollection()
        {
            var t = m_storeRepository.GetCategories();
            return View(t);
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}