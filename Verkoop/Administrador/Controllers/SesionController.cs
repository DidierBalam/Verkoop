using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using Verkoop.Business;


namespace Administrador.Controllers
{
    public class SesionController : Controller
    {
        // GET: Sesión

            /// <summary>
            /// Método para visualizar el apartado de iniciar sesión
            /// </summary>
            /// <returns>Retorna una vista</returns>
        public ActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// Método para iniciar sesión, sin datos nulos, comparando en el business, se ingresa a una variable sesión
        /// </summary>
        /// <param name="_cCorreo">Recibe el correo del usuario</param>
        /// <param name="_cContrasenia">Resibe la contraseña</param>
        /// <returns>Retorna el estado de la operación, un mensaje y la variabe session </returns>
        [HttpPost]
        public JsonResult IniciarSesion()
        {
            bool _bEstadoOperacion;
            string _cMensaje = "";

            string _cCorreo = JsonConvert.DeserializeObject<string>(Request["Correo"]); //se deserealiza el json y se convierte a un string
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
    }
}