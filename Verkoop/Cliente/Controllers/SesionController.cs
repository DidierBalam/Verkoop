using System.Web.Mvc;
using Verkoop.Business;
using Verkoop.CapaDatos.DTO;
using Verkoop.CapaDatos;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace Cliente.Controllers
{
    public class SesionController : Controller
    {
        UsuarioBusiness UsuarioBusiness = new UsuarioBusiness();
        DireccionBusiness DireccionBusiness = new DireccionBusiness();
        SesionBusiness SesionBusiness = new SesionBusiness();


        /// <summary>
        /// Método para visualizar la vista de iniciar sesión
        /// </summary>
        /// <returns>Retorna una vista</returns>

        #region Vistas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegistroUsuario()
        {
            List<tblPais> _lstPais = DireccionBusiness.ObtenerTodosPaises();

            return View(_lstPais);
        }

        public ActionResult VerificarContrasenia()
        {
            return View();
        }

        #endregion

        #region Métodos
        /// <summary>
        /// Método que conecta al método RegistrarUsuario() del UsuarioBusiness
        /// </summary>
        /// <param name="_objDatos">Recibe los datos del usuario</param>
        /// <returns>Devuelve el estado de la operación y el mensaje de respuesta</returns>
        [HttpPost]
        public JsonResult RegistrarUsuario()
        {
            RegistrarUsuarioDTO _objDatosUsuario = JsonConvert.DeserializeObject<RegistrarUsuarioDTO>(Request["objDatosUsuario"]);

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

            string _cCorreo = JsonConvert.DeserializeObject<string>(Request["Correo"]); //se deserealiza el json y se convierte a un string
            string _cContrasenia = JsonConvert.DeserializeObject<string>(Request["Contrasenia"]);

            SesionBusiness _SesionBusiness = new SesionBusiness();

            if (_cCorreo != null && _cContrasenia != null) //se comprueba si los datos enviados son diferentes a nulo
            {
                object _objRespuesta = _SesionBusiness.IniciarSesion(_cCorreo, _cContrasenia); //se envían los datos al business
               
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
        /// MÉTODO QUE LLAMA AL MÉTODO CAMBIARCONTRASENIA() DE SESIONBUSINESS
        /// </summary>
        /// <param name="_cContraseniaActual">Recibe la contraseña actual</param>
        /// <param name="_cContraseniaNueva">Recibe la nueva contraseña </param>
        /// <returns>Regresa el estado de la operación y el mensaje de respuesta</returns>
        [HttpPost]
        public JsonResult CambiarContrasenia(string _cContraseniaActual, string _cContraseniaNueva)
        {
            object _objResultado = SesionBusiness.CambiarContrasenia(_cContraseniaActual, _cContraseniaNueva, 1);

            return Json(_objResultado);
        }

        /// <summary>
        /// Método para verificar la cuenta.
        /// </summary>
        /// <param name="_cCodigo">Recibe el código de verificación</param>
        /// <param name="_cCorreo">recibe el correo</param>
        /// <returns>retorna el estado de la operación y su mensaje</returns>
        [HttpPost]
        public JsonResult VerficarCuenta(string _cCodigo, string _cCorreo)
        {
            object _objRespuesta = SesionBusiness.VerificarCuenta(_cCodigo, _cCorreo);

            bool _bEstadoOperacion = Convert.ToBoolean(_objRespuesta.GetType().GetProperty("EstadoOperacion").GetValue(_objRespuesta));
            string _cMensaje = Convert.ToString(_objRespuesta.GetType().GetProperty("Mensaje").GetValue(_objRespuesta));
            
            
                if(_bEstadoOperacion) 
            {
                int _iVariableSesion =  Convert.ToInt32(_objRespuesta.GetType().GetProperty("VariableSesion").GetValue(_objRespuesta));
                Session["iIdUsuario"] = _iVariableSesion;
 
            }

            return Json(new { _bEstadoOperacion, _cMensaje});
        }

        /// <summary>
        /// Método para cerrar la sesión de usuario.
        /// </summary>
        /// <returns>Retorna a la vista catálogo</returns>
        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session["iIdUsuario"] = null;

            return RedirectToAction("Catalogo","Producto");
        }
        #endregion
    }
}