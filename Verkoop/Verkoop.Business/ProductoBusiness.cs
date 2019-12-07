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

        /// <summary>
        /// Método para disminuir la cantidad del producto al realizar el pago.
        /// </summary>
        /// <param name="_ctx">Recibe el contexto de la  base de datos</param>
        /// <param name="_lstProducto">Recibe la lista de los productos a afectar(contiene el id  del producto y su cantidad de compra)</param>
        public void DisminuirCantidadProducto(VerkoopDBEntities _ctx, List<tblProductoComprado> _lstProducto)
        {
          

            List<tblCat_Producto> lstCarrito = _ctx.tblCat_Producto.Where(x => _lstProducto.Select(y => y.iIdProducto).Contains(x.iIdProducto)).ToList();

            lstCarrito.ForEach(z =>
            {
                z.iCantidad -= Convert.ToInt32(_lstProducto.Where(a => a.iIdProducto == z.iIdProducto).Select(a => new { a.iCantidad }).First());
            });

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
        /// Método para obtener los productos recien agregados
        /// </summary>
        /// <param name="_iNumeroConsulta">Recibe el número de consultas realizadas</param>
        /// <returns>Retorna una lista con los productos</returns>
        public List<VistaPreviaProductoClienteDTO> ObtenerProductosRecientes(int _iNumeroConsulta)
        {
            int _iProductosObtener = 20;

            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                List<VistaPreviaProductoClienteDTO> _lstProductos = (from Producto in _ctx.tblCat_Producto
                                                                     orderby Producto.dtFechaAlta descending
                                                                     select new VistaPreviaProductoClienteDTO
                                                                     {
                                                                         iIdProducto = Producto.iIdProducto,
                                                                         cNombreProducto = Producto.cNombre,
                                                                         cImagenProducto = Producto.cImagen,
                                                                         dPrecioProducto = Producto.dPrecio.ToString(),
                                                                         iCantidad = Producto.iCantidad

                                                                     }).Skip(_iNumeroConsulta * _iProductosObtener)
                                                                     .Take(_iProductosObtener)
                                                                     .ToList();
                return _lstProductos;
            }
        }


        /// <summary>
        /// Método para obtener productos por categoría
        /// </summary>
        /// <param name="_iIdCategoria">Recibe el id de la categoría</param>
        /// <param name="_iNumeroConsulta ">Recibe el número de consultas realizadas</param>
        /// <returns>Retorna una lista con los productos</returns>
        public List<VistaPreviaProductoClienteDTO> ObtenerProductosPorCategoria(int _iIdCategoria, int _iNumeroConsulta)
        {
            int _iProductosObtener = 20;

            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                List<VistaPreviaProductoClienteDTO> _lstProductos = (from Producto in _ctx.tblCat_Producto
                                                                     where Producto.iIdCategoria == _iIdCategoria
                                                                     orderby Producto.dtFechaAlta descending
                                                                     select new VistaPreviaProductoClienteDTO
                                                                     {
                                                                         iIdProducto = Producto.iIdProducto,
                                                                         cNombreProducto = Producto.cNombre,
                                                                         cImagenProducto = Producto.cImagen,
                                                                         dPrecioProducto = Producto.dPrecio.ToString(),
                                                                         iCantidad = Producto.iCantidad

                                                                     }).Skip(_iNumeroConsulta * _iProductosObtener)
                                                                     .Take(_iProductosObtener)
                                                                     .ToList();
                return _lstProductos;
            }
        }

        /// <summary>
        /// Método para obtener los productos más comprados
        /// </summary>
        /// <param name="_iNumeroConsulta">Recibe el número de consultas realizadas</param>
        /// <returns>Retorna una lista con los productos</returns>
        public List<VistaPreviaProductoClienteDTO> ObtenerProductosMasComprados(int _iNumeroConsulta)
        {
            int _iProductosObtener = 20;

            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                List<VistaPreviaProductoClienteDTO> _lstProductos = (from ProductoComprado in _ctx.tblProductoComprado
                                                                     join Producto in _ctx.tblCat_Producto on ProductoComprado.iIdProducto
                                                                     equals Producto.iIdProducto
                                                                     group new { ProductoComprado, Producto } by ProductoComprado.iIdProducto into ProductoAgrupado
                                                                     orderby ProductoAgrupado.Select(x => x.ProductoComprado.iCantidad).FirstOrDefault() descending
                                                                     select new VistaPreviaProductoClienteDTO
                                                                     {
                                                                         iIdProducto = ProductoAgrupado.Select(x => x.ProductoComprado.iIdProducto).FirstOrDefault(),
                                                                         cNombreProducto = ProductoAgrupado.Select(x => x.Producto.cNombre).FirstOrDefault(),
                                                                         cImagenProducto = ProductoAgrupado.Select(x => x.Producto.cImagen).FirstOrDefault(),
                                                                         dPrecioProducto = ProductoAgrupado.Select(x => x.Producto.dPrecio).FirstOrDefault().ToString(),
                                                                         iCantidad = ProductoAgrupado.Select(x => x.Producto.iCantidad).FirstOrDefault()

                                                                     }).Skip(_iNumeroConsulta * _iProductosObtener)
                                                                     .Take(_iProductosObtener)
                                                                     .ToList();
                return _lstProductos;
            }
        }

        /// <summary>
        /// Método para buscar productos por nombre
        /// </summary>
        /// <param name="_cNombre">Recibe el nombre del producto</param>
        /// <param name="_iNumeroConsulta">Recibe el núemero de consultas realzadas</param>
        /// <returns>Retorna una lista con los productos</returns>
        public List<VistaPreviaProductoClienteDTO> BuscarProducto(string _cNombre, int _iNumeroConsulta)
        {
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {      
                int _iProductosObtener = 20;

                List<VistaPreviaProductoClienteDTO> _lstProductos = (from Producto in _ctx.tblCat_Producto
                                                                     where Producto.cNombre.ToUpper().Contains(_cNombre.ToUpper())
                                                                     
                                                                     orderby Producto.dtFechaAlta descending
                                                                     select new VistaPreviaProductoClienteDTO
                                                                     {
                                                                         iIdProducto = Producto.iIdProducto,
                                                                         cNombreProducto = Producto.cNombre,
                                                                         cImagenProducto = Producto.cImagen,
                                                                         dPrecioProducto = Producto.dPrecio.ToString(),
                                                                         iCantidad = Producto.iCantidad

                                                                     }).Skip(_iNumeroConsulta * _iProductosObtener)
                                                                     .Take(_iProductosObtener)
                                                                     .ToList();
                return _lstProductos;
            }
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
