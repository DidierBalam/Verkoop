using System.Net;
using System.Net.Mail;
using System.IO;
using System;

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


        /// <summary>
        /// MÉTODO PARA ENVIAR TICKET DE COMPRA POR CORREO.
        /// </summary>
        /// /// <param name="_cCorreo">Recibe el correo de destino</param>
        /// <param name="Pdf">Recibe el pdf</param>
        /// <returns>Retorna el estado de la operación</returns>
        public bool EnviarTicketCompra(string _cCorreo, byte[] _Pdf)
        {
            bool bEstadoOperacion;

            Attachment pdfAttachment = new Attachment(new MemoryStream(_Pdf), "TicketCompra.pdf");
            
            pdfAttachment.ContentType.MediaType = System.Net.Mime.MediaTypeNames.Application.Pdf;

            try
            {
                MailAddress Usuario = new MailAddress(_cCorreo);

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
