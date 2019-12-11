using System.Web.Mvc;
using Verkoop.CapaDatos.DTO;
using Verkoop.Business;
using System.Collections.Generic;

namespace Cliente.Controllers
{
    public class ProductoController : Controller
    {
        ProductoBusiness ProductoBusiness = new ProductoBusiness();

        #region Vistas
        public ActionResult Principal()
        {
            List<VistaPreviaProductoClienteDTO> _lstProducto = ProductosRecientes();

            return View(_lstProducto);
        }

        public ActionResult DetallesProductos()
        {
            return View();
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método para obtener detalles de producto
        /// </summary>
        /// <param name="_idProducto">Recibe el id del producto</param>
        /// <returns>Retorna un objeto con los datos del producto</returns>
        public JsonResult VisualizarDetallesDeProductoCliente(int _iIdProducto)
        {
            DetallesProductoDTO _objProducto = ProductoBusiness.VisualizarDetallesDeProductoCliente(_iIdProducto);

            return Json(_objProducto);
        }

        public List<VistaPreviaProductoClienteDTO> ProductosRecientes()
        {
            List<VistaPreviaProductoClienteDTO> _lstProducto = ProductoBusiness.ObtenerProductosRecientes(20);

            return _lstProducto;
        }

        public List<VistaPreviaProductoClienteDTO> ProductosMasVendidos()
        {
            List<VistaPreviaProductoClienteDTO> _lstProducto = ProductoBusiness.ObtenerProductosRecientes(20);

            return _lstProducto;
        }
        #endregion
    }
}