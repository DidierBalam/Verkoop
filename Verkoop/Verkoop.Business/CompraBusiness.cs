using System.Collections.Generic;
using Verkoop.CapaDatos.DTO;
using Verkoop.CapaDatos;
using System.Linq;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using System.Text;
using System.Web;
using iTextSharp.tool.xml.pipeline.end;
using System;

namespace Verkoop.Business
{
    public class CompraBusiness
    {
        CorreoBusiness CorreoBusiness = new CorreoBusiness();

        /// <summary>
        /// Método que crea un documento PDF.
        /// </summary>
        /// <param name="_iIdCompra">Identificador de la compra.</param>
        /// <returns>Devuelve un array de bytes.</returns>
        public byte[] ImprimirTicketDeCompra(int _iIdCompra)
        {
            byte[] _bPDF;

            try
            {
                TicketCompraDTO _objUsuarioCompra = ObtenerDetallesCompra(_iIdCompra);

                _bPDF = GenerarTicketCompra(_objUsuarioCompra);
            }
            catch(Exception)
            {
                _bPDF = null;
            }
            
            return _bPDF;

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

        /// <summary>
        /// Obtiene los detalles de la compra de un cliente.
        /// </summary>
        /// <param name="_iIdCompra">Se utiliza para saber los detalles del cliente.</param>
        /// <returns>Devuelve un objeto con los detalles de compra.</returns>
        public TicketCompraDTO ObtenerDetallesCompra(int _iIdCompra)
        {

            TicketCompraDTO _objCompra = new TicketCompraDTO();

            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                _objCompra = (from Compra in _ctx.tblCompra
                              where Compra.iIdUsuario == _iIdCompra
                              select new TicketCompraDTO
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
                                  dPrecio = Compra.tblProductoComprado.Select(z => z.iCantidad * z.tblCat_Producto.dPrecio).Sum(),
                                  objUsuario = (from usuario in _ctx.tblCat_Usuario
                                                where usuario.iIdUsuario == Compra.iIdUsuario
                                                join correo in _ctx.tblSesion
                                                on usuario.iIdUsuario equals correo.iIdUsuario
                                                join direccion in _ctx.tblDireccion
                                                on usuario.iIdUsuario equals direccion.iIdUsuario
                                                select new DatosUsuarioTicketDTO
                                                {
                                                    cNombre = usuario.cNombre,
                                                    cCorreo = correo.cCorreo,
                                                    cDireccion = direccion.cDireccion
                                                }).FirstOrDefault()
                              }).SingleOrDefault();
            }

            return _objCompra;
        }

        /// <summary>
        /// Método que genera el contenido del documento PDF.
        /// </summary>
        /// <param name="_objCompra">Contiene los datos de la compra de un cliente.</param>
        /// <returns>Devuelve un array de bytes que contienen el documento PDF.</returns>
        public byte[] GenerarTicketCompra(TicketCompraDTO _objCompra)
        {
            string _cNombreCliente = _objCompra.objUsuario.cNombre.ToString();
            string _cCorreoCliente = _objCompra.objUsuario.cCorreo.ToString();
            string _cDireccionCliente = _objCompra.objUsuario.cDireccion.ToString();
            string _cFechaCompra = _objCompra.dtFecha.ToString();

            MemoryStream _ms = new MemoryStream(); //Creación del MemoryStream.

            Document _documento = new Document(PageSize.A4, 25, 25, 25, 25); // Creación del documento de iTextSharp.

            PdfWriter _oPdfWriter = PdfWriter.GetInstance(_documento, _ms); //Creación de la instancia del archivo PDF, usando el documento y el MemoryStream.

            HtmlPipelineContext _htmlContext = new HtmlPipelineContext(null); //Creación del contexto Pipeline para contenido HTML.

            _htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory()); //Añade el procesador de etiquetas HTML al contexto de HTML.

            ICSSResolver _cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false); //Manejo de las propiedades CSS.

            _cssResolver.AddCssFile(HttpContext.Current.Server.MapPath("~/Assets/css/bootstrap.min.css"), true); //Añadir archivo con propiedades CSS.

            IPipeline _pipeline = new CssResolverPipeline(_cssResolver, new HtmlPipeline(_htmlContext, new PdfWriterPipeline(_documento, _oPdfWriter))); //Creación del pipeline para CSS, sin este pipeline no funcionará el CSS.

            XMLWorker _worker = new XMLWorker(_pipeline, true); //Creación del XMLWorker.

            XMLParser _xmlParser = new XMLParser(_worker); // Añadir un analizador al XMLWorker.

            _documento.Open(); // Se abre el documento.

            _documento.Add(new Chunk("")); //Se añade un espacio en blanco para evitar que ocurra un error por falta de páginas.

            StringBuilder _estructuraHTML = new StringBuilder(); //Creación de una variable que contiene toda la estructura de HTML.

            _estructuraHTML.Append("<html>");

            _estructuraHTML.Append("<body>");

            // Logo a la izquierda del documento
            _estructuraHTML.Append("<div class='text-left'>");

            _estructuraHTML.Append("<img src='https://res.cloudinary.com/drab09by4/image/upload/v1575483281/Verkoop/Predeterminado/logoVerkoop_xanvsl.png' height='100'/>");

            _estructuraHTML.Append("</div>"); //.

            _estructuraHTML.Append("<div class='text-center'>");

            _estructuraHTML.Append("<strong><p style='font-size: 40px;'>Ticket de Compra</p></strong>"); // Título.

            _estructuraHTML.Append("</div>");

            _estructuraHTML.Append("<span>&#8203;</span>"); // Espacio debajo del título.
            _estructuraHTML.Append("" +
                "" +
                "" +
                ".");

            _estructuraHTML.Append("<div class='text-left mb-5'>");
            _estructuraHTML.Append("<table class='table table-borderless'>"); // Estilo de la tabla.
            _estructuraHTML.Append("<thead>");

            _estructuraHTML.Append("<tr>");

            _estructuraHTML.Append("<th scope='col' class='align-middle'>Nombre: " + _cNombreCliente + "</th>"); // Información del cliente.
            _estructuraHTML.Append("<th scope='col' class='align-middle'>Correo: " + _cCorreoCliente + "</th>"); // Información del cliente.

            _estructuraHTML.Append("</tr>");


            _estructuraHTML.Append("</thead>");

            _estructuraHTML.Append("<tbody>");

            _estructuraHTML.Append("<tr>");

            _estructuraHTML.Append("<th scope='col' class='align-middle'>Dirección: " + _cDireccionCliente + "</th>"); // Información del cliente.
            _estructuraHTML.Append("<th scope='col' class='align-middle'>Fecha: " + _cFechaCompra + "</th>"); // Información del cliente.

            _estructuraHTML.Append("</tr>");

            _estructuraHTML.Append("</tbody>");
            _estructuraHTML.Append("</table>");

            _estructuraHTML.Append("</div>");

            _estructuraHTML.Append("" +
                "" +
                "" +
                "."); // Espacio después de la información del cliente.

            _estructuraHTML.Append("<div class='text-center'>");

            _estructuraHTML.Append("<table class='table table-borderless'>"); // Estilo de la tabla de productos.
            _estructuraHTML.Append("<thead>");
            _estructuraHTML.Append("<tr>");

            _estructuraHTML.Append("<th scope='col'><strong>Imagen</strong></th>"); // Columna principal.
            _estructuraHTML.Append("<th scope='col'><strong>Nombre</strong></th>"); // Columna principal.
            _estructuraHTML.Append("<th scope='col'><strong>Cantidad</strong></th>"); // Columna principal.
            _estructuraHTML.Append("<th scope='col'><strong>Precio</strong></th>"); // Columna principal.

            _estructuraHTML.Append("</tr>");
            _estructuraHTML.Append("</thead>");
            _estructuraHTML.Append("<tbody>");

            _objCompra.ProductoComprado.ForEach(x => // Filas dinámicas de la tabla.
            {
                _estructuraHTML.Append("<tr>");

                _estructuraHTML.Append("<td class='align-middle' style='height: 120px!important;' scope='row'><img src='" + x.cImagen + "' height='100'/></td>");
                _estructuraHTML.Append("<td class='align-middle' style='height: 120px!important;'>" + x.cNombre + "</td>");
                _estructuraHTML.Append("<td class='align-middle'style='height: 120px!important;'>" + x.iCantidad + "</td>");
                _estructuraHTML.Append("<td class='align-middle' style='height: 120px!important;'>" + x.dPrecio + "</td>");

                _estructuraHTML.Append("</tr>");
            });

            _estructuraHTML.Append("<tr>"); // Fila del precio total.

            string _cPrecioTotal = _objCompra.dPrecio.ToString();

            _estructuraHTML.Append("<td class='align-middle' style='height: 120px!important;' scope='row'></td>");
            _estructuraHTML.Append("<td class='align-middle' style='height: 120px!important;'></td>");
            _estructuraHTML.Append("<td class='align-middle'style='height: 120px!important;'></td>");
            _estructuraHTML.Append("<td class='align-middle' style='height: 120px!important;'>Precio Total: " + _cPrecioTotal + "</td>");

            _estructuraHTML.Append("</tr>");

            _estructuraHTML.Append("</tbody>");
            _estructuraHTML.Append("</table>");
            _estructuraHTML.Append("</div>");
            _estructuraHTML.Append("</body>");
            _estructuraHTML.Append("</html>"); // Finalización del documento.

            XMLParser _contenidoPDF = new XMLParser(true, _worker, Encoding.GetEncoding("UTF-8")); // añadir contenido HTML/XHTML para procesar.

            _contenidoPDF.Parse(new StringReader(_estructuraHTML.ToString())); // Convertir el contenido a String para el documento.

            _documento.Close(); // Cerrar el documento.

            byte[] _bPDF = _ms.ToArray(); // Añadir contenido del MemoryStream a una variable byte.

            return _bPDF;

        }

    }
}
