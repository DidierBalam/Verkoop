using System.Web.Mvc;

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