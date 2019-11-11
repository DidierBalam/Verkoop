using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cliente.Controllers
{
    public class CarritoComprasController : Controller
    {
        // GET: CarritoCompras
        public ActionResult Index()
        {
            return View();
        }
    }
}