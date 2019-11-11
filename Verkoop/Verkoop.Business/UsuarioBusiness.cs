using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verkoop.CapaDatos.DTO;

namespace Verkoop.Business
{
    class UsuarioBusiness
    {

        public bool ActualizarDatosUsuario(ActualizarDatosUsuarioDTO objDatos)
        {

            return true;
        }

      
        public bool CambiarEstadoUsuario(bool bEstado, int iIdUsuario)
        {

            return true;
        }

       
        public string CambiarFotoPerfil(string cRutaFoto, int iIdUsuario)
        {

            return "";
        }

       
        public List<PerfilDatosUsuarioDTO> ObtenerDatosDeUsuario(int iIdUsuario)
        {

            return null;
        }

        
        public string ObtenerNombreDeUsuario(int iIdNombreUsuario)
        {

            return "";
        }

        public int ObtenerNumeroTotalUsuariosClientes()
        {

            return 0;
        }

      
        public List<BusquedaUsuarioPorEstadoDTO> ObtenerUsuarioClientePorEstado(bool bEstado)
        {

            return null;
        }

        
        public bool RegistrarUsuario(RegistrarUsuarioDTO objDatosUsuario)
        {

            return true;
        }
    }
}
