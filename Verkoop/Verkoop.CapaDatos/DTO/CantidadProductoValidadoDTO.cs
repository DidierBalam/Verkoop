using System.Collections.Generic;

namespace Verkoop.CapaDatos.DTO
{
    public class CantidadProductoValidadoDTO
    {
       public bool bEstadoValidacion { get; set; }

        public List<ProductoEstadoDisponibleDTO> lstProducto { get; set; }
    }
}
