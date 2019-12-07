using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verkoop.CapaDatos.DTO
{
    public class CantidadProductoValidadoDTO
    {
       public bool bEstadoValidacion { get; set; }

        public List<ProductoEstadoDisponibleDTO> lstProducto { get; set; }
    }
}
