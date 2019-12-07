using System.Net;
using System.Net.Mail;
using System.IO;
using System;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace Verkoop.Business
{
    public class CorreoBusiness
    {
        /// <summary>
        /// Credenciales
        /// </summary>
        MailAddress Emisor = new MailAddress("didier.15070005@itsmotul.edu.mx");

        const string cContraseña = "didierjose12345";

        /// <summary>
        /// Configuración
        /// </summary>
        SmtpClient smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,

        };

        /// <summary>
        /// Método para enviar código de verificación de usuario
        /// </summary>
        /// <param name="_cCorreo"></param>
        /// <param name="_cCodigoVerificacion"></param>
        public void EnviarCódigoVerificacion(string _cCorreo, string _cCodigoVerificacion)
        {
            MailAddress Usuario = new MailAddress(_cCorreo);

            string _cAsunto = "Código de verificación";
            string _cMensaje = _cCodigoVerificacion;

            smtp.Credentials = new NetworkCredential(Emisor.Address, cContraseña);

            using (MailMessage message = new MailMessage(Emisor, Usuario)
            {
                Subject = _cAsunto,
                Body = _cMensaje
            })

                smtp.Send(message);
        }


        public byte[] GenerarTicketCompra(/*DTO compra,  */)
        {
          
            Document document = new Document(PageSize.A4, 88f, 88f, 10f, 10f);

            //Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
            using (MemoryStream memoryStream = new MemoryStream())
            {

                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                //Color color = null;

                document.Open();
                document.Add(new Chunk(""));
                //Header Table
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.3f, 0.7f });

                document.Add(table);
                document.Close();


                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                return bytes;

                //MailMessage Msg = new MailMessage();
                //MailAddress fromMail = new MailAddress("frommailid");
                //// Sender e-mail address.
                //Msg.From = fromMail;
                //// Recipient e-mail address.
                //Msg.To.Add(new MailAddress("tomailid"));
                //Msg.CC.Add(new MailAddress("ccmailid"));
                //// Subject of e-mail
                //Msg.Subject = "subject";
                //Msg.Body = "Msg Body";

                //Msg.Attachments.Add(new Attachment(new MemoryStream(bytes), "CC" + result + ".pdf"));
                //Msg.IsBodyHtml = true;

                //SmtpClient smtps = new SmtpClient("smtp.mailid.com", 2525);
                //smtps.UseDefaultCredentials = false;
                //smtps.Credentials = new NetworkCredential("username", "password");

                //smtps.EnableSsl = true;
                //smtps.Send(Msg);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool EnviarTicketCompra()
        {
            byte[] pdf = GenerarTicketCompra();

            bool bEstadoOperacion;

            Attachment pdfAttachment = new Attachment(new MemoryStream(pdf), "tickect.pdf");
            
            pdfAttachment.ContentType.MediaType = System.Net.Mime.MediaTypeNames.Application.Pdf;


            try
            {
                MailAddress Usuario = new MailAddress("angel.15070027@itsmotul.edu.mx");

                string _cAsunto = "Ticket de compra";

                smtp.Credentials = new NetworkCredential(Emisor.Address, cContraseña);

                using (MailMessage message = new MailMessage(Emisor, Usuario)
                {
                    Subject = _cAsunto,

                })
                {
                    message.Attachments.Add(pdfAttachment);

                    smtp.Send(message);
                }

                bEstadoOperacion= true;
            }
            catch (Exception)
            {
                bEstadoOperacion = false;
            }

            return bEstadoOperacion;
        }
    }
}
