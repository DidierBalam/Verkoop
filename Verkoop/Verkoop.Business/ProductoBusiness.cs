using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verkoop.CapaDatos.DTO;

namespace Verkoop.Business
{
    class ProductoBusiness
    {
        public bool ActualizarDatosProducto(ProductoDTO _objDatosProducto)
        {

            return true;
        }

      
        public int CambiarCantidadProducto(int _iCantidad, int _iIdProducto)
        {

            return 0;
        }

     
        public bool CambiarEstadoProducto(bool _bEstado, int _iIdProducto)
        {

            return true;
        }

     
        public string CambiarImagenProducto(int _iIdProducto, string _cRutaImagen)
        {

            return "";
        }

        public string CargarPlantilla()
        {

            return null;
        }

        public string DescargarPlantilla()
        {

            return "";
        }

        public string ExportarDatos()
        {

            return "";
        }

        public Object ObtenerNumeroTotalProductoPorCategoria()
        {

            return null;
        }

        public int ObtenerNumeroTotalProductos()
        {

            return 0;
        }

       
        public List<VistaPreviaProductoClienteDTO> ObtenerProductoPorFiltro(int _iHistoral, int _iIdCategoria, decimal _dPrecioInicial, decimal _dPrecioFinal, string _cNombre)
        {

            return null;
        }

       
        public List<CatalogoProductoAdministradorDTO> ObtenerProductosPorEstado(bool _bEstado)
        {

            return null;
        }

        
        public bool RegistrarProducto(ProductoDTO _objDatosProducto)
        {

            return true;
        }

      
        public List<DetallesProductoDTO> VisualizarDetallesDeProductoCliente(int _iIdProducto)
        {

            return null;
        }

        
        public List<VisualizarDetallesProductoAdministradorDTO> VisualizarDetallesProductoAdministrador(int _iIdProducto)
        {

            return null;
        }
    }
}
