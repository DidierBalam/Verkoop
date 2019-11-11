using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verkoop.CapaDatos.DTO;

namespace Verkoop.Business
{
    class CompraBusiness
    {
        public bool EnviarCorreo(DetallesCompraClienteDTO objDatos)
        {

            return false;
        }

       
        public string GenerarTicketDeCompra(DetallesCompraClienteDTO objDatos)
        {

            return "";
        }

      
        public List<CompraDeClienteDTO> ObtenerComprasDeCliente(int iIdUsuario)
        {

            return null;
        }

        public int ObtenerNumeroTotalCompras()
        {

            return 0;
        }

        public decimal ObtenerNumeroTotalIngresoVentas()
        {

            return 0;
        }

        public List<ListaCompraClienteDTO> ObtenerTodasLasCompras()
        {

            return null;
        }

        public object ObtenerUsuariosMasCompras()
        {

            return null;
        }

        
        public List<DetallesCompraClienteDTO> VisualizarDetallesCompraCliente(int iIdCompra)
        {

            return null;
        }
    }
}
