using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCalc.Models;

namespace WebCalc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact(long id, string name)
        {
            ViewBag.Id = id;
            ViewBag.Namesdasdqw = name;
            ViewBag.Message = $"Your contact page. {id} {name}";


            return View("OpDetails");
        }
        public ActionResult Empty(long id)
        {
            return Content($"id={id}");
        }

    }
}