using System.Collections.Generic;
using Verkoop.CapaDatos.DTO;

namespace Verkoop.Business
{
    class UsuarioBusiness
    {

        public bool ActualizarDatosUsuario(ActualizarDatosUsuarioDTO _objDatos)
        {

            return true;
        }

      
        public bool CambiarEstadoUsuario(bool _bEstado, int _iIdUsuario)
        {

            return true;
        }

       
        public string CambiarFotoPerfil(string _cRutaFoto, int iIdUsuario)
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

      
        public List<BusquedaUsuarioPorEstadoDTO> ObtenerUsuarioClientePorEstado(bool _bEstado)
        {

            return null;
        }

        
        public bool RegistrarUsuario(RegistrarUsuarioDTO _objDatosUsuario)
        {

            return true;
        }
    }
}
