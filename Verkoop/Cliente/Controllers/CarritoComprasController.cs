using System.Collections.Generic;
using System.Web.Mvc;
using Verkoop.Business;
using Verkoop.CapaDatos.DTO;
using System;
using PayPal.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace Cliente.Controllers
{
    public class CarritoComprasController : Controller
    {
        CarritoBusiness CarritoBusiness = new CarritoBusiness();
        TarjetaBusiness TarjetaBusiness = new TarjetaBusiness();


        /// <summary>
        /// Método para visualizar el carrito de compras
        /// </summary>
        /// <returns>Retorna la lista de los productos en carrito</returns>
        public ActionResult CarritoCompras()
        {
            int _iTotalCarrito = CarritoBusiness.ObtenerNumeroTotalProductosDeUsuario(Convert.ToInt32(Session["iIdUsuario"]));

            ViewBag.TotalCarrito = _iTotalCarrito;

            List<ProductoEnCarritoDTO> _lstProducto = ObtenerProductosDeUsuario(Convert.ToInt32(Session["iIdUsuario"]));

            return View(_lstProducto);
        }

        /// <summary>
        /// Método que se conecta a AgregarProductoCarrito() de CarritoBusiness
        /// </summary>
        /// <param name="_objProducto">Contiene el idProducto y idUsuario</param>
        /// <returns>Retorna el estado de la consulta y la cantidad de productos agregados al carrito del usuario</returns>
        [HttpPost]
        public JsonResult AgregarProductoCarrito(int _iIdProducto)
        {
            object _objResultado;

            if (Session["iIdUsuario"] != null)
            {
                _objResultado = CarritoBusiness.AgregarProductoCarrito(_iIdProducto, Convert.ToInt32(Session["iIdUsuario"]));

            }
            else
            {
                _objResultado = new
                {
                    _bEstadoOperacion = false,
                    _cMensaje = "¡Inicie sesión para poder agregar el producto al carrito!"
                };
            }

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

        public List<ProductoEnCarritoDTO> ObtenerProductosDeUsuario(int _iIdUsuario)
        {
            List<ProductoEnCarritoDTO> _lstResultado = CarritoBusiness.ObtenerProductosDeUsuario(_iIdUsuario);

            return _lstResultado;
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

                _objRespuestaGuardarTarjeta = TarjetaBusiness.GuardarTarjeta(1, _objPago.objTarjeta); //Guarda la tarjeta y recibe el estado de la operación y la tarjeta guardada.

                _objPago.objTarjeta.iIdTarjeta = Convert.ToInt32(_objRespuestaGuardarTarjeta.GetType().GetProperty("_objDatosTarjeta.iIdTarjeta").GetValue(_objRespuestaGuardarTarjeta));//Obtiene el id de la tarjeta y se lo asigna al objeto tarjeta que pertenece al objeto pago.

            }

            object _objRespuestaPago = CarritoBusiness.RealizarPago(1, _objPago);

            return Json(_objRespuestaPago);
        }

        /// <summary>
        /// Método que sirve para realizar un pago con Paypal.
        /// </summary>
        /// <param name="Cancel">Sirve para cancelar el pago.</param>
        /// <returns>Una vista de Éxito o Error.</returns>
        [HttpPost]
        public JsonResult PagoConPaypal(PagoPaypalDTO Productos, string Cancel = null)
        {
            bool _bEstadoOperacion = false;

            PaypalBusiness oPaypal = new PaypalBusiness();


            APIContext apiContext = PaypalConfiguracion.GetAPIContext();// Llamada al apiContext de Paypal.

            string _cPaypalRedirectUrl = null;

            try
            {
                // Un recurso que representa a un comprador que financia un método de pago como paypal.
                // ID del comprador será devuelta cuando el pago proceda o haga clic para pagar.
                string _payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(_payerId))
                {
                    // Esta sección se ejecutrará primero proque el ID del comprador no existe.

                    // Creación del pago.
                    // La url donde paypal envia de regreso datos.
                    string _baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                                "/Cliente";

                    // Aquí se genera el GUID para almacenar el ID del pago recibido en la sesión.
                    // Será usado en el proceso de pago.
                    var _guid = Convert.ToString((new Random()).Next(100000));

                    // _pagoCreado devuelve la url aprovada
                    // en el cuál el comprador es redirigido para proceder al proceso de pago de paypal.
                    var _pagoCreado = oPaypal.CrearPago(apiContext, _baseURI, Productos);


                    var _links = _pagoCreado.links.GetEnumerator(); // Obtiene los links devueltos de paypal.

                    while (_links.MoveNext())
                    {
                        Links lnk = _links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            _bEstadoOperacion = true;
                            _cPaypalRedirectUrl = lnk.href; // Guardar la url de paypal para redirigir al usuario a pagar.
                        }
                    }

                    Session.Add(_guid, _pagoCreado.id);// guardar el ID del pago en GUID.

                    return Json( new { _cPaypalRedirectUrl , _bEstadoOperacion});
                }
                else
                {

                    // Este bloque de código se ejecuta después de recibir todos los parámetros del pago.

                    var _guid = Request.Params["guid"];

                    var _pagoRealizado = oPaypal.EjecutarPago(apiContext, _payerId, Session[_guid] as string);


                    if (_pagoRealizado.state.ToLower() != "approved") // Si pagoRealizado falló entonces se redirige a la vista de error.
                    {
                        _bEstadoOperacion = false;
                        return Json(new { _bEstadoOperacion});
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage("Error" + ex.Message); // Imprimir el mensaje de error.
                _bEstadoOperacion = false;
                return Json(new { _bEstadoOperacion });
            }

            return Json(new { _cPaypalRedirectUrl, _bEstadoOperacion });

            

        }

        /// <summary>
        /// Vista que se carga después de haber completado con éxito la compra en paypal.
        /// </summary>
        /// <returns></returns>
        public ActionResult Agradecimiento()
        {
            return View();
        }
    }
}