using System;
using System.Collections.Generic;
using Verkoop.CapaDatos.DTO;
using Verkoop.CapaDatos;
using System.Linq;

namespace Verkoop.Business
{
    public class ProductoBusiness
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<VistaPreviaProductoClienteDTO> ObtenerProductosRecientes()
        {
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                List<VistaPreviaProductoClienteDTO> _lstProducto = (from Producto in _ctx.tblCat_Producto
                                                                    orderby Producto.dtFechaModificacion descending
                                                                    select new VistaPreviaProductoClienteDTO
                                                                    {
                                                                        iIdProducto = Producto.iIdProducto,
                                                                        cNombreProducto = Producto.cNombre,
                                                                        cImagenProducto = Producto.cImagen,
                                                                        dPrecioProducto = Producto.dPrecio.ToString(),
                                                                        iCantidad = Producto.iCantidad

                                                                    }).ToList();
            }
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_iIdCategoria"></param>
        /// <returns></returns>
        public List<VistaPreviaProductoClienteDTO> ObtenerProductosRecientesPorCategoria(int _iIdCategoria)
        {
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                List<VistaPreviaProductoClienteDTO> _lstProducto = (from Producto in _ctx.tblCat_Producto                                                                  
                                                                    where Producto.iIdCategoria == _iIdCategoria
                                                                    orderby Producto.dtFechaModificacion descending
                                                                    select new VistaPreviaProductoClienteDTO
                                                                    {
                                                                        iIdProducto = Producto.iIdProducto,
                                                                        cNombreProducto = Producto.cNombre,
                                                                        cImagenProducto = Producto.cImagen,
                                                                        dPrecioProducto = Producto.dPrecio.ToString(),
                                                                        iCantidad = Producto.iCantidad

                                                                    }).ToList();
            }
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


        /// <summary>
        /// Método para obtener los detalles del producto
        /// </summary>
        /// <param name="_iIdProducto">Recibe el id del producto</param>
        /// <returns>Retorna un objeto con los datos del producto</returns>
        public DetallesProductoDTO VisualizarDetallesDeProductoCliente(int _iIdProducto)
        {
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                DetallesProductoDTO _objProducto = (from Producto in _ctx.tblCat_Producto.AsNoTracking()
                                                    where Producto.iIdProducto == _iIdProducto
                                                    select new DetallesProductoDTO
                                                    {
                                                        iIdProducto = Producto.iIdProducto,
                                                        cNombre = Producto.cNombre,
                                                        cDescripcionProducto = Producto.cDescripcion,
                                                        cPrecioProducto = Producto.dPrecio.ToString(),
                                                        cImagenProducto = Producto.cImagen,
                                                        iCantidad = Producto.iCantidad

                                                    }).SingleOrDefault();
                return _objProducto;
            }
        }


        public List<VisualizarDetallesProductoAdministradorDTO> VisualizarDetallesProductoAdministrador(int _iIdProducto)
        {

            return null;
        }
    }
}
