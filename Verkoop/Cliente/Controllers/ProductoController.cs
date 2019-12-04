using System.Web.Mvc;
using Verkoop.CapaDatos.DTO;
using Verkoop.Business;
using System.Collections.Generic;

namespace Cliente.Controllers
{
    public class ProductoController : Controller
    {
        ProductoBusiness ProductoBusiness = new ProductoBusiness();


        public ActionResult Index()
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

        /// <summary>
        /// Método para obtener los productos recien agregados
        /// </summary>
        /// <param name="_iNumeroConsulta ">Recibe el número de consultas realizadas</param>
        /// <returns>Retorna una lista de los productos</returns>
        [HttpPost]
        public JsonResult ObtenerProductosRecientes(int _iNumeroConsulta)
        {
            List<VistaPreviaProductoClienteDTO> _lstProductos = ProductoBusiness.ObtenerProductosRecientes(_iNumeroConsulta);

            return Json(_lstProductos);
        }


        /// <summary>
        /// Método para obtener productos por categoría
        /// </summary>
        /// <param name="_iIdCategoria">Recibe el id de la categoría</param>
        /// <param name="_iNumeroConsulta ">Recibe el número de consultas realizadas</param>
        /// <returns>Retorna una lista de los productos</returns>
        [HttpPost]
        public JsonResult ObtenerProductosPorCategoria(int _iIdCategoria, int _iNumeroConsulta)
        {
            List<VistaPreviaProductoClienteDTO> _lstProductos = ProductoBusiness.ObtenerProductosPorCategoria(_iIdCategoria, _iNumeroConsulta);

            return Json(_lstProductos);
        }

        /// <summary>
        /// Método para obtener los productos más comprados
        /// </summary>
        /// <param name="_iNumeroConsulta ">Recibe el número de consultas realizadas</param>
        /// <returns>Retorna una lista de los productos</returns>
        [HttpPost]
        public JsonResult ObtenerProductosMasComprados(int _iNumeroConsulta)
        {
            List<VistaPreviaProductoClienteDTO> _lstProductos = ProductoBusiness.ObtenerProductosMasComprados(_iNumeroConsulta);

            return Json(_lstProductos);
        }

        /// <summary>
        /// Método para buscar productos por nombre
        /// </summary>
        /// <param name="_cNombre">Recibe el nombre del producto</param>
        /// <param name="_iNumeroConsulta">Recibe el núemero de consultas realzadas</param>
        /// <returns>Retorna una lista con los productos</returns>
        public JsonResult BuscarProducto(string _cNombre, int _iNumeroConsulta)
        {
            List<VistaPreviaProductoClienteDTO> _lstProducto = ProductoBusiness.BuscarProducto(_cNombre,_iNumeroConsulta);

            return Json(_lstProducto);
        }
    }
}