﻿using System.Web.Mvc;
using Verkoop.Business;
using Verkoop.CapaDatos.DTO;
using Verkoop.CapaDatos;
using System.Web;
using System;
using System.Collections.Generic;

namespace Cliente.Controllers
{
    public class PerfilController : Controller
    {
        UsuarioBusiness UsuarioBusiness = new UsuarioBusiness();
        DireccionBusiness DireccionBusiness = new DireccionBusiness();
        CarritoBusiness CarritoBusiness = new CarritoBusiness();

        TarjetaBusiness TarjetaBusiness = new TarjetaBusiness();

        #region Vistas
        public ActionResult InformacionPersonal()
        {
            int _iTotalCarrito = CarritoBusiness.ObtenerNumeroTotalProductosDeUsuario(Convert.ToInt32(Session["iIdUsuario"]));

            ViewBag.TotalCarrito = _iTotalCarrito;

            return View();
        }


        public ActionResult CambiarContraseña()
        {
            int _iTotalCarrito = CarritoBusiness.ObtenerNumeroTotalProductosDeUsuario(Convert.ToInt32(Session["iIdUsuario"]));

            ViewBag.TotalCarrito = _iTotalCarrito;

            return View();
        }

        public ActionResult Direcciones()
        {
            int _iTotalCarrito = CarritoBusiness.ObtenerNumeroTotalProductosDeUsuario(Convert.ToInt32(Session["iIdUsuario"]));

            ViewBag.TotalCarrito = _iTotalCarrito;

            List<DireccionDTO> _lstDirecciones = DireccionBusiness.ObtenerDireccionesDeUsuario(2);

            return View(_lstDirecciones);
        }

        public ActionResult NuevaDireccion()
        {
            return View();
        }

        /// <summary>
        /// Método que devuelve la vista tarjetas con la lista de tarjetas
        /// </summary>  
        /// <returns>retorna la vista de tarjetas con la lista </returns>
        public ActionResult Tarjetas()
        {
            int _iTotalCarrito = CarritoBusiness.ObtenerNumeroTotalProductosDeUsuario(Convert.ToInt32(Session["iIdUsuario"]));

            ViewBag.TotalCarrito = _iTotalCarrito;

            List<TarjetaDTO> _lstTarjetas = TarjetaBusiness.ObtenerTodasTarjetas(1);

            return View(_lstTarjetas);
        }

        /// <summary>
        /// Método que regresa a la vista NuevaTarjeta.
        /// </summary>
        /// <returns>Regresa a la vista a NuevaTarjeta.</returns>
        [HttpPost]
        public ActionResult NuevaTarjeta()
        {
            return View();
        }


        #endregion

        #region Métodos
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ObtenerDatosDeUsuario()
        {
            PerfilDatosUsuarioDTO _objDatosUsuario = UsuarioBusiness.ObtenerDatosDeUsuario(Convert.ToInt32(Session["iIdUsuario"])/*iIdUsuario*/);

            return Json(_objDatosUsuario);
        }

        /// <summary>
        ///  Método que conecta al método ActualizarDatosUsuario() del UsuarioBusiness
        /// </summary>
        /// <param name="_objDatosUsuario"></param>
        /// <returns>Retorna el estado de la operación y su mensaje</returns>
        [HttpPost]
        public JsonResult ActualizarDatosUsuario(tblCat_Usuario _objDatosUsuario)
        {
            object _objResultado = UsuarioBusiness.ActualizarDatosUsuario(_objDatosUsuario, 19 /*iIdUsuario*/);

            return Json(_objResultado);
        }

        /// <summary>
        /// Método para cambiar la foto de perfil del usuario
        /// </summary>
        /// <param name="_Imagen">Recibe la imagen</param>
        /// <returns>Retorna el estado de la operación y su mensaje (La ruta de la imagen si la operación es exitosa)</returns>
        [HttpPost]
        public JsonResult CambiarFotoPerfil(HttpPostedFileBase _Imagen)
        {

            object _bResultado = UsuarioBusiness.CambiarFotoPerfil(_Imagen, 1/*iIdUsuario*/);

            return Json(_bResultado);

        }

        /// <summary>
        /// Método para cancelar cuenta usuario.
        /// </summary>
        /// <returns>regresa el estado de la operación y su mensaje</returns>
        [HttpPost]
        public JsonResult CancelarCuenta()
        {
            object _objResultado = UsuarioBusiness.CambiarEstadoUsuario(1 , false);

            return Json(_objResultado);
        }

        #endregion
    }
}