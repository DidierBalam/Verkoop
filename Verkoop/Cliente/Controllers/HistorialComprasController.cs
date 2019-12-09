using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Verkoop.Business;
using Verkoop.CapaDatos.DTO;

namespace Cliente.Controllers
{
    public class HistorialComprasController : Controller
    {
        CompraBusiness CompraBusiness = new CompraBusiness();

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
            List<CompraDeClienteDTO> _lstResultado = CompraBusiness.ObtenerComprasDeCliente(_iIdUsuario);

            return Json(_lstResultado);
        }

        /// <summary>
        /// Método que sirve para descargar un documento PDF.
        /// </summary>
        /// <returns>Devuelve un True si se lográ descargar el pdf.</returns>
        [HttpPost]
        public JsonResult ImprimirTicketDeCompra()
        {

            int.TryParse(Request["iIdCompra"], out int _iIdCompra);

            byte[] _bPDF = CompraBusiness.ImprimirTicketDeCompra(2);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + "PDFfile.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(_bPDF);
            Response.End();

            return Json(true);

        }
       
    }
}