using System;
using System.Collections.Generic;
using System.Linq;
using Verkoop.CapaDatos.DTO;
using Verkoop.CapaDatos;

namespace Verkoop.Business
{

    public class CarritoBusiness
    {
        ProductoBusiness ProductoBusiness = new ProductoBusiness();

        /// <summary>
        /// Método para agregar un producto al carrito
        /// </summary>
        /// <param name="_objProducto">Contiene el idProducto y idUsuario</param>
        /// <returns>Retorna el estado de la consulta y la cantidad de productos agregados al carrito del usuario</returns>
        public object AgregarProductoCarrito(int _iIdProducto, int _iIdUsuario, int _iCantidad)
        {
            bool _EstadoConsulta;
            string _cMensaje;

            try
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
                {
                    tblCarrito _objTablaCarrito = new tblCarrito
                    {
                        iIdProducto = _iIdProducto,
                        iIdUsuario = _iIdUsuario,
                        lEstatus = false,
                        iCantidad = _iCantidad,
                        dtFechaSeleccion = DateTime.Today
                    };

                    _ctx.tblCarrito.Add(_objTablaCarrito);
                    _ctx.SaveChanges();

                    _EstadoConsulta = true;
                    _cMensaje = "Producto agregado al carrito";
                }
            }
            catch (Exception)
            {
                _EstadoConsulta = false;
                _cMensaje = "Algo falló al agregar el producto al carrito";
            }
            return (new { EstadoConsulta = _EstadoConsulta, _cMensaje});
        }

        /// <summary>
        /// Método para cambiar el estado de los productos del carrito al realizar el pago.
        /// </summary>
        /// <param name="_ctx">Recibe el contexto de la base de datos</param>
        /// <param name="_lstProducto">Recibe la lista de los productos a afectar</param>
        public List<tblCarrito> CambiarEstadoProductoCarrito(VerkoopDBEntities _ctx, List<tblProductoComprado> _lstProducto)
        {
            List<tblCarrito> _lstCarritoAfectado = new List<tblCarrito>();

            _lstProducto.ForEach(x =>
            {
                tblCarrito _objCarrito = _ctx.tblCarrito.Where(z => z.iIdProducto == x.iIdProducto).FirstOrDefault();

                _objCarrito.lEstatus = true;

                _lstCarritoAfectado.Add(_objCarrito);

            });

            return _lstCarritoAfectado;
        }

        /// <summary>
        /// Método para obtener los productos agregados del cliente.
        /// </summary>
        /// <param name="_ctx">Recibe el contexto de la base de datos</param>
        /// <param name="_iIdUsuario">Recibe el id del usuario</param>
        /// <returns>Retorna el valor entero de los productos agregados al carrito</returns>
        public int ObtenerNumeroTotalProductosDeUsuario(VerkoopDBEntities _ctx, int _iIdUsuario)
        {
            int _iProductosCarrito = (from carrito in _ctx.tblCarrito.AsNoTracking()
                                      where carrito.iIdUsuario == _iIdUsuario
                                      && carrito.lEstatus == false
                                      select carrito.iIdUsuario).Count();

            return _iIdUsuario;
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
                _lstProductos = (from Carrito in _ctx.tblCarrito.AsNoTracking()
                                 where Carrito.iIdUsuario == _iIdUsuario
                                 && Carrito.lEstatus == false
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
            catch (Exception)
            {
                _cMensaje = "Ups, el producto no se pudo remover correctamente.";
                _bEstadoOperacion = false;
            }
            return (new { _bEstadoOperacion, _cMensaje });
        }

        /// <summary>
        /// Método para realizar el pago de los productos agregados al carrito.
        /// </summary>
        /// <param name="_objPago">Recibe el id del usuario</param>
        public object RealizarPago(int _iIdUsuario, RealizarPagoDTO _objPago)
        {
            bool _bEstadoOperacion;
            string _cMensaje;
            object _objProductosEstado;

            List<tblProductoComprado> _TablaProductoComprado = new List<tblProductoComprado>();

            try
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
                {
                    CantidadProductoValidadoDTO _objResultadoValidarCantidad = ProductoBusiness.ValidarCantidadCompraProducto(_ctx, _objPago.lstProductoComprado);//Valida la cantidad de la compra del producto.

                    if (_objResultadoValidarCantidad.bEstadoValidacion)
                    {
                        tblCompra _TablaCompra = new tblCompra
                        {
                            iIdUsuario = _iIdUsuario,
                            iIdDireccion = _objPago.iIdDireccion,
                            iIdTarjeta = _objPago.objTarjeta.iIdTarjeta,
                            dtFecha = DateTime.Today
                        };

                        _objPago.lstProductoComprado.ForEach(x =>
                        {

                            tblProductoComprado _objProducto = new tblProductoComprado
                            {
                                iIdCompra = _TablaCompra.iIdCompra,
                                iIdProducto = x.iIdProducto,
                                iCantidad = x.iCantidad
                            };

                            _TablaProductoComprado.Add(_objProducto);
                        });

                        _TablaCompra.tblProductoComprado = _TablaProductoComprado;
                        _ctx.tblCompra.Add(_TablaCompra);

                        List<tblCarrito> _lstCarritoAfectado = CambiarEstadoProductoCarrito(_ctx, _objPago.lstProductoComprado);//Cambia estado del producto a true indicando que el producto se ha comprado.
                        //List<tblCat_Producto> _lstProductoAfectado = ProductoBusiness.DisminuirCantidadProducto(_ctx, _objPago.lstProductoComprado); //Resta a la cantidad disponible del producto la cantidad asignada en la compra.

                        _ctx.SaveChanges();

                        _bEstadoOperacion = true;
                        _cMensaje = "Pago realizado exitosamente";
                        _objProductosEstado = null;
                    }

                    else
                    {
                        _bEstadoOperacion = false;
                        _cMensaje = "La compra no se puede realizar";
                        _objProductosEstado = _objResultadoValidarCantidad.lstProducto;
                    }

                }

            }
            catch (Exception e)
            {
                _bEstadoOperacion = false;
                _cMensaje = e.Message/*"Algo falló al realizar el pago"*/;
                _objProductosEstado = null;
            }

            return (new
            {
                _bEstadoOperacion,
                _cMensaje,
                _objProductosEstado
            });
        }

        public PagoPaypalDTO ObtenerProductosCarrito(RealizarPagoDTO _objPago)
        {
            List<int> productsIds = new List<int> { 1, 2, 3, 2, 1 };

            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                var shoppingCart = _ctx.tblCat_Producto
                   .Join(productsIds, SC => SC.iIdProducto, PI => PI, (SC, PI) => SC)
                   .ToList();

                var xD = shoppingCart;
            }

            List<ProductoPaypalDTO> lstProductos = new List<ProductoPaypalDTO>();

            PagoPaypalDTO ProductosxD = new PagoPaypalDTO();

            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                _objPago.lstProductoComprado.ForEach(x =>
                {
                    ProductoPaypalDTO ob = (from producto in _ctx.tblCat_Producto
                                            where producto.iIdProducto == x.iIdProducto
                                            select new ProductoPaypalDTO
                                            {
                                                iCantidad = x.iCantidad,
                                                cNombre = producto.cNombre
                                            }).SingleOrDefault();

                    lstProductos.Add(/*ob*/null);
                });

                ProductosxD.lstProducto = lstProductos;

            }

            return ProductosxD;
        }
    }
}
