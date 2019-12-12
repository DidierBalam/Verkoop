using System.Web.Mvc;
using Verkoop.CapaDatos.DTO;
using Verkoop.Business;
using System.Collections.Generic;
using System;
namespace Cliente.Controllers
{
    public class ProductoController : Controller
    {
        int iProductosObtener = 3;

        ProductoBusiness ProductoBusiness = new ProductoBusiness();
        CategoriaBusiness CategoriaBusiness = new CategoriaBusiness();
 #region Vistas
        [HttpGet]
        public ActionResult Catalogo(string _cFiltro)
        {
            string cSesion = System.Web.HttpContext.Current.Session["iIdProducto"] as String;
            bool _bEstadoSesion;

            string _cFiltroUpper = _cFiltro != null ? _cFiltro.ToUpper() : "RECIENTES";
            //string _cFiltro = JsonConvert.DeserializeObject<string>(Request["Filtro"]==null ? "": Request["Filtro"]);

            List<VistaPreviaProductoClienteDTO> _lstProducto = new List<VistaPreviaProductoClienteDTO>();

            switch (_cFiltroUpper)
            {
                case "RECIENTES":
                    _lstProducto = ProductosRecientes(0);
                    break;
                case "MASVENDIDOS":
                    _lstProducto = ProductosMasComprados(0);
                    break;
                case null:
                    _lstProducto = ProductosRecientes(0);
                    break;
                default:
                    _lstProducto = ProductosPorCategoria(_cFiltro, 0);
                    break;
            }

            List<CategoriaDTO> _lstCategoria = CategoriaBusiness.ObtenerCategorias();

            if (cSesion != null) _bEstadoSesion = true;
            else _bEstadoSesion = false;

            ViewBag.Data = _lstCategoria;
            ViewBag.Select = _cFiltroUpper;
            ViewBag.Sesion = _bEstadoSesion;

            return View(_lstProducto);
        }

        [HttpPost]
        public ActionResult DetallesProductos()
        {
            int.TryParse(Request["iIdProducto"], out int _iIdProducto);

            DetallesProductoDTO _objProducto = VisualizarDetallesDeProductoCliente(_iIdProducto);

            return View(_objProducto);
        }
        #endregion

        #region Métodos


        /// <summary>
        /// MÉTODO PARA OBTENER DETALLES DE PRODUCTO.
        /// </summary>
        /// <param name="_idProducto">Recibe el id del producto</param>
        /// <returns>Retorna un objeto con los datos del producto</returns>
        public DetallesProductoDTO VisualizarDetallesDeProductoCliente(int _iIdProducto)
        {
            DetallesProductoDTO _objProducto = ProductoBusiness.VisualizarDetallesDeProductoCliente(_iIdProducto);

            return _objProducto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_iNumeroConsulta"></param>
        /// <returns></returns>
        public List<VistaPreviaProductoClienteDTO> ProductosRecientes(int _iNumeroConsulta)
        {
            List<VistaPreviaProductoClienteDTO> _lstProducto = ProductoBusiness.ObtenerProductosRecientes(_iNumeroConsulta, iProductosObtener);

            return _lstProducto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_iNumeroConsulta"></param>
        /// <returns></returns>
        public List<VistaPreviaProductoClienteDTO> ProductosMasComprados(int _iNumeroConsulta)
        {
            List<VistaPreviaProductoClienteDTO> _lstProducto = ProductoBusiness.ObtenerProductosMasComprados(_iNumeroConsulta, iProductosObtener);

            return _lstProducto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_cCategoria"></param>
        /// <param name="_iNumeroConsulta"></param>
        /// <returns></returns>
        public List<VistaPreviaProductoClienteDTO> ProductosPorCategoria(string _cCategoria, int _iNumeroConsulta)
        {
            List<VistaPreviaProductoClienteDTO> _lstProducto = ProductoBusiness.ObtenerProductosPorCategoria(_cCategoria, _iNumeroConsulta, iProductosObtener);

            return _lstProducto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_cNombre"></param>
        /// <param name="_iNumeroConsulta"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult BuscarProducto(string _cNombre, int _iNumeroConsulta)
        {
            List<VistaPreviaProductoClienteDTO> _lstProducto = ProductoBusiness.BuscarProducto(_cNombre, _iNumeroConsulta, iProductosObtener);

            return PartialView(_lstProducto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_cFiltro"></param>
        /// <param name="_iNumeroConsulta"></param>
        /// <returns></returns>
        public PartialViewResult VerMasProductos(string _cFiltro, int _iNumeroConsulta)
        {
            string _cFiltroUpper = _cFiltro != null ? _cFiltro.ToUpper() : "RECIENTES";

            List<VistaPreviaProductoClienteDTO> _lstProducto = new List<VistaPreviaProductoClienteDTO>();

            switch (_cFiltroUpper)
            {
                case "RECIENTES":
                    _lstProducto = ProductosRecientes(_iNumeroConsulta);
                    break;
                case "MASVENDIDOS":
                    _lstProducto = ProductosMasComprados(_iNumeroConsulta);
                    break;
                default:
                    _lstProducto = ProductosPorCategoria(_cFiltro, _iNumeroConsulta);
                    break;
            }


            return PartialView(_lstProducto);
        }
        #endregion
    }
}