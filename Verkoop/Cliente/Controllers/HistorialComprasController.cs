using System.Collections.Generic;
using System.Web.Mvc;
using Verkoop.Business;
using Verkoop.CapaDatos.DTO;

namespace Cliente.Controllers
{
    public class HistorialComprasController : Controller
    {
        CompraBusiness ClaseBusiness = new CompraBusiness();
        // GET: HistorialCompras
        public ActionResult ComprasRealizadas()
        {
            return View();
        }
        /// <summary>
        /// Método que conecta a ObtenerComprasDeCliente() de CompraBusiness
        /// </summary>
        /// <param name="_iIdUsuario">Contiene el idUsuario</param>
        /// <returns>Retorna la lista de las compras</returns>
        [HttpPost]
        public JsonResult ObtenerComprasDeCliente(int _iIdUsuario)
        {
            List<CompraDeClienteDTO> _lstResultado = ClaseBusiness.ObtenerComprasDeCliente(_iIdUsuario);
            return Json(_lstResultado);
        }
    }
}