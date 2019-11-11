using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Verkoop.Business;

namespace Administrador.Controllers
{
    public class CategoriaController : Controller
    {
        CategoriaBusiness ClaseBusiness = new CategoriaBusiness();

        // GET: Categoria
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public bool GuardarCategoria(string _cNombreCategoria)
        {
            bool _lResultado = ClaseBusiness.AgregarCategoria(_cNombreCategoria);
           
            return _lResultado;
        }
    }
}