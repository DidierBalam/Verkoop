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
        public bool AgregarProductoCarrito(int iIdProducto, int iIdUsuario)
        {

            return true;
        }

       
        public bool CambiarEstadoProductoCarrito(int iIdCarrito, bool bEstado)
        {

            return true;
        }

       
        public int ObtenerNumeroTotalProductosDeUsuario(int iIdUsuario)
        {

            return 0;
        }

       
        public List<ProductoEnCarritoDTO> ObtenerProductosDeUsuario(int iIdUsuario)
        {

            return null;
        }

       
        public bool QuitarProductoCarrito(int iIdCarrito, int iIdUsuario)
        {

            return true;
        }

     
        public bool RealizarPago(RealizarPagoDTO objDatos)
        {

            return true;
        }
    }
}
