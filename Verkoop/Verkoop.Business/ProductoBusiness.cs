using System;
using System.Collections.Generic;
using Verkoop.CapaDatos.DTO;
using Verkoop.CapaDatos;
using System.Linq;
using System.Data.Entity;

namespace Verkoop.Business
{
    public class ProductoBusiness
    {
        
        private string _cMensaje = string.Empty;
        private string _EstadoConsulta = string.Empty;

        public bool ActualizarDatosProducto(ProductoDTO _objDatosProducto)
        {

            return true;
        }

        /// <summary>
        /// Método para disminuir la cantidad del producto al realizar el pago.
        /// </summary>
        /// <param name="_ctx">Recibe el contexto de la  base de datos</param>
        /// <param name="_lstProducto">Recibe la lista de los productos a afectar(contiene el id  del producto y su cantidad de compra)</param>
        public List<tblCat_Producto> DisminuirCantidadProducto(VerkoopDBEntities _ctx, List<tblProductoComprado> _lstProducto)
        {
            List<tblCat_Producto> _lstProductoAfectado = new List<tblCat_Producto>();

            _lstProducto.ForEach(x =>
            {
                tblCat_Producto _objCarrito = _ctx.tblCat_Producto.Where(z => z.iIdProducto == x.iIdProducto).FirstOrDefault();

                _objCarrito.iCantidad -= x.iCantidad;

                _lstProductoAfectado.Add(_objCarrito);

            });

            return _lstProductoAfectado;       
        }

        /// <summary>
        /// Método para cambiar el estado del producto.
        /// </summary>
        /// <param name="_bEstado">Recibe el nuevo estado del producto</param>
        /// <param name="_iIdProducto">Recibe el id del produto</param>
        /// <returns>Retorna el estado de la operación y su mensaje</returns>
        public object CambiarEstadoProducto(bool _bEstado, int _iIdProducto)
        {
            
            try
            {             
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
                {
                    tblCat_Producto _objProductos;
                    _objProductos = _ctx.tblCat_Producto.Where(m => m.iIdProducto == _iIdProducto).FirstOrDefault();
                    _objProductos.lEstatus = false;
                    _objProductos.dtFechaBaja = DateTime.Now;

                    _ctx.Entry(_objProductos).State = EntityState.Modified;
                    _ctx.SaveChanges();

                    _EstadoConsulta = "success";
                    _cMensaje = "Producto deshabilitado Correctamente";
                }
            }
            catch (Exception)
            {
                _EstadoConsulta = "error";
                _cMensaje = "Algo falló al deshabilitar el producto";
            }

            return (new { EstadoConsulta = _EstadoConsulta, Mensaje = _cMensaje });

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

        /// <summary>
        /// Método para obtener la cantidad de productos agregados
        /// </summary>
        /// <returns>la cantidad de registros</returns>
        public int ObtenerNumeroTotalProductos()
        {
            int _iDato = 0;
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                _iDato = _ctx.tblCat_Producto.Count();
            }
            return _iDato;
        }

        /// <summary>
        /// Método para obtener los productos recien agregados
        /// </summary>
        /// <param name="_iNumeroConsulta">Recibe el número de consultas realizadas</param>
        /// <returns>Retorna una lista con los productos</returns>
        public List<VistaPreviaProductoClienteDTO> ObtenerProductosRecientes(int _iNumeroConsulta, int _iProductosObtener)
        {
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                List<VistaPreviaProductoClienteDTO> _lstProductos = (from Producto in _ctx.tblCat_Producto
                                                                     //orderby Producto.cNombre descending
                                                                     select new VistaPreviaProductoClienteDTO
                                                                     {
                                                                         iIdProducto = Producto.iIdProducto,
                                                                         cNombreProducto = Producto.cNombre,
                                                                         cImagenProducto = Producto.cImagen,
                                                                         dPrecioProducto = Producto.dPrecio.ToString(),
                                                                         iCantidad = Producto.iCantidad

                                                                     })/*.Skip(_iNumeroConsulta * _iProductosObtener)*/
                                                                     //.Take(_iProductosObtener)
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
        public List<VistaPreviaProductoClienteDTO> ObtenerProductosPorCategoria(string _cCategoria, int _iNumeroConsulta, int _iProductosObtener)
        {
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                List<VistaPreviaProductoClienteDTO> _lstProductos = (from Producto in _ctx.tblCat_Producto
                                                                     join Categoria in _ctx.tblCat_Categoria 
                                                                     on Producto.iIdCategoria equals Categoria.iIdCategoria
                                                                     where Categoria.cNombre == _cCategoria
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
        public List<VistaPreviaProductoClienteDTO> ObtenerProductosMasComprados(int _iNumeroConsulta, int _iProductosObtener)
        {
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
        public List<VistaPreviaProductoClienteDTO> BuscarProducto(string _cNombre, int _iNumeroConsulta, int _iProductosObtener)
        {
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            { 
                List<VistaPreviaProductoClienteDTO> _lstProductos = (from Producto in _ctx.tblCat_Producto
                                                                     where Producto.cNombre.ToUpper().Contains(_cNombre.Trim().ToUpper())

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
        /// MÉTODO PARA OBTENER PRODUCTOS POR ESTADO.
        /// </summary>
        /// <param name="_bEstado"></param>
        /// <returns></returns>
        public List<CatalogoProductoAdministradorDTO> ObtenerProductosPorEstado(bool _bEstado)
        {
            List<CatalogoProductoAdministradorDTO> lstUsuarios;
            using (VerkoopDBEntities ctx = new VerkoopDBEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;
                lstUsuarios = (from Producto in ctx.tblCat_Producto.AsNoTracking()
                               where (Producto.lEstatus == _bEstado)
                               select new CatalogoProductoAdministradorDTO()
                               {
                                   iIdProducto = Producto.iIdProducto,
                                   cNombre = Producto.cNombre,
                                   iCantidad = Producto.iCantidad,
                                   dPrecio = Producto.dPrecio,
                                   dtFechaAlta = Producto.dtFechaAlta,
                                   dtFechaModificacion = Producto.dtFechaModificacion
                               }).ToList();

            }
            return lstUsuarios;
        }


        public bool RegistrarProducto(ProductoDTO _objDatosProducto)
        {

            return true;
        }


        /// <summary>
        /// MÉTODO PARA OBTENER LOS DETALLES DEL PRODUCTO
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

        /// <summary>
        /// MÉTODO PARA VALIDAR LA CANTIDAD DE LOS PRODUCTOS A COMPRAR.
        /// </summary>
        /// <param name="_ctx">Recibe el contexto de la base de datos</param>
        /// <param name="_lstProducto">Recibe las listas de los productos a validar</param>
        /// <returns>Retorna el estado general de la validación y el estado de cada producto con su mensaje</returns>
        public CantidadProductoValidadoDTO ValidarCatidadCompraProducto(VerkoopDBEntities _ctx, List<tblProductoComprado> _lstProducto)
        {

            CantidadProductoValidadoDTO _lstCatidadValidad = new CantidadProductoValidadoDTO();

            _lstCatidadValidad.lstProducto = new List<ProductoEstadoDisponibleDTO>();
            _lstCatidadValidad.bEstadoValidacion = true; //Inicializamos la validación a true, esperando un false en cualquier iteración.

            _lstProducto.ForEach(x => //Recorre cada producto de la lista.
            {
                ProductoEstadoDisponibleDTO _objProductoValidado = new ProductoEstadoDisponibleDTO();

                tblCat_Producto _objProducto = _ctx.tblCat_Producto.AsNoTracking().Where(y => y.iIdProducto == x.iIdProducto).SingleOrDefault();//Obtiene el nombre y la cantidad del producto en la DB.

                if (_objProducto.iCantidad == 0) //Condición producto agotado.
                {
                    _lstCatidadValidad.bEstadoValidacion = false; //Estado validación general.
                    _objProductoValidado.bEstado = false;//Estado validación producto individual.
                    _objProductoValidado.cMensaje = "Lo sentimos, el producto " + _objProducto.cNombre + " se agotó";//Mensaje de la validación del producto.

                }
                else if (_objProducto.iCantidad > 0 && _objProducto.iCantidad < x.iCantidad) //Condición cantidad de pedido mayor a la disponibilidad del producto.
                {
                    _lstCatidadValidad.bEstadoValidacion = false;
                    _objProductoValidado.bEstado = false;
                    _objProductoValidado.cMensaje = "Lo sentimos, el producto " + _objProducto.cNombre + " solo cuenta con " + _objProducto.iCantidad + " piezas disponibles";

                }
                else
                {
                    _objProductoValidado.bEstado = true;
                    _objProductoValidado.cMensaje = "";

                }

                _lstCatidadValidad.lstProducto.Add(_objProductoValidado);

            });

            return _lstCatidadValidad;
        }

        /// <summary>
        /// MÉTODO PARA VISUALIZAR LOS DETALLES DEL PRODUCTO.
        /// </summary>
        /// <param name="_iIdProducto"></param>
        /// <returns></returns>
        public VisualizarDetallesProductoAdministradorDTO VisualizarDetallesProductoAdministrador(int _iIdProducto)
        {
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                VisualizarDetallesProductoAdministradorDTO _objProducto = (from Producto in _ctx.tblCat_Producto.AsNoTracking()
                                                                           where Producto.iIdProducto == _iIdProducto
                                                                           select new VisualizarDetallesProductoAdministradorDTO
                                                                           {
                                                                               iIdProducto = Producto.iIdProducto,
                                                                               cNombreProducto = Producto.cNombre,
                                                                               cDescripcion = Producto.cDescripcion,
                                                                               dPrecio = Producto.dPrecio,
                                                                               cImagen = Producto.cImagen,
                                                                               iCantidad = Producto.iCantidad,
                                                                               dtFechaAlta = Producto.dtFechaAlta,
                                                                               dtFechaModificacion = Producto.dtFechaModificacion

                                                                           }).SingleOrDefault();
                return _objProducto;
            }
        }
    }
}
