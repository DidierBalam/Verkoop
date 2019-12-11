﻿using System.Web.Mvc;
using Verkoop.Business;
using Verkoop.CapaDatos.DTO;
using Verkoop.CapaDatos;
using System.Web;
using System;

namespace Cliente.Controllers
{
    public class PerfilController : Controller
    {
        UsuarioBusiness UsuarioBusiness = new UsuarioBusiness();

        #region Vistas
        public ActionResult InformacionPersonal()
        {
            return View();
        }


        public ActionResult CambiarContraseña()
        {
            return View();
        }

        public ActionResult Direcciones()
        {
            return View();
        }


        public ActionResult Tarjetas()
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