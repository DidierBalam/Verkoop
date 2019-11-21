using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
