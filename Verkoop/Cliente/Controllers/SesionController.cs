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

        public ActionResult RegistroUsuario()
        {
            return View();
        }

        public ActionResult Contrasenia()
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

        /// <summary>
        /// Método que llama al método CambiarContrasenia() de SesionBusiness
        /// </summary>
        /// <param name="_cContraseniaActual">Recibe la contraseña actual</param>
        /// <param name="_cContraseniaNueva">Recibe la nueva contraseña </param>
        /// <returns>Regresa el estado de la operación y el mensaje de respuesta</returns>
        [HttpPost]
        public JsonResult CambiarContrasenia(string _cContraseniaActual, string _cContraseniaNueva)
        {
            SesionBusiness SesionBusiness = new SesionBusiness();

            object _objResultado = SesionBusiness.CambiarContrasenia(_cContraseniaActual, _cContraseniaNueva, 1);

            return Json(_objResultado);
        }
    }
}