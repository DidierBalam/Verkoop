using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Verkoop.Business;
using Verkoop.CapaDatos;
using Verkoop.CapaDatos.DTO;

namespace Administrador.Controllers
{
    public class ProductoController : Controller
    {
        ProductoBusiness _objProductosBusiness = new ProductoBusiness();


        #region[ Vistas ]
        public ActionResult Principal()
        {
            return View();
        }
        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Actualizar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Visualizar()
        {
            VisualizarDetallesProductoAdministradorDTO _objProductos = new VisualizarDetallesProductoAdministradorDTO();
            int.TryParse(Request["iIdProducto"], out int iIdProducto);//obtiene el iIdProducto
            _objProductos = _objProductosBusiness.VisualizarDetallesProductoAdministrador(iIdProducto);//obtiene el producto según su iIdProducto

            return View(_objProductos);
        }
        #endregion

        [HttpPost]
        public JsonResult Eliminar()
        {
            int.TryParse(Request["iIdProducto"], out int iIdProducto); //obtiene el iIdProducto
            bool.TryParse(Request["iEstatus"], out bool iEstatus);
            object _objResultado = _objProductosBusiness.CambiarEstadoProducto(iEstatus, iIdProducto);
            return Json(_objResultado);
        }

        [HttpPost]
        public JsonResult ObtenerProductos()
        {
            List<CatalogoProductoAdministradorDTO> lstProductos = new List<CatalogoProductoAdministradorDTO>();

            bool.TryParse(Request["iEstatus"], out bool iEstatus);
            lstProductos = _objProductosBusiness.ObtenerProductosPorEstado(iEstatus);

            return Json(new { data = lstProductos }, JsonRequestBehavior.AllowGet);
        }
    }
}