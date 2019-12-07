using System.Collections.Generic;
using System.Web.Mvc;
using Verkoop.Business;
using Verkoop.CapaDatos.DTO;

namespace Cliente.Controllers
{
    public class CarritoComprasController : Controller
    {
        CarritoBusiness ClaseBusiness = new CarritoBusiness();
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
            object _objResultado = ClaseBusiness.AgregarProductoCarrito(_iIdProducto,1, _iCantidad);

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
            object _objResultado = ClaseBusiness.QuitarProductoCarrito(_iIdCarrito);

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
            List<ProductoEnCarritoDTO> _lstResultado = ClaseBusiness.ObtenerProductosDeUsuario(_iIdUsuario);

            return Json(_lstResultado);
        }
    }
}