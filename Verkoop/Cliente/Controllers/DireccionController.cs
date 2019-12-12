using Verkoop.Business;
using System.Web.Mvc;
using System.Collections.Generic;
using Verkoop.CapaDatos;

namespace Cliente.Controllers
{
    public class DireccionController : Controller
    {
        object _objResultado;

        /// <summary>
        /// Instancia de la lógica de negocios de DireccionBusiness.
        /// </summary>
        readonly DireccionBusiness DireccionBusiness = new DireccionBusiness();


        /// <summary>
        /// Método que se conecta con ActualizarDireccion() de la clase de DireccionBussines.
        /// </summary>
        /// <param name="_objDireccion">Recibe los datos de la dirección en forma de objeto.</param>
        /// <returns>Retorna el estado de la operación y su mensaje de confirmación.</returns>
        [HttpPost]
        public JsonResult ActualizarDireccion (tblDireccion _objDireccion)
        {
            _objResultado =DireccionBusiness.ActualizarDireccion(_objDireccion);///Se guardan las propiedades de la tarjeta en un objeto llamado _objResultado.

            return Json(_objResultado);///Regresa un Json con los datos que este en la variable _objResultado.
        }

        
        /// <summary>
        /// Método para conectar con GuardarDireccion()
        /// </summary>
        /// <param name="_objDireccion"> Recibe los datos de la dirección en forma de objeto.</param>
        /// <returns>Retorna el estado de la operación, su mensaje de confirmación y devuelve el </returns>
        [HttpPost]
        public JsonResult GuardarDireccion(tblDireccion  _objDireccion)
        {
            _objResultado = DireccionBusiness.GuardarDireccion(_objDireccion);///Se guardan las propiedades de la tarjeta en un objeto llamado _objResultado.

            return Json(_objResultado);///Regresa un Json con los datos que este en la variable _objResultado.
        }


        /// <summary>
        /// Método para conectar con ObtenerDireccionesDeUsuario(). 
        /// </summary>
        /// <param name="_iIdUsuario">Contiene el id del usuario.</param>
        /// <returns>Devuelve el listado de direcciones.</returns>
        [HttpPost]
        public JsonResult ObtenerDireccionesDeUsuario(int _iIdUsuario)
        {
             object _lstDirecciones = DireccionBusiness.ObtenerDireccionesDeUsuario(_iIdUsuario);

            return Json(_lstDirecciones);
        }

        /// <summary>
        /// Método que conecta con EliminarDireccion().
        /// </summary>
        /// <param name="_iIdUsuario">Contiene el id del usuario.</param>
        /// <returns>Retorna el estado de la operación y su mensaje de confirmación.</returns>
        [HttpPost]
        public JsonResult EliminarDireccion(int _iIdDireccion)
        {
            _objResultado = DireccionBusiness.EliminarDireccion(_iIdDireccion);

            return Json(_objResultado);
        }

        /// <summary>
        /// MÉTODO PARA OBTENER LOS ESTADOS DE UN PAÍS.
        /// </summary>
        /// <param name="_iIdPais">Recibe el Id del país</param>
        /// <returns>Retorna una lista con los estados</returns>
        [HttpPost]
        public JsonResult ObtenerEstadosPorPais(int _iIdPais)
        {
            List<tblEstado> _lstEstado = DireccionBusiness.ObtenerEstadosPorPais(_iIdPais);

            return Json(_lstEstado);
        }

        /// <summary>
        /// MÉTODO PARA OBTENER MUNICIPIOS DE UN ESTADO.
        /// </summary>
        /// <param name="_iIdEstado">Recibe el Id del estado</param>
        /// <returns>Retorna una lista con los municipios</returns>
        [HttpPost]
        public JsonResult ObtenerMunicipiosPorEstado(int _iIdEstado)
        {
            List<tblMunicipio> _lstMunicipio = DireccionBusiness.ObtenerMunicipiosPorEstado(_iIdEstado);

            return Json(_lstMunicipio);
        }
    }
}