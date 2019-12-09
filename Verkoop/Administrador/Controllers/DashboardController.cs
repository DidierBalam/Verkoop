using System.Web.Mvc;
using Verkoop.Business;

namespace Administrador.Controllers
{
    public class DashboardController : Controller
    {
        ProductoBusiness objProducto = new ProductoBusiness();
        UsuarioBusiness objUsuario = new UsuarioBusiness();
        CompraBusiness objCompra = new CompraBusiness();
        // GET: Dashboard
        public ActionResult Principal()
        {
            ViewData["TClientes"] = objUsuario.ObtenerNumeroTotalUsuariosClientes();
            ViewData["TProducto"] = objProducto.ObtenerNumeroTotalProductos();
            ViewData["TVentas"] = objCompra.ObtenerNumeroTotalCompras();
            ViewData["TIngresos"] = objCompra.ObtenerNumeroTotalIngresoVentas();
            return View();
        }
    }
}