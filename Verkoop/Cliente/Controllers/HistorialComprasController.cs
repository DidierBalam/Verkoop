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

        #region Vistas
        public ActionResult ComprasRealizadas()
        {
            List<CompraDeClienteDTO> _lstResultado = ObtenerComprasDeCliente();

            return View(_lstResultado);
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que conecta a ObtenerComprasDeCliente() de CompraBusiness
        /// </summary>
        /// <param name="_iIdUsuario">Contiene el idUsuario</param>
        /// <returns>Retorna la lista de las compras</returns>
        public List<CompraDeClienteDTO> ObtenerComprasDeCliente()
        {
            List<CompraDeClienteDTO> _lstResultado = CompraBusiness.ObtenerComprasDeCliente(5/*Variable sesión*/);

            return _lstResultado;
        }

        /// <summary>
        /// Método que sirve para descargar un documento PDF.
        /// </summary>
        /// <returns>Devuelve un True si se lográ descargar el pdf.</returns>
        [HttpGet]
        public JsonResult ImprimirTicketDeCompra(int iIdCompra)
        {
            //int.TryParse(/*Request*/"iIdCompra", out int _iIdCompra);

            byte[] _bPDF = CompraBusiness.ImprimirTicketDeCompra(iIdCompra);

            Response.Clear();
            Response.ContentType = "Application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + "VerkoopCompra.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(_bPDF);
            Response.End();

            return Json(true);
        }
        #endregion


    }
}