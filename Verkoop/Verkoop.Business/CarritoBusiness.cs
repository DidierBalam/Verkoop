using System;
using System.Collections.Generic;
using System.Linq;
using Verkoop.CapaDatos.DTO;
using Verkoop.CapaDatos;

namespace Verkoop.Business
{
    public class CarritoBusiness
    {
        /// <summary>
        /// Método para agregar un producto al carrito
        /// </summary>
        /// <param name="_objProducto">Contiene el idProducto y idUsuario</param>
        /// <returns>Retorna el estado de la consulta y la cantidad de productos agregados al carrito del usuario</returns>
        public object AgregarProductoCarrito(int _iIdProducto, int _iIdUsuario)
        {
            bool _EstadoConsulta;
            int _iProductosCarrito = 0;

            try
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
                {
                    tblCarrito _objTablaCarrito = new tblCarrito();

                    _objTablaCarrito.iIdProducto = _iIdProducto;
                    _objTablaCarrito.iIdUsuario = _iIdUsuario;
                    _objTablaCarrito.lEstatus = false;
                    _objTablaCarrito.dtFechaSeleccion = DateTime.Today;

                    _ctx.tblCarrito.Add(_objTablaCarrito);
                    _ctx.SaveChanges();

                    _iProductosCarrito = (from carrito in _ctx.tblCarrito
                                          where carrito.iIdUsuario == _iIdUsuario
                                          select carrito.iIdUsuario).Count();

                    _EstadoConsulta = true;
                }
            }
            catch (Exception)
            {
                _EstadoConsulta = false;
            }
            return (new { EstadoConsulta = _EstadoConsulta, ProductosCarrito = _iProductosCarrito });
        }

        public bool CambiarEstadoProductoCarrito(int _iIdCarrito, bool _bEstado)
        {

            return true;
        }

        public int ObtenerNumeroTotalProductosDeUsuario(int _iIdUsuario)
        {

            return 0;
        }
        /// <summary>
        /// Método para visualizar productos en el carrito
        /// </summary>
        /// <param name="_iIdUsuario">Contiene el idUsuario</param>
        /// <returns>Retorna la lista de los productos</returns>
        public List<ProductoEnCarritoDTO> ObtenerProductosDeUsuario(int _iIdUsuario)
        {
            List<ProductoEnCarritoDTO> _lstProductos = new List<ProductoEnCarritoDTO>();
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                _lstProductos = (from Carrito in _ctx.tblCarrito
                             where Carrito.iIdUsuario == _iIdUsuario
                             join Producto in _ctx.tblCat_Producto
                             on Carrito.iIdCarrito equals Producto.iIdProducto
                             select new ProductoEnCarritoDTO
                             {
                                 iIdCarrito = Carrito.iIdCarrito,
                                 cImagenCarrito = Producto.cImagen,
                                 cNombreproducto = Producto.cNombre,
                                 dPrecioProducto = Producto.dPrecio,
                                 iCantidad = Carrito.iCantidad
                             }).ToList();
            }
            return _lstProductos.ToList();
        }
        /// <summary>
        /// Método para quitar producto del carrito
        /// </summary>
        /// <param name="_iIdCarrito">Contiene el idCarrito</param>
        /// <returns>Retorna el estado de la operación y su mensaje</returns>
        public object QuitarProductoCarrito(int _iIdCarrito)
        {
            string _cMensaje;
            bool _bEstadoOperacion;

            try
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
                {
                    tblCarrito _TablaCarrito = (from Carrito in _ctx.tblCarrito
                                                where Carrito.iIdCarrito == _iIdCarrito
                                                select Carrito).First();

                    _ctx.tblCarrito.Remove(_TablaCarrito);
                    _ctx.SaveChanges();

                    _cMensaje = "Se ha removido el producto.";
                    _bEstadoOperacion = true;
                }
            }
            catch (Exception e)
            {
                _cMensaje = "Ups, el producto no se pudo remover correctamente.";
                _bEstadoOperacion = false;
            }
            return (new { _bEstadoOperacion, _cMensaje });
        }

        public bool RealizarPago(RealizarPagoDTO _objDatos)
        {

            return true;
        }
    }
}
