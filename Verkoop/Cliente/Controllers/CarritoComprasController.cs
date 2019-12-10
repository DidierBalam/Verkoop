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
        public ActionResult CarritoCompras()
        {
            return View();
        }

        public ActionResult PagoConTarjeta()
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
        /// MÉTODO PARA REALIZAR UN PAGO.
        /// </summary>
        /// <param name="_objPago">Recibe el objeto con el id de la dirección usuario, los atributos de la tarjeta y los productos a comprar</param>
        /// <returns></returns>
        public JsonResult RealizarPago(RealizarPagoDTO _objPago)
        {
            object _objRespuestaGuardarTarjeta;         

            if (_objPago.objTarjeta.iIdTarjeta == 0) //Verifica si no se está recibiendo el id de alguna tarjeta seleccionada
            {
                _objPago.objTarjeta.iIdUsuario = 1/*Varible de sesión*/;//asigna el id del usuario al objeto tarjeta para guardar.

                _objRespuestaGuardarTarjeta = TarjetaBusiness.GuardarTarjeta(_objPago.objTarjeta); //Guarda la tarjeta y recibe el estado de la operación y la tarjeta guardada.

                _objPago.objTarjeta.iIdTarjeta = Convert.ToInt32(_objRespuestaGuardarTarjeta.GetType().GetProperty("_objDatosTarjeta.iIdTarjeta").GetValue(_objRespuestaGuardarTarjeta));//Obtiene el id de la tarjeta y se lo asigna al objeto tarjeta que pertenece al objeto pago.

            }
            
            object _objRespuestaPago = CarritoBusiness.RealizarPago(1, _objPago);

            return Json(_objRespuestaPago);
        }
    }
}