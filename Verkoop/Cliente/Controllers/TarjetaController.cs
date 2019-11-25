using System.Web.Mvc;
using Verkoop.Business;
using Verkoop.CapaDatos;


namespace Cliente.Controllers
{
    public class TarjetaController : Controller
    {
        object _objResultado;

        /// <summary>
        /// Instancia de la lógica de negocios TarjetaBusiness.
        /// </summary>
        TarjetaBusiness TarjetaBusiness = new TarjetaBusiness();

        /// <summary>
        /// Método que se conecta a GuardarTarjeta() de la clase TarjetaBusiness.
        /// </summary>
        /// <param name="objDatos">Recibe los datos de la tarjeta.</param>
        /// <returns> Retorna el mensaje de confirmación</returns.>
        [HttpPost]
        public JsonResult GuardarTarjeta(tblTarjeta _objTarjeta)
        {         
             _objResultado = TarjetaBusiness.GuardarTarjeta(_objTarjeta);///Se guardan las propiedades de la tarjeta en un objeto llamado _objResultado.
            
            return  Json(_objResultado);///Regresa un Json con los datos que este en la variable _objResultado.
        }

        /// <summary>
        /// Método que se conecta con GuardarPrimeraTarjeta de la clase TarjetaBusiness.
        /// </summary>
        /// <param name="_objTarjeta">Recibe los datos  de la tarjeta a guardar.</param>
        /// <returns>Retorna el estado de la operación y su mensaje de confirmación.</returns>
        [HttpPost]
        public JsonResult GuardarPrimeraTarjeta(tblTarjeta _objTarjeta)
        {            
             _objResultado = TarjetaBusiness.GuardarTarjeta(_objTarjeta);//Se guardan las propiedades de la tarjeta en un objeto llamado _objResultado.

            return Json(_objResultado);///Regresa un Json con los datos que este en la variable _objResultado.
        }

        /// <summary>
        /// Método que conecta con EliminarTarjeta() de la TarjetaBusiness.
        /// </summary>
        /// <param name="_iIdTarjeta">Recibe el id de la tarjeta.</param>
        /// <returns>Retorna el estado de la operación y su mensaje de confirmación.</returns>
        [HttpPost]
        public JsonResult EliminarTarjeta(int _iIdTarjeta)
        {
             _objResultado = TarjetaBusiness.EliminarTarjeta(_iIdTarjeta);

            return Json(_objResultado);
        }

        /// <summary>
        /// Método que se conecta con obtenerTodasTarjetas().
        /// </summary>
        /// <param name="_iIdUsuario">Contiene el id del usuario.</param>
        /// <returns>Retorna el resultado en forma de objeto.</returns>
        [HttpPost]
        public JsonResult ObtenerTodasTarjetas(int _iIdUsuario)
        {
           object _lstTarjetas = TarjetaBusiness.ObtenerTodasTarjetas(_iIdUsuario);

            return Json(_lstTarjetas);
        }



    }
}