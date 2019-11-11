using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verkoop.CapaDatos.DTO;

namespace Verkoop.Business
{
    class CarritoBusiness
    {       
        public bool AgregarProductoCarrito(int _iIdProducto, int _iIdUsuario)
        {

            return true;
        }

       
        public bool CambiarEstadoProductoCarrito(int _iIdCarrito, bool _bEstado)
        {

            return true;
        }

       
        public int ObtenerNumeroTotalProductosDeUsuario(int _iIdUsuario)
        {

            return 0;
        }

       
        public List<ProductoEnCarritoDTO> ObtenerProductosDeUsuario(int _iIdUsuario)
        {

            return null;
        }

       
        public bool QuitarProductoCarrito(int _iIdCarrito, int _iIdUsuario)
        {

            return true;
        }

     
        public bool RealizarPago(RealizarPagoDTO _objDatos)
        {

            return true;
        }
    }
}
