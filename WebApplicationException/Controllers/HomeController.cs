using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplicationException.App_Start;

namespace WebApplicationException.Controllers
{
    public class HomeController : Controller
    {
        [HandleExceptionError]
        public ActionResult Index()
        {
            throw new Exception("Falla Error Fail");
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
    }
}