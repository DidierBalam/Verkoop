﻿using System.Collections.Generic;
using System.Web.Mvc;
using Verkoop.Business;
using Verkoop.CapaDatos.DTO;
using Verkoop.CapaDatos;
using System;
using PayPal.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

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

        /// <summary>
        /// Método que sirve para realizar un pago con Paypal.
        /// </summary>
        /// <param name="Cancel">Sirve para cancelar el pago.</param>
        /// <returns>Una vista de Éxito o Error.</returns>
        [HttpGet]
        public ActionResult PagoConPaypal(string Cancel = null)
        {
            //_objPago = JsonConvert.DeserializeObject<RealizarPagoDTO>(Request["_objPago"]);

            PaypalBusiness oPaypal = new PaypalBusiness();

            // Llamada al apiContext de Paypal.
            APIContext apiContext = PaypalConfiguracion.GetAPIContext();

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
                                "/Cliente/CarritoCompras/PagoConPaypal?";

                    // Aquí se genera el GUID para almacenar el ID del pago recibido en la sesión.
                    // Será usado en el proceso de pago.
                    var _guid = Convert.ToString((new Random()).Next(100000));
                    
                    // _pagoCreado devuelve la url aprovada
                    // en el cuál el comprador es redirigido para proceder al proceso de pago de paypal.
                    var _pagoCreado = oPaypal.CrearPago(apiContext, _baseURI + "guid=" + _guid);

                    
                    var _links = _pagoCreado.links.GetEnumerator(); // Obtiene los links devueltos de paypal.

                    string _cPaypalRedirectUrl = null;

                    while (_links.MoveNext())
                    {
                        Links lnk = _links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            
                            _cPaypalRedirectUrl = lnk.href; // Guardar la url de paypal para redirigir al usuario a pagar.
                        }
                    }

                    Session.Add(_guid, _pagoCreado.id);// guardar el ID del pago en GUID

                    return Redirect(_cPaypalRedirectUrl);
                }
                else
                {

                    // Este bloque de código se ejecuta después de recibir todos los parámetros del pago

                    var _guid = Request.Params["guid"];

                    var _pagoRealizado = oPaypal.EjecutarPago(apiContext, _payerId, Session[_guid] as string);

                    
                    if (_pagoRealizado.state.ToLower() != "approved") // Si pagoRealizado falló entonces se redirige a la vista de error.
                    {

                        return View("FailureView");

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage("Error" + ex.Message); // Imprimir el mensaje de error.

                return View("FailureView"); // Si ocurre algun error, enviar mensaje de error

            }

            // En caso de éxito, se muestra un mensaje de aprovado al user.
            return View("SuccessView");

        }
    }
}