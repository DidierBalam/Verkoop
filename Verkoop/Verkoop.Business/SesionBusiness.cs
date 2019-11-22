using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Verkoop.CapaDatos;

namespace Verkoop.Business
{
    public class SesionBusiness
    {
        public bool CambiarContrasenia(string _cContraseniaNueva)
        {

            return true;
        }

        public bool CerrarSesion()
        {

            return true;
        }

        public bool IniciarSesion(string _cCorreo, string _cContrasenia)
        {
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                

               tblSesion _objSesion = (from Sesion in _ctx.tblSesion.AsNoTracking()
                           where Sesion.cCorreo == _cCorreo 
                           select Sesion).FirstOrDefault();

                
            }
                return true;
        }

        /// <summary>
        /// Método que comprueba si el correo existe en la base de datos.
        /// </summary>
        /// <param name="_cCorreo">Recibe el correo del usuario</param>
        /// <returns>Retorna true si exiten coincidencias o false si no</returns>
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
