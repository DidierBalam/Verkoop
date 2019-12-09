using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Verkoop.CapaDatos;
using System;

namespace Verkoop.Business
{
    public class SesionBusiness
    {
        /// <summary>
        /// Método para cambiar la contraseña del usuario.
        /// </summary>
        /// <param name="_cContraseniaActual">Recibe la contraseña actual</param>
        /// <param name="_cContraseniaNueva">Recibe la nueva contraseña</param>
        /// <param name="_iIdUsuario">Recibe el id del usuario</param>
        /// <returns></returns>
        public object CambiarContrasenia(string _cContraseniaActual, string _cContraseniaNueva, int _iIdUsuario)
        {
            bool _bEstadoOperacion;
            string _cMensaje;

            try
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
                {
                    if (VerificarContrasenia(_cContraseniaActual, _ctx))
                    {

                        _ctx.Configuration.LazyLoadingEnabled = false;
                        _ctx.Configuration.ProxyCreationEnabled = false;

                        tblSesion _objSesion = _ctx.tblSesion.AsNoTracking().FirstOrDefault(x => x.iIdUsuario == _iIdUsuario);

                        _objSesion.cContrasenia = _cContraseniaNueva;
                        _objSesion.dtFechaActualizacion = DateTime.Today;

                        _ctx.Entry(_objSesion).State = System.Data.Entity.EntityState.Modified;
                        _ctx.SaveChanges();

                        _bEstadoOperacion = true;
                        _cMensaje = "La contraseña ha sido actualizada";

                    }
                    else {
                        _bEstadoOperacion = false;
                        _cMensaje = "La contraseña actual es incorrecta.";

                    }
                   
                }
            }
            catch (Exception)
            {
                _bEstadoOperacion = false;
                _cMensaje = "La contraseña no pudo ser actualizada";
            }
            return (new { _bEstadoOperacion, _cMensaje });
        }


        /// <summary>
        /// Este método sirve para verificar la contraseña
        /// </summary>
        /// <param name="_cContraseniaActual">contiene la contraseña actual</param>
        /// <param name="_ctx">Recibe el contexto de la DB</param>
        /// <returns>Retorna el valor de la coincidencia (true/false) </returns>
        public bool VerificarContrasenia(string _cContraseniaActual, VerkoopDBEntities _ctx)
        {

            bool _bCoincidencia = _ctx.tblSesion.Any(x => x.cContrasenia == _cContraseniaActual);

            return _bCoincidencia;
        }

        public bool CerrarSesion()
        {

            return true;
        }

        /// <summary>
        /// Método para iniciar sesión haciendo una consulta en la tabla sesion y se comprar si el correo y la contraseña coinciden
        /// </summary>
        /// <param name="_cCorreo">recibe el correo electronico del usuario</param>
        /// <param name="_cContrasenia">Recibe la contraseña del usuario</param>
        /// <returns>Retorna el estado de la operación, un mensaje y la variable sesión</returns>
        public object IniciarSesion(string _cCorreo, string _cContrasenia)
        {
            bool _bEstadoOperacio;
            string _cVariableSesion = "";
            string _cMensaje = "";

            try { 
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                tblSesion _objSesion = (from Sesion in _ctx.tblSesion // se hace la consulta para tener el registro que contiene ese correo
                                        where Sesion.cCorreo == _cCorreo
                                        select Sesion).SingleOrDefault(); 

                if (_objSesion != null)
                {
                    string _cContraseniaEncriptada = EncriptarContrasenia(_cContrasenia); //se envia la contraseña y se toma el metodo EncriptarContraseña para compararlo en la base de datos

                    if (_objSesion.cContrasenia.Equals(_cContraseniaEncriptada)) // se comprueba si la contraseña es igual a la ingresada
                    {
                        _cVariableSesion = _objSesion.iIdUsuario.ToString();
                        _bEstadoOperacio = true;
                        _cMensaje = "ok";
                    }
                    else
                    {
                        _cMensaje = "La contraseña es incorrecta";
                        _bEstadoOperacio = false;
                    }
                }

                else
                {
                    _cMensaje = "No se encuentra el correo";
                    _bEstadoOperacio = false;
                }
            }
            }
            catch (Exception)
            {
                _bEstadoOperacio = false;
                _cMensaje = "Algo falló al iniciar sesión";
            }
            return (new { EstadoOperacion = _bEstadoOperacio, Mensaje = _cMensaje, VariableSesion = _cVariableSesion });
        }

        /// <summary>
        /// Método que comprueba si el correo existe en la base de datos.
        /// </summary>
        /// <param name="_cCorreo">Recibe el correo del usuario</param>
        /// <returns>Retorna true si existen coincidencias o false si no</returns>
        public bool VerificarExistenciaCorreo(string _cCorreo)
        {
            bool _bCoincidencia = false;

            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                _bCoincidencia = _ctx.tblSesion.Any(x => x.cCorreo == _cCorreo);
            }

            return _bCoincidencia;
        }

        /// <summary>
        /// Método para encriptar la contraseña del usuario con SHA256
        /// </summary>
        /// <param name="cContraseña">Recibe la contraseña</param>
        /// <returns>Retorna la cadena resultante del encriptado</returns>
        public string EncriptarContrasenia(string _cContrasenia)
        {
            using (SHA256 _Metodo = SHA256.Create())
            {
                byte[] _bContraseniaEncriptada = _Metodo.ComputeHash(Encoding.UTF8.GetBytes(_cContrasenia));

                StringBuilder _CadenaEncriptada = new StringBuilder();

                for (int _i = 0; _i < _bContraseniaEncriptada.Length; _i++)
                {
                    _CadenaEncriptada.Append(_bContraseniaEncriptada[_i].ToString());
                }

                return _CadenaEncriptada.ToString();
            }
        }
    }
}
