using System.Collections.Generic;
using System.Web.Mvc;
using Verkoop.Business;
using Verkoop.CapaDatos.DTO;
using Verkoop.CapaDatos;
using System.Reflection;
using System;

namespace Cliente.Controllers
{
    public class CarritoComprasController : Controller
    {
        CarritoBusiness CarritoBusiness = new CarritoBusiness();
        TarjetaBusiness TarjetaBusiness = new TarjetaBusiness();

        // GET: CarritoCompras
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método que se conecta a AgregarProductoCarrito() de CarritoBusiness
        /// </summary>
        /// <param name="_objProducto">Contiene el idProducto y idUsuario</param>
        /// <returns>Retorna el estado de la consulta y la cantidad de productos agregados al carrito del usuario</returns>
        [HttpPost]
        public JsonResult AgregarProductoCarrito(int _iIdProducto, int _iCantidad)
        {
            object _objResultado = CarritoBusiness.AgregarProductoCarrito(_iIdProducto, 1, _iCantidad);

            return Json(_objResultado);
        }

        /// <summary>
        /// Método que conecta a QuitarProductoCarrito() de CarritoBusiness
        /// </summary>
        /// <param name="_iIdCarrito">Contiene el idCarrito</param>
        /// <returns>Retorna el estado de la consulta</returns>
        [HttpPost]
        public JsonResult QuitarProductoCarrito(int _iIdCarrito)
        {
            object _objResultado = CarritoBusiness.QuitarProductoCarrito(_iIdCarrito);

            return Json(_objResultado);
        }

        /// <summary>
        /// Método que conecta a ObtenerProductosDeUsuario() de CarritoBusiness
        /// </summary>
        /// <param name="_iIdUsuario">Contiene el idUsuario</param>
        /// <returns>Retorna la lista de los productos</returns>
        [HttpPost]
        public JsonResult ObtenerProductosDeUsuario(int _iIdUsuario)
        {
            List<ProductoEnCarritoDTO> _lstResultado = CarritoBusiness.ObtenerProductosDeUsuario(_iIdUsuario);

            return Json(_lstResultado);
        }

        /// <summary>
        /// Método para realizar un pago.
        /// </summary>
        /// <param name="_objPago">REcibe el objeto con el id de la dirección usuario, los atributos de la tarjeta y los productos a comprar</param>
        /// <returns></returns>
        public JsonResult RealizarPago(RealizarPagoDTO _objPago)
        {
            object _objRespuestaGuardarTarjeta;

            if (Convert.ToInt32(_objPago.bGuardarTarjeta.GetType().GetProperty("iIdTarjeta").GetValue(_objPago)) == 0)
            {
                _objPago.bGuardarTarjeta.GetType().GetProperty("iIdUsuario").SetValue(0, 1);

                _objRespuestaGuardarTarjeta = TarjetaBusiness.GuardarTarjeta(_objPago.objTarjeta);

                _objPago.objTarjeta.iIdTarjeta = Convert.ToInt32(_objRespuestaGuardarTarjeta.GetType().GetProperty("_objDatosTarjeta.iIdTarjeta").GetValue(_objRespuestaGuardarTarjeta));

            }
            
            object _objRespuestaPago = CarritoBusiness.RealizarPago(1, _objPago);

            return Json(_objRespuestaPago);
        }
    }
}