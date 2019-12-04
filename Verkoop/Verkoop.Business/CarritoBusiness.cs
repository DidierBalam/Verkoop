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
            int _iProductosCarrito = 0;
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

                    _iProductosCarrito = (from carrito in _ctx.tblCarrito.AsNoTracking()
                                          where carrito.iIdUsuario == _iIdUsuario
                                          && carrito.lEstatus == false
                                          select carrito.iIdUsuario).Count();

                    _EstadoConsulta = true;
                    _cMensaje = "Producto agregado al carrito";
                }
            }
            catch (Exception)
            {
                _iProductosCarrito = 0;
                _EstadoConsulta = false;
                _cMensaje = "Algo falló al agregar el producto al carrito";
            }
            return (new { EstadoConsulta = _EstadoConsulta, Mensaje = _cMensaje, ProductosAgregados = _iProductosCarrito });
        }

        /// <summary>
        /// Método para cambiar el estado de los productos del carrito al realizar el pago.
        /// </summary>
        /// <param name="_ctx">Recibe el contexto de la base de datos</param>
        /// <param name="_lstProducto">Recibe la lista de los productos a afectar</param>
        public void CambiarEstadoProductoCarrito(VerkoopDBEntities _ctx, List<tblProductoComprado> _lstProducto)
        {

            List<tblCarrito> lstCarrito = _ctx.tblCarrito.Where(x => _lstProducto.Select(y => y.iIdProducto).Contains(x.iIdProducto)).ToList();

            lstCarrito.ForEach(z =>
            {
                z.lEstatus = false;
            });
           
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
        /// <param name="_iIdUsuario">Recibe el id del del cliente</param>
        /// <param name="_iIdDireccion">Recibe el id de la dirección</param>
        /// <param name="_iIdTarjeta">Recibe el id de la tarjeta</param>
        /// <returns>Retorna el estado de la operación y su mensaje</returns>
        public object RealizarPago(int _iIdUsuario, int _iIdDireccion, tblTarjeta _objTargeta)
        {
            bool bEstadoOperacion;
            string cMensaje;

            try
            {
                

                using (VerkoopDBEntities ctx = new VerkoopDBEntities())
                {
                    tblCompra TablaCompra = new tblCompra
                    {
                        iIdUsuario = _iIdUsuario,
                        iIdDireccion = _iIdDireccion,
                        //iIdTarjeta = _iIdTarjeta,
                        dtFecha = DateTime.Today

                    };

                    List<tblProductoComprado> lstProducto = (from Carrito in ctx.tblCarrito
                                                             where Carrito.iIdUsuario == _iIdUsuario
                                                             || Carrito.lEstatus == false
                                                             select new tblProductoComprado
                                                             {
                                                                 iIdCompra = TablaCompra.iIdCompra,
                                                                 iIdProducto = Carrito.iIdProducto,
                                                                 iCantidad = Carrito.iCantidad

                                                             }).ToList();

                    TablaCompra.tblProductoComprado = lstProducto;
                    ctx.tblCompra.Add(TablaCompra);                    

                    CambiarEstadoProductoCarrito(ctx, lstProducto);

                    ProductoBusiness.DisminuirCantidadProducto(ctx, lstProducto);

                    ctx.SaveChanges();

                    bEstadoOperacion = true;
                    cMensaje = "Pago realizado exitosamente";

                }

            }
            catch (Exception)
            {
                bEstadoOperacion = false;
                cMensaje = "Algo falló al realizar el pago";
            }

            return (new { bEstadoOperacion , cMensaje});
        }
    }
}
