using System.Web.Mvc;
using Verkoop.Business;
using Verkoop.CapaDatos;

namespace Administrador.Controllers
{
    public class SesionController : Controller
    {
       

        // GET: Sesion
        public ActionResult Index()
        {
            return View();
        }


    }
}