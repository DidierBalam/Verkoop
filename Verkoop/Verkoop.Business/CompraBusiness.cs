using System.Collections.Generic;
using Verkoop.CapaDatos.DTO;
using Verkoop.CapaDatos;
using System.Linq;
//using System.IO;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using System.Web;

namespace Verkoop.Business
{
    public class CompraBusiness
    {
        public bool EnviarCorreo(DetallesCompraClienteDTO _objDatos)
        {

            return false;
        }


        public bool ImprimirTicketDeCompra(DetallesCompraClienteDTO _objDatos)
        {
          

        //    Document doc = new Document(PageSize.LETTER);
        //    // Indicamos donde vamos a guardar el documento
        //    PdfWriter writer = PdfWriter.GetInstance(doc,
        //                                new FileStream(@"C:\prueba.pdf", FileMode.Create));

        //    // Le colocamos el título y el autor
        //    // **Nota: Esto no será visible en el documento
        //    doc.AddTitle("Mi primer PDF");
        //    doc.AddCreator("Roberto Torres");

        //    // Abrimos el archivo
        //    //doc.Open();
            
            return true;
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
            int dato = 0;
            using (VerkoopDBEntities ctx = new VerkoopDBEntities())
            {
                dato = ctx.tblCompra.Count();
            }
            return dato;
        }

        public decimal ObtenerNumeroTotalIngresoVentas()
        {
            decimal dato = 0;
            using (VerkoopDBEntities ctx = new VerkoopDBEntities())
            {
                dato = ctx.tblCompra.Select(i => i.dPrecioTotal).Sum();
            }
            return dato;
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
