using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace Administrador.Controllers
{
    public class SesionController : Controller
    {
        // GET: Sesión
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Usuario()
        {
            return View();
        }
        //public async Task<ActionResult> Usuarios (tblInicioSesion datos) //método sincrónico
        //{

        //}
    }
}