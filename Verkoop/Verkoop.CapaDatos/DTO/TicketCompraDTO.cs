using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verkoop.CapaDatos.DTO
{
    public class TicketCompraDTO
    {

        public decimal dPrecio { get; set; }
        public string dtFecha { get; set; }
        public int iIdCompra { get; set; }

        public List<ProductoCompradoDTO> ProductoComprado { get; set; }

        public DatosUsuarioTicketDTO objUsuario { get; set; }

    }
}
