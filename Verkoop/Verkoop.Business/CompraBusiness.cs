using System.Collections.Generic;
using Verkoop.CapaDatos.DTO;
using Verkoop.CapaDatos;
using System.Linq;
namespace Verkoop.Business
{
    public class CompraBusiness
    {
        public bool EnviarCorreo(DetallesCompraClienteDTO _objDatos)
        {

            return false;
        }


        public string GenerarTicketDeCompra(DetallesCompraClienteDTO _objDatos)
        {

            return "";
        }
        /// <summary>
        /// Método para Visualizar las compras del cliente
        /// </summary>
        /// <param name="_iIdUsuario">Contiene el idUsuario</param>
        /// <returns>Retorna la lista de las compras</returns>
        public List<CompraDeClienteDTO> ObtenerComprasDeCliente(int _iIdUsuario)
        {
            List<CompraDeClienteDTO> _lstCompras = new List<CompraDeClienteDTO>();
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                _lstCompras = (from Compra in _ctx.tblCompra
                               where Compra.iIdUsuario == _iIdUsuario
                               select new CompraDeClienteDTO
                               {
                                   iIdCompra = Compra.iIdCompra,
                                   dtFecha = Compra.dtFecha.ToString(),
                                   ProductoComprado = (from Producto in _ctx.tblProductoComprado
                                                       where Producto.iIdCompra == Compra.iIdCompra
                                                       join CatalogoProducto in _ctx.tblCat_Producto
                                                       on Producto.iIdProducto equals CatalogoProducto.iIdProducto
                                                       select new ProductoCompradoDTO
                                                       {
                                                           iCantidad = Producto.iCantidad,
                                                           dPrecio = Producto.iCantidad * CatalogoProducto.dPrecio,
                                                           cNombre = CatalogoProducto.cNombre,
                                                           cImagen = CatalogoProducto.cImagen
                                                       }).ToList(),
                                   dPrecio = Compra.tblProductoComprado.Select(z => z.iCantidad * z.tblCat_Producto.dPrecio).Sum()
                               }).ToList();
            }
            return _lstCompras.ToList();
        }

        public int ObtenerNumeroTotalCompras()
        {

            return 0;
        }

        public decimal ObtenerNumeroTotalIngresoVentas()
        {

            return 0;
        }

        public List<ListaCompraClienteDTO> ObtenerTodasLasCompras()
        {

            return null;
        }

        public object ObtenerUsuariosMasCompras()
        {

            return null;
        }

    }
}
