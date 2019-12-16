using System.Web.Mvc;
using Verkoop.CapaDatos.DTO;
using Verkoop.Business;
using System.Collections.Generic;
using System;
using Verkoop.CapaDatos;
using Bogus;

namespace Cliente.Controllers
{
    public class ProductoController : Controller
    {
        int iProductosObtener = 20;

        ProductoBusiness ProductoBusiness = new ProductoBusiness();
        CategoriaBusiness CategoriaBusiness = new CategoriaBusiness();
        CarritoBusiness CarritoBusiness = new CarritoBusiness();

        #region Vistas
        [HttpGet]
        public ActionResult Catalogo(string _cFiltro)
        {

            int _iTotalCarrito = 0;
            string _cFiltroUpper = _cFiltro != null ? _cFiltro.ToUpper() : "RECIENTES";
            

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

            if (Session["iIdUsuario"] != null)
            {
                _iTotalCarrito = CarritoBusiness.ObtenerNumeroTotalProductosDeUsuario(Convert.ToInt32(Session["iIdUsuario"]));
            }

            ViewBag.Data = _lstCategoria;
            ViewBag.Select = _cFiltroUpper;
            ViewBag.TotalCarrito = _iTotalCarrito;

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
            DetallesProductoDTO _objProducto = ProductoBusiness.VisualizarDetallesDeProductoCliente(_iIdProducto, Convert.ToInt32(Session["iIdUsuario"]));

            return _objProducto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_iNumeroConsulta"></param>
        /// <returns></returns>
        public List<VistaPreviaProductoClienteDTO> ProductosRecientes(int _iNumeroConsulta)
        {
            List<VistaPreviaProductoClienteDTO> _lstProducto = ProductoBusiness.ObtenerProductosRecientes(_iNumeroConsulta, iProductosObtener, Convert.ToInt32(Session["iIdUsuario"]));

            return _lstProducto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_iNumeroConsulta"></param>
        /// <returns></returns>
        public List<VistaPreviaProductoClienteDTO> ProductosMasComprados(int _iNumeroConsulta)
        {
            List<VistaPreviaProductoClienteDTO> _lstProducto = ProductoBusiness.ObtenerProductosMasComprados(_iNumeroConsulta, iProductosObtener, Convert.ToInt32(Session["iIdUsuario"]));

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
            List<VistaPreviaProductoClienteDTO> _lstProducto = ProductoBusiness.ObtenerProductosPorCategoria(_cCategoria, _iNumeroConsulta, iProductosObtener, Convert.ToInt32(Session["iIdUsuario"]));

            return _lstProducto;
        }

        /// <summary>
        /// MÉTODO PARA BUSCAR PRODUCTOS.
        /// </summary>
        /// <param name="_cNombre">Recibe el nombre del producto</param>
        /// <param name="_iNumeroConsulta">Recibe el número de la consulta</param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult BuscarProducto(string _cNombre, int _iNumeroConsulta)
        {
            List<VistaPreviaProductoClienteDTO> _lstProducto = ProductoBusiness.BuscarProducto(_cNombre, _iNumeroConsulta, iProductosObtener, Convert.ToInt32(Session["iIdUsuario"]));

            return PartialView(_lstProducto);
        }

        /// <summary>
        /// MÉTODO PARA VER MÁS PRODUCTOS.
        /// </summary>
        /// <param name="_cFiltro">Recibe el filtro</param>
        /// <param name="_iNumeroConsulta">Recibe el número de la consulta</param>
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

     
        public void GenerarDatos(int _iIdCategoria)
        {
            var ListAdministradores = new Faker<tblCat_Producto>()

                .RuleFor(o => o.cNombre, f => f.Commerce.ProductName())
                .RuleFor(o => o.iCantidad, f => f.Random.Number(0, 100))
                .RuleFor(o => o.iIdCategoria, f => _iIdCategoria)
                .RuleFor(o => o.cImagen, f => f.Image.PicsumUrl())
                .RuleFor(o => o.dPrecio, f => f.Random.Number(1, 3000))
                .RuleFor(o => o.lEstatus, f => f.Random.Bool(1))
                .RuleFor(o => o.dtFechaAlta, f => f.Date.Recent())
                .RuleFor(o => o.cDescripcion, f => f.Lorem.Word());



            using (VerkoopDBEntities ctx = new VerkoopDBEntities())
            {

                var user = ListAdministradores.Generate(100);

               

                ctx.tblCat_Producto.AddRange(user);

                ctx.SaveChanges();
            }
        }
        #endregion
    }
}