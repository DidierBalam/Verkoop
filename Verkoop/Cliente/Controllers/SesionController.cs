using System.Web.Mvc;
using Verkoop.Business;
using Verkoop.CapaDatos.DTO;
using System;
using Newtonsoft.Json;

namespace Cliente.Controllers
{
    public class SesionController : Controller
    {
        UsuarioBusiness UsuarioBusiness = new UsuarioBusiness();

        // GET: Sesión

        /// <summary>
        /// Método para visualizar la vista de iniciar sesión
        /// </summary>
        /// <returns>Retorna una vista</returns>
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

        /// <summary>
        /// Método para iniciar sesión, sin datos nulos, comparando en el business, se ingresa a una variable sesión
        /// </summary>
        /// <param name="_cCorreo">Recibe el correo del usuario</param>
        /// <param name="_cContrasenia">recibe la contraseña</param>
        /// <returns>Retorna el estado de la operación, un mensaje y la variable session</returns>
        [HttpPost]
        public JsonResult IniciarSesion()
        {
            bool _bEstadoOperacion;
            string _cMensaje = "";

            string _cCorreo = JsonConvert.DeserializeObject<string>(Request["Correo"]);
            string _cContrasenia = JsonConvert.DeserializeObject<string>(Request["Contrasenia"]);

            SesionBusiness _SesionBusiness = new SesionBusiness();

            if (_cCorreo != null && _cContrasenia != null) //se comprueba si los datos enviados son diferentes a nulo
            {
                object _objRespuesta = _SesionBusiness.IniciarSesion(_cCorreo, _cContrasenia); //se envían los datos al business
                bool p = Convert.ToBoolean(_objRespuesta.GetType().GetProperty("EstadoOperacion").GetValue(_objRespuesta));
                if (Convert.ToBoolean(_objRespuesta.GetType().GetProperty("EstadoOperacion").GetValue(_objRespuesta))) //se compara si  es diferente a nulo
                {
                    Session["iIdUsuario"] = int.Parse(Convert.ToString(_objRespuesta.GetType().GetProperty("VariableSesion").GetValue(_objRespuesta)));// Se ingresa a una variable Sesión
                    _bEstadoOperacion = true;

                }
                else
                {
                    _bEstadoOperacion = false;
                    _cMensaje = Convert.ToString(_objRespuesta.GetType().GetProperty("Mensaje").GetValue(_objRespuesta));
                }
            }
            else
            {
                _bEstadoOperacion = false;
                _cMensaje = "El correo o la contraseña están vacías";
            }

            return Json(new { _bEstadoOperacion, _cMensaje });
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