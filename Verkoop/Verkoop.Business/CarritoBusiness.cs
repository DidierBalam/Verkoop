﻿using System;
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
        /// MÉTODO PARA AGREGAR UN PRODUCTO AL CARRITO.
        /// </summary>
        /// <param name="_objProducto">Contiene el idProducto y idUsuario</param>
        /// <returns>Retorna el estado de la consulta y la cantidad de productos agregados al carrito del usuario</returns>
        public object AgregarProductoCarrito(int _iIdProducto, int _iIdUsuario)
        {
            bool _bEstadoOperacion;
            string _cMensaje;

            try
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
                {
                    if (!VerificarProductosEnCarrito(_ctx, _iIdProducto, _iIdUsuario))
                    {
                        tblCarrito _objTablaCarrito = new tblCarrito
                        {
                            iIdProducto = _iIdProducto,
                            iIdUsuario = _iIdUsuario,
                            lEstatus = false,
                            dtFechaSeleccion = DateTime.Today
                        };

                        _ctx.tblCarrito.Add(_objTablaCarrito);
                        _ctx.SaveChanges();

                        _bEstadoOperacion = true;
                        _cMensaje = "Producto agregado al carrito";
                    }
                    else
                    {
                        _bEstadoOperacion = false;
                        _cMensaje = "El producto ya se ha agregado en el carrito";
                    }


                }
            }
            catch (Exception)
            {
                _bEstadoOperacion = false;
                _cMensaje = "Algo falló al agregar el producto al carrito";
            }
            return (new { _bEstadoOperacion, _cMensaje });
        }

        /// <summary>
        /// MÉTODO PARA CAMBIAR EL ESTADO DE LOS PRODUCTOS DEL CARRITO AL REALIZAR EL PAGO.
        /// </summary>
        /// <param name="_ctx">Recibe el contexto de la base de datos</param>
        /// <param name="_lstProducto">Recibe la lista de los productos a afectar</param>
        public List<tblCarrito> CambiarEstadoProductoCarrito(VerkoopDBEntities _ctx, List<tblProductoComprado> _lstProducto, int _iIdUsuario)
        {
            List<tblCarrito> _lstCarritoAfectado = new List<tblCarrito>();

            _lstProducto.ForEach(x =>
            {
                tblCarrito _objCarrito = _ctx.tblCarrito.Where(z => z.iIdProducto == x.iIdProducto && z.iIdUsuario == _iIdUsuario).FirstOrDefault();

                _objCarrito.lEstatus = true;

                _lstCarritoAfectado.Add(_objCarrito);

            });

            return _lstCarritoAfectado;
        }

        /// <summary>
        /// MÉTODO PARA OBTENER LOS PRODUCTOS AGREGADOS DEL CLIENTE.
        /// </summary>
        /// <param name="_ctx">Recibe el contexto de la base de datos</param>
        /// <param name="_iIdUsuario">Recibe el id del usuario</param>
        /// <returns>Retorna el valor entero de los productos agregados al carrito</returns>
        public int ObtenerNumeroTotalProductosDeUsuario(int _iIdUsuario)
        {

            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                int _iProductosCarrito = (from carrito in _ctx.tblCarrito.AsNoTracking()
                                          where carrito.iIdUsuario == _iIdUsuario
                                          && carrito.lEstatus == false
                                          select carrito.iIdUsuario).Count();

                return _iProductosCarrito;

            }

        }

        /// <summary>
        /// MÉTODO PARA VISUALIZAR PRODUCTOS EN EL CARRITO.
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
                                 && Carrito.lEstatus == false
                                 join Producto in _ctx.tblCat_Producto
                                 on Carrito.iIdProducto equals Producto.iIdProducto
                                 select new ProductoEnCarritoDTO
                                 {
                                     iIdProducto = Producto.iIdProducto,
                                     iIdCarrito = Carrito.iIdCarrito,
                                     cImagenCarrito = Producto.cImagen,
                                     cNombreproducto = Producto.cNombre,
                                     dPrecioProducto = Producto.dPrecio,
                                     iCantidad = Producto.iCantidad


                                 }).ToList();
            }
            return _lstProductos;
        }

        /// <summary>
        /// MÉTODO PARA QUITAR PRODUCTO DEL CARRITO.
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
        /// MÉTODO PARA REALIZAR EL PAGO DE LOS PRODUCTOS AGREGADOS AL CARRITO.
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

                        List<tblCarrito> _lstCarritoAfectado = CambiarEstadoProductoCarrito(_ctx, _objPago.lstProductoComprado, _iIdUsuario);//Cambia estado del producto a true indicando que el producto se ha comprado.
                                                                                                                                //List<tblCat_Producto> _lstProductoAfectado = ProductoBusiness.DisminuirCantidadProducto(_ctx, _objPago.lstProductoComprado); //Resta a la cantidad disponible del producto la cantidad asignada en la compra.

                        _ctx.SaveChanges();

                        CompraBusiness compraBusiness = new CompraBusiness();

                        byte[] _bPDF = compraBusiness.ImprimirTicketDeCompra(_TablaCompra.iIdCompra);
                        CorreoBusiness correoBusiness = new CorreoBusiness();

                        tblSesion _objSesion = _ctx.tblSesion.AsNoTracking().Where(x => x.iIdUsuario == _iIdUsuario).FirstOrDefault();

                        correoBusiness.EnviarTicketCompra(_objSesion.cCorreo, _bPDF);

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

        /// <summary>
        /// MÉTODO PARA OBTENER LOS PRODUCTOS AGREGADOS AL CARRITO.
        /// </summary>
        /// <param name="Productos">Recibe el objecto con los productos</param>
        /// <returns></returns>
        public PagoPaypalDTO ObtenerProductosCarrito(PagoPaypalDTO Productos)
        {

            List<ProductoPaypalDTO> lstProducto = new List<ProductoPaypalDTO>();

            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                Productos.lstProducto.ForEach(x =>
                {
                    ProductoPaypalDTO ob = (from producto in _ctx.tblCat_Producto
                                            where producto.iIdProducto == x.iIdProducto
                                            select new ProductoPaypalDTO
                                            {
                                                iIdProducto = producto.iIdProducto,
                                                iCantidad = x.iCantidad,
                                                cNombre = producto.cNombre,
                                                dPrecio = producto.dPrecio
                                            }).SingleOrDefault();

                    lstProducto.Add(ob);
                });

                Productos.lstProducto = lstProducto;

                Productos.dPrecioTotal = Productos.lstProducto.Sum(x => x.dPrecio * x.iCantidad);

            }

            return Productos;
        }

        /// <summary>
        /// MÉTODO PARA VERIFICAR LOS PRODUCTOS EN EL CARRITO.
        /// </summary>
        /// <param name="_ctx"></param>
        /// <param name="_iIdProducto"></param>
        /// <param name="_iIdUsuario"></param>
        /// <returns></returns>
        public bool VerificarProductosEnCarrito(VerkoopDBEntities _ctx, int _iIdProducto, int _iIdUsuario)
        {

            bool _bCoincidencia = _ctx.tblCarrito.Any(x => x.iIdProducto == _iIdProducto && x.iIdUsuario == _iIdUsuario && x.lEstatus== false);

            return _bCoincidencia;

        }
    }
}
