using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;
using Verkoop.Business;
using Verkoop.CapaDatos.DTO;

namespace Administrador.Controllers
{
    public class CategoriaController : Controller
    {
        object _objResultado;

        CategoriaBusiness CategoriaBusiness = new CategoriaBusiness();

        // GET: Categoría
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método para conectar con GuardarCategoria().
        /// </summary>
        /// <param name="_cNombreCategoria">Contiene el nombre de la categoría.</param>
        /// <returns>Retorna el estado de la operación y su mensaje de confirmación.</returns>
        [HttpPost]
        public JsonResult GuardarCategoria()
        {
            string _CNombre = JsonConvert.DeserializeObject<string>(Request["cNombre"]);
            _objResultado = CategoriaBusiness.GuardarCategoria(_CNombre);

            return Json(_objResultado);
        }

        /// <summary>
        /// Método que conecta con EliminarCategoria().
        /// </summary>
        /// <param name="_iIdCategoria">Contiene el id de la categoría.</param>
        /// <returns>Retorna el estado de la operación y su mensaje de confirmación.</returns>
        [HttpPost]
        public JsonResult EliminarCategoria(int _iIdCategoria)
        {
            _objResultado = CategoriaBusiness.EliminarCategoria(_iIdCategoria);

            return Json(_objResultado);
        }

        /// <summary>
        /// Método para conectar con ObtenerCategorias().
        /// </summary>
        /// <param name="_iIdUsuario">Contiene el id del usuario.</param>
        /// <returns>Retorna la lista de la consulta.</returns>
        [HttpPost]
        public JsonResult ObtenerCategorias()
        {
            List<CategoriaDTO> _lstCategorias = CategoriaBusiness.ObtenerCategorias();

            return Json(_lstCategorias);
        }
    }
}