using System.Collections.Generic;
using System.Web.Mvc;
using Verkoop.Business;
using Verkoop.CapaDatos.DTO;

namespace Administrador.Controllers
{
    public class ClienteController : Controller
    {
        UsuarioBusiness _objUsuariosBusiness = new UsuarioBusiness();
        PerfilDatosUsuarioDTO _objUsuarios = new PerfilDatosUsuarioDTO();
        // GET: Cliente

        #region[ Vistas ]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Visualizar()
        {
            int.TryParse(Request["iIdUsuario"], out int iIdUsuario);//obtiene el iIdProducto
            _objUsuarios = _objUsuariosBusiness.ObtenerDatosDeUsuario(iIdUsuario);//obtiene el producto según si iIdProducto

            return View(_objUsuarios);
        }
        #endregion

        [HttpPost]
        public JsonResult ObtenerClientes()
        {
            List<BusquedaUsuarioPorEstadoDTO> lstUsuarios = new List<BusquedaUsuarioPorEstadoDTO>();
            lstUsuarios = _objUsuariosBusiness.ObtenerUsuarioClientePorEstado();
            return Json(new { data = lstUsuarios }, JsonRequestBehavior.AllowGet);
        }


    }
}