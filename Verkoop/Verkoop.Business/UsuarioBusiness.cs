using System;
using System.Collections.Generic;
using Verkoop.CapaDatos.DTO;
using Verkoop.CapaDatos;
using System.Linq;
using System.Web;

namespace Verkoop.Business
{
    public class UsuarioBusiness
    {

        /// <summary>
        /// Método para actualizar los datos del usuario.
        /// </summary>
        /// <param name="_objDatosUsuario">Recibe los datos del usuario</param>
        /// <param name="_iIdUsuario">Recibe únicamente el id del usuario</param>
        /// <returns>Retorna el estado de la operación y su mensaje</returns>
        public object ActualizarDatosUsuario(tblCat_Usuario _objDatosUsuario, int _iIdUsuario)
        {
            bool _bEstadoOperacion;
            string _cMensaje;

            try
            {
                if (!VerificarExistenciaTelefonoEnActualizar(_iIdUsuario, _objDatosUsuario.cTelefono))
                {
                    using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
                    {

                        tblCat_Usuario _objUsuario = (from Usuario in _ctx.tblCat_Usuario
                                                      where Usuario.iIdUsuario == _iIdUsuario
                                                      select Usuario).SingleOrDefault();

                        _objUsuario.cNombre = _objDatosUsuario.cNombre;
                        _objUsuario.cApellidoPaterno = _objDatosUsuario.cApellidoPaterno;
                        _objUsuario.cApellidoMaterno = _objDatosUsuario.cApellidoMaterno;
                        _objUsuario.cTelefono = _objDatosUsuario.cTelefono;

                        _ctx.SaveChanges();

                        _bEstadoOperacion = true;
                        _cMensaje = "Datos Actualizados";
                    }
                }
                else
                {
                    _bEstadoOperacion = false;
                    _cMensaje = "¡No se pudieron actualizar los datos! El número de teléfono ya se ha registrado con otra cuenta";
                }

            }
            catch (Exception)
            {
                _bEstadoOperacion = false;
                _cMensaje = "Woow, Algo falló al actualizar los datos";
            }

            return (new { _bEstadoOperacion, _cMensaje });
        }

        /// <summary>
        /// Método para cambiar el estado de un usuario 
        /// </summary>
        /// <param name="_iIdUsuario">Recibe el ID del usuario.</param>
        /// <param name="_bEstado">Recibe el nuevo estado del usuario.</param>
        /// <returns>Retorna el estado de la operación y su mensaje.</returns>
        public object CambiarEstadoUsuario(int _iIdUsuario, bool _bEstado)
        {
            bool _bEstadoOperacion;
            string _cMensaje;

            try
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
                {
                    tblCat_Usuario _objUsuario = _ctx.tblCat_Usuario.AsNoTracking().FirstOrDefault(x => x.iIdUsuario == _iIdUsuario);

                    _objUsuario.lEstatus = _bEstado;
                    _objUsuario.dtFechaBaja = DateTime.Today;

                    _ctx.Entry(_objUsuario).State = System.Data.Entity.EntityState.Modified;
                    _ctx.SaveChanges();

                    _bEstadoOperacion = true;
                    _cMensaje = "Su cuenta ha sido cancelada.";
                }
            }
            catch (Exception e)
            {
                _bEstadoOperacion = false;
                _cMensaje = e.Message;//"Algo falló al momento de cancelar la cuenta.";
            }

            return (new { _bEstadoOperacion, _cMensaje});

        }

        /// <summary>
        /// Método para Cambiar la foto de perfil del usuario
        /// </summary>
        /// <param name="_cRutaFoto">Recibe la imagen</param>
        /// <param name="iIdUsuario">Recibe el id del usuario</param>
        /// <returns>Retorna el estado de la consulta y su mensaje (la url de la imagen si la operación es exitosa)</returns>
        public object CambiarFotoPerfil(HttpPostedFileBase _Imagen, int _iIdUsuario)
        {
            bool _bEstadoOperacion;
            string _cMensaje;

            CloudinaryBusiness _CloudinaryBusiness = new CloudinaryBusiness();

            try
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
                {
                    string _cRuta = _CloudinaryBusiness.SubirFotoPerfil(_Imagen, _iIdUsuario);

                    tblCat_Usuario _objUsuario = (from Usuario in _ctx.tblCat_Usuario
                                                  where Usuario.iIdUsuario == _iIdUsuario
                                                  select Usuario).SingleOrDefault();

                    _objUsuario.cImagen = _cRuta;
                    _ctx.SaveChanges();

                    _cMensaje = _cRuta;
                    _bEstadoOperacion = true;
                }
            }
            catch (Exception)
            {
                _cMensaje = "Algo falló al cambiar la foto.";
                _bEstadoOperacion = false;
            }

            return (new { _bEstadoOperacion, _cMensaje });
        }


        /// <summary>
        /// Método para obtener los datos del usuario.
        /// </summary>
        /// <param name="iIdUsuario">Recibe el id del usuario</param>
        /// <returns>Retorna los datos en un objeto con las propiedades del PerfilDatosUsuarioDTO</returns>
        public PerfilDatosUsuarioDTO ObtenerDatosDeUsuario(int _iIdUsuario)
        {
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                PerfilDatosUsuarioDTO _objDatosUsuario = (from Usuario in _ctx.tblCat_Usuario.AsNoTracking()
                                                          where Usuario.iIdUsuario == _iIdUsuario
                                                          select new PerfilDatosUsuarioDTO()
                                                          {
                                                              cNombre = Usuario.cNombre,
                                                              cApellidoPaterno = Usuario.cApellidoPaterno,
                                                              cApellidoMaterno = Usuario.cApellidoMaterno,
                                                              cImagenPerfil = Usuario.cImagen,
                                                              cNumeroTelefonico = Usuario.cTelefono
                                                          }).SingleOrDefault();

                return _objDatosUsuario;
            }
        }


        public string ObtenerNombreDeUsuario(int iIdNombreUsuario)
        {

            return "";
        }

        public int ObtenerNumeroTotalUsuariosClientes()
        {
            int dato = 0;
            using (VerkoopDBEntities ctx = new VerkoopDBEntities())
            {
                dato = ctx.tblCat_Usuario.Count();
            }
            return dato;
        }


        public List<BusquedaUsuarioPorEstadoDTO> ObtenerUsuarioClientePorEstado()
        {
            List<BusquedaUsuarioPorEstadoDTO> lstUsuarios;
            using (VerkoopDBEntities ctx = new VerkoopDBEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;
                lstUsuarios = (from user in ctx.tblCat_Usuario.AsNoTracking()
                               select new BusquedaUsuarioPorEstadoDTO()
                               {
                                   iIdUsuario = user.iIdUsuario,
                                   cNombre = user.cApellidoPaterno + " " + user.cApellidoMaterno + " " + user.cNombre,
                                   cTelefono = user.cTelefono,
                                   dtFechaIngreso = user.dtFechaIngreso

                               }).ToList();

            }
            return lstUsuarios;
        }

        /// <summary>
        /// Método para registrar un nuevo usuario.
        /// </summary>
        /// <param name="_objDatosUsuario">Recibe los datos del usuario (Datos para las tablas: tblCat_Usuario, tblDireccion y tblSesion)</param>
        /// <returns>Retorna el estado de la operación y su mensaje</returns>
        public object RegistrarUsuario(RegistrarUsuarioDTO _objDatosUsuario)
        {
            CorreoBusiness CorreoBusiness = new CorreoBusiness();

            string _cCodigoVerificacion = GenerarCodigoVerificacion();

            bool _bEstadoOperacion;
            string _cMensaje = "";

            SesionBusiness ClaseSesionBusiness = new SesionBusiness();

            try
            {
                if (!ClaseSesionBusiness.VerificarExistenciaCorreo(_objDatosUsuario.cCorreo))
                {
                    if (!VerificarExistenciaTelefonoEnRegistro(_objDatosUsuario.cTelefono))
                    {
                        using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
                        {
                            string _cContraseniaEncriptada = ClaseSesionBusiness.EncriptarContrasenia(_objDatosUsuario.cContrasenia);

                            tblCat_Usuario _TablaUsuario = new tblCat_Usuario
                            {
                                cNombre = _objDatosUsuario.cNombre,
                                cApellidoPaterno = _objDatosUsuario.cApellidoPaterno,
                                cApellidoMaterno = _objDatosUsuario.cApellidoMaterno,
                                cTelefono = _objDatosUsuario.cTelefono,
                                dtFechaIngreso = DateTime.Today,
                                lEstatus = false,
                                iTipoUsuario = 2
                                
                                
                            };

                            List<tblDireccion> _lstTablaDireccion = new List<tblDireccion>
                    {
                        new tblDireccion {
                            iIdUsuario = _TablaUsuario.iIdUsuario,
                            iIdMunicipio = _objDatosUsuario.iIdMunicipio,
                            cDireccion = _objDatosUsuario.cDireccion,
                            cCodigoPostal = _objDatosUsuario.cCodigoPostal,
                            lDefault = true
                        }
                    };

                            List<tblSesion> _lstTablaSesion = new List<tblSesion>()
                    {
                       new tblSesion()
                        {
                            iIdUsuario = _TablaUsuario.iIdUsuario,
                            cCorreo = _objDatosUsuario.cCorreo,
                            cContrasenia = _cContraseniaEncriptada,
                            cCodigoVerificacion = _cCodigoVerificacion,
                            lEstadoVerificacion = false
                        }
                    };

                            _TablaUsuario.tblSesion = _lstTablaSesion;
                            _TablaUsuario.tblDireccion = _lstTablaDireccion;

                            _ctx.tblCat_Usuario.Add(_TablaUsuario);
                            _ctx.SaveChanges();
                        }

                        CorreoBusiness.EnviarCódigoVerificacion(_objDatosUsuario.cCorreo, _cCodigoVerificacion);

                        _bEstadoOperacion = true;                       

                    }
                    else
                    {
                        _bEstadoOperacion = false;
                        _cMensaje = "El Teléfono ya se ha registrado con otra cuenta";
                    }
                }
                else
                {
                    _bEstadoOperacion = false;
                    _cMensaje = "El Correo ya se ha registrado con otra cuenta";
                }
            }
            catch (Exception e)
            {
                _bEstadoOperacion = false;
                _cMensaje = e.Message;// "Woow, algo salió mal al momento de registrar la cuenta";
            }

            return (new { _bEstadoOperacion, _cMensaje });
        }


        /// <summary>
        /// Método que comprueba si el teléfono existe en la base de datos.
        /// </summary>
        /// <param name="_cTelefono">Recibe el teléfono del usuario</param>
        /// <returns>Retorna true si existen coincidencias o false si no</returns>
        public bool VerificarExistenciaTelefonoEnRegistro(string _cTelefono)
        {
            bool _bCoincidencia = false;

            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                _bCoincidencia = _ctx.tblCat_Usuario.Any(x => x.cTelefono == _cTelefono && x.cTelefono != null);
            }

            return _bCoincidencia;
        }

        /// <summary>
        /// Método que comprueba si el teléfono existe en la base de datos.
        /// </summary>
        /// <param name="_iIdUsuario">Recibe el id del usuario</param>
        /// /// <param name="_cTelefono">Recibe el teléfono del usuario</param>
        /// <returns>Retorna true si existen coincidencias o false si no</returns>
        public bool VerificarExistenciaTelefonoEnActualizar(int _iIdUsuario, string _cTelefono)
        {
            bool _bCoincidencia = false;

            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                _bCoincidencia = _ctx.tblCat_Usuario.Any(x => x.cTelefono == _cTelefono && x.iIdUsuario != _iIdUsuario);

            }

            return _bCoincidencia;
        }

        /// <summary>
        /// Método para generar código de verificación
        /// </summary>
        /// <returns>Retorna el código generado</returns>
        private string GenerarCodigoVerificacion()
        {
            
            string _cCodigo = "VKR";

            Random _Valor = new Random();

            for (int _iContador = 0; _iContador < 6; _iContador++)
            {
                _cCodigo += _Valor.Next(0, 9);
            }

            return _cCodigo; 
        }
    }

}
