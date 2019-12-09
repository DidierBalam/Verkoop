using System.Web.Mvc;
using Verkoop.CapaDatos.DTO;
using Verkoop.Business;
using System.Collections.Generic;

namespace Cliente.Controllers
{
    public class ProductoController : Controller
    {
        ProductoBusiness ProductoBusiness = new ProductoBusiness();

    
        public ActionResult Principal(List<VistaPreviaProductoClienteDTO> _lstProducto)
        {          

            return View(_lstProducto);
        }

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

        [HttpGet]
        public ActionResult ProductosRecientes()
        {
            List<VistaPreviaProductoClienteDTO> _lstProducto = ProductoBusiness.ObtenerProductosRecientes(20);

           return RedirectToAction("Principal",_lstProducto);
        }

        [HttpGet]
        public List<VistaPreviaProductoClienteDTO> ProductosMasVendidos()
        {
            List<VistaPreviaProductoClienteDTO> _lstProducto = ProductoBusiness.ObtenerProductosRecientes(20);

            return _lstProducto;
        }
    }
}