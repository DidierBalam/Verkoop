using System.Web.Mvc;
using Verkoop.Business;
using Verkoop.CapaDatos.DTO;

namespace Cliente.Controllers
{
    public class SesionController : Controller
    {
        UsuarioBusiness UsuarioBusiness = new UsuarioBusiness();

        // GET: Sesion
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método que conecta al método RegistrarUsuario() del UsuarioBusiness
        /// </summary>
        /// <param name="_objDatos">Recibe los datos del usuario</param>
        /// <returns>Devuelve el estado de la operación y el mensaje de respuesta</returns>
        [HttpPost]
        public JsonResult RegistrarUsuario(RegistrarUsuarioDTO _objDatosUsuario)
        {
            object _objResultado = UsuarioBusiness.RegistrarUsuario(_objDatosUsuario);

            return Json(_objResultado);
        }
    }
}