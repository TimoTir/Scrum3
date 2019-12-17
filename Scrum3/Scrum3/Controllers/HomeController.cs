using Scrum3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Scrum3.Controllers
{
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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult Hae()
        //{
        //    var x = "";

        //    ScrumEntities1 db = new ScrumEntities1();
        //    var a = from b in db.Kurssit
        //            select b;
        //    foreach (var s in a)
        //    {

        //        x += s.Kurssi.ToString() + s.Laajuus.ToString();

        //    }
        //        ViewBag.Message = x; 




        //    return View();
        
    }
}