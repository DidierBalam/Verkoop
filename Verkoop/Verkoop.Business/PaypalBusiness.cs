using PayPal.Api;
using System.Collections.Generic;
using Verkoop.CapaDatos.DTO;

namespace Verkoop.Business
{
    /// <summary>
    /// Contiene los métodos principales de Paypal.
    /// </summary>
    public class PaypalBusiness
    {
        private Payment payment;

        /// <summary>
        /// Método que ejecuta el pago de paypal.
        /// </summary>
        /// <param name="apiContext">Contexto de paypal.</param>
        /// <param name="payerId">Identificador del comprador.</param>
        /// <param name="paymentId">Identificador del pago.</param>
        /// <returns>Devuelve el pago.</returns>
        public Payment EjecutarPago(APIContext apiContext, string payerId, string paymentId)
        {

            var paymentExecution = new PaymentExecution() { payer_id = payerId };

            this.payment = new Payment() { id = paymentId };

            return this.payment.Execute(apiContext, paymentExecution);

        }

        /// <summary>
        /// Método que crea el pago final de paypal.
        /// </summary>
        /// <param name="apiContext"></param>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        public Payment CrearPago(APIContext apiContext, string redirectUrl, PagoPaypalDTO Productos)
        {
            CarritoBusiness oCarrito = new CarritoBusiness();

            PagoPaypalDTO lstProductos = oCarrito.ObtenerProductosCarrito(Productos);

            //var productosEnCarrito = oCarrito.ObtenerProductosCarrito();

            var itemList = new ItemList() { items = new List<Item>() }; // Creación de una lista de Productos.

            // Añadir productos a la lista de productos.
            foreach (var item in lstProductos.lstProducto)
            {
                itemList.items.Add(new Item()
                {
                    name = item.cNombre,
                    currency = "MXN",
                    price = item.dPrecio.ToString(),
                    quantity = item.iCantidad.ToString(),
                });
            }

            var payer = new Payer() { payment_method = "paypal" }; // Creación del objeto comprador y también el método.

            // Configuración de las URLs.
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl + "/CarritoCompras/Agradecimiento"
            };

            // Impuesto, Envio, subTotal
            var details = new Details()
            {
                subtotal = lstProductos.dPrecioTotal.ToString()
            };

            //Cantidad final con detalles.
            var amount = new Amount()
            {
                currency = "MXN",
                total = lstProductos.dPrecioTotal.ToString(),//"1", // El total debe ser igual a la suma del impuesto, envío y subtotal.
                details = details
            };

            var transactionList = new List<Transaction>(); // Creación de la lista de transacción.

            // Añadiendo la descripcion acerca de la transacción.
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                amount = amount,
                item_list = itemList
            });

            // Creación de la configuración del pago.
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Creación del pago usando la Api de Paypal.
            return this.payment.Create(apiContext);

        }

    }
}
