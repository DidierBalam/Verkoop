using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.html;
using System.Text;
using Verkoop.CapaDatos.DTO;
using Verkoop.CapaDatos;

namespace Verkoop.Business
{
    public class CompraBusiness
    {
        public bool EnviarCorreo(DetallesCompraClienteDTO _objDatos)
        {

            return false;
        }

        public byte[] GenerarTicketDeCompra(DetallesCompraClienteDTO _objDatos)
        {

            MemoryStream ms = new MemoryStream(); //Creación del MemoryStream.

            Document doc = new Document(PageSize.A4, 25, 25, 25, 25); // Creación del documento de iTextSharp.

            PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms); //Creación de la instancia del archivo PDF, usando el documento y el MemoryStream.

            HtmlPipelineContext htmlContext = new HtmlPipelineContext(null); //Creación del contexto Pipeline para contenido HTML.

            htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory()); //Añade el procesador de etiquetas HTML al contexto de HTML.

            ICSSResolver cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false); //Manejo de las propiedades CSS.

            cssResolver.AddCssFile(HttpContext.Current.Server.MapPath("~/Assets/Bootstrap4.3/bootstrap.min.css"), true); //Añadir archivo con propiedades CSS.

            IPipeline pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(htmlContext, new PdfWriterPipeline(doc, oPdfWriter))); //Creación del pipeline para CSS, sin este pipeline no funcionará el CSS.

            XMLWorker worker = new XMLWorker(pipeline, true); //Creación del XMLWorker.

            XMLParser xmlParser = new XMLParser(worker); // Añadir un analizador al XMLWorker.

            doc.Open(); // Se abre el documento.

            doc.Add(new Chunk("")); //Se añade un espacio en blanco para evitar que ocurra un error por falta de páginas.

            StringBuilder estructuraHTML = new StringBuilder(); //Creación de una variable que contiene toda la estructura de HTML.

            estructuraHTML.Append("<html>");

            estructuraHTML.Append("<body>");

            // Logo a la izquierda del documento
            estructuraHTML.Append("<div class='text-left'>");

            estructuraHTML.Append("<img src='https://res.cloudinary.com/drab09by4/image/upload/v1575483281/Verkoop/Predeterminado/logoVerkoop_xanvsl.png' height='100'/>");

            estructuraHTML.Append("</div>"); //.

            estructuraHTML.Append("<div class='text-center'>");

            estructuraHTML.Append("<strong><p style='font-size: 40px;'>Ticket de Compra</p></strong>"); // Título.

            estructuraHTML.Append("</div>");

            estructuraHTML.Append("<span>&#8203;</span>"); // Espacio debajo del título.
            estructuraHTML.Append("" +
                "" +
                "" +
                ".");

            estructuraHTML.Append("<div class='text-left mb-5'>");
            estructuraHTML.Append("<table class='table table-borderless'>"); // Estilo de la tabla.
            estructuraHTML.Append("<thead>");

            estructuraHTML.Append("<tr>");

            estructuraHTML.Append("<th scope='col' class='align-middle'>Nombre: Angel</th>"); // Información del cliente.
            estructuraHTML.Append("<th scope='col' class='align-middle'>Correo: angel.15070027@itsmotul.edu.mx</th>"); // Información del cliente.

            estructuraHTML.Append("</tr>");


            estructuraHTML.Append("</thead>");

            estructuraHTML.Append("<tbody>");

            estructuraHTML.Append("<tr>");

            estructuraHTML.Append("<th scope='col' class='align-middle'>Dirección: ########################################</th>"); // Información del cliente.
            estructuraHTML.Append("<th scope='col' class='align-middle'>Fecha: 05/12/2019</th>"); // Información del cliente.

            estructuraHTML.Append("</tr>");

            estructuraHTML.Append("</tbody>");
            estructuraHTML.Append("</table>");

            estructuraHTML.Append("</div>");

            estructuraHTML.Append("" +
                "" +
                "" +
                "."); // Espacio después de la información del cliente.

            estructuraHTML.Append("<div class='text-center'>");

            estructuraHTML.Append("<table class='table table-borderless'>"); // Estilo de la tabla de productos.
            estructuraHTML.Append("<thead>");
            estructuraHTML.Append("<tr>");

            estructuraHTML.Append("<th scope='col'><strong>Imagen</strong></th>"); // Columna principal.
            estructuraHTML.Append("<th scope='col'><strong>Nombre</strong></th>"); // Columna principal.
            estructuraHTML.Append("<th scope='col'><strong>Cantidad</strong></th>"); // Columna principal.
            estructuraHTML.Append("<th scope='col'><strong>Precio</strong></th>"); // Columna principal.

            estructuraHTML.Append("</tr>");
            estructuraHTML.Append("</thead>");
            estructuraHTML.Append("<tbody>");

            string xiaomi = "xiaomi 9t pro";

            for (int i = 1; i < 6; i++) // Filas dinámicas de la tabla.
            {
                estructuraHTML.Append("<tr>");

                estructuraHTML.Append("<td class='align-middle' style='height: 120px!important;' scope='row'><img src='https://http2.mlstatic.com/rosario-celular-xiaomi-redmi-7-64gb-3gb-ds-nuevo-D_NQ_NP_751015-MLA31098066162_062019-Q.jpg' height='100'/></td>");
                estructuraHTML.Append("<td class='align-middle' style='height: 120px!important;'>" + xiaomi + "</td>");
                estructuraHTML.Append("<td class='align-middle'style='height: 120px!important;'>1</td>");
                estructuraHTML.Append("<td class='align-middle' style='height: 120px!important;'>3999</td>");

                estructuraHTML.Append("</tr>");
            }

            estructuraHTML.Append("<tr>"); // Fila del precio total.

            estructuraHTML.Append("<td class='align-middle' style='height: 120px!important;' scope='row'></td>");
            estructuraHTML.Append("<td class='align-middle' style='height: 120px!important;'></td>");
            estructuraHTML.Append("<td class='align-middle'style='height: 120px!important;'></td>");
            estructuraHTML.Append("<td class='align-middle' style='height: 120px!important;'>Precio Total: $9000.00</td>");

            estructuraHTML.Append("</tr>");

            estructuraHTML.Append("</tbody>");
            estructuraHTML.Append("</table>");
            estructuraHTML.Append("</div>");
            estructuraHTML.Append("</body>");
            estructuraHTML.Append("</html>"); // Finalización del documento.

            XMLParser contenidoPDF = new XMLParser(true, worker, Encoding.GetEncoding("UTF-8")); // añadir contenido HTML/XHTML para procesar.

            contenidoPDF.Parse(new StringReader(estructuraHTML.ToString())); // Convertir el contenido a String para el documento.

            doc.Close(); // Cerrar el documento.

            byte[] bPDF = ms.ToArray(); // Añadir contenido del MemoryStream a una variable byte.

            return bPDF;

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
