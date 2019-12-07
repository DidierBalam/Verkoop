using System.Web.Mvc;
using Verkoop.CapaDatos.DTO;
using Verkoop.Business;
namespace Cliente.Controllers
{
    public class ProductoController : Controller
    {
        ProductoBusiness ProductoBusiness = new ProductoBusiness();

    
        public ActionResult Principal()
        {
            return View();
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
    }
}