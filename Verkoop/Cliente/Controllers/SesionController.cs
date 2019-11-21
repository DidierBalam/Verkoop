using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Verkoop.Business;

namespace Cliente.Controllers
{
    public class SesionController : Controller
    {
        // GET: Sesión
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Usuario()//Recibe datos vía get
        {
            return View();
        }
        [HttpPost]
        public ActionResult IniciarSesion(SesionBusiness _SesionBusiness)// Método sincrónico, recibe datos vía post
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            else
            {
                return View("Index");
            }
        }

    }
}