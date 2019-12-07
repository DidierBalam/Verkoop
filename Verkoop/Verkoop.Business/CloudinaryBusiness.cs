using System.Web;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace Verkoop.Business
{
    public class CloudinaryBusiness
    {
        static Account Credenciales = new Account(
                "drab09by4",
                "858696692252385",
                "uL9hpzVxdpcrT2f-j5se8fkBlDA"
                );

        Cloudinary Cloudinary = new Cloudinary(Credenciales);

        /// <summary>
        /// Método para subir foto de perfil a Cloudinary
        /// </summary>
        /// <param name="_Imagen">Recibe la imagen</param>
        /// <returns>Retorna un objeto Json que contiene la propiedad StatusCode de tipo System.Net.HttpStatusCode para manejar errores y la propiedad JsonObj de tipo Newtonsoft.Json.Linq.JToke</returns>
        public string SubirFotoPerfil(HttpPostedFileBase _Imagen, int _iIdUsuario)
        {
            string _cRuta = "Verkoop/Perfil/VrkFpUs" + _iIdUsuario + "";

            ImageUploadResult _Respuesta = SubirImagenCloudinary(_Imagen, _cRuta);

            return _Respuesta.Uri.ToString();
        }

        /// <summary>
        /// Método para subir imagen de producto a cloudinary
        /// </summary>
        /// <param name="_Imagen">Recibe la imagen</param>
        /// <param name="_iIdProducto">Recibe el id del producto</param>
        /// <returns>Retorna un objeto Json que contiene la propiedad StatusCode de tipo System.Net.HttpStatusCode para manejar errores y la propiedad JsonObj de tipo Newtonsoft.Json.Linq.JToke</returns>
        public string SubirImagenProducto(HttpPostedFileBase _Imagen, int _iIdProducto)
        {
            string _cRuta = "Verkoop/Producto/VrkImgPd" + _iIdProducto + "";

            ImageUploadResult _Respuesta = SubirImagenCloudinary(_Imagen, _cRuta);

            return _Respuesta.Uri.ToString();
        }


        /// <summary>
        /// Método para subir una imagen a Cloudinary
        /// </summary>
        /// <param name="_Imagen">Recibe a la imagen</param>
        /// <param name="_iIdProducto">Recibe el id del sujeto</param>
        /// <returns>Retorna un objeto Json que contiene la propiedad StatusCode de tipo System.Net.HttpStatusCode para manejar errores y la propiedad JsonObj de tipo Newtonsoft.Json.Linq.JToke</returns>
        public ImageUploadResult SubirImagenCloudinary(HttpPostedFileBase _Imagen, string _cRuta)
        {
            ImageUploadParams _objImagen = new ImageUploadParams()
            {
                File = new FileDescription(_Imagen.FileName, _Imagen.InputStream),
                PublicId = _cRuta,
                Transformation = new Transformation()
                  .Width(400)
                  .Height(400)
                  .FetchFormat("jpg")
            };

            ImageUploadResult _objResultado = Cloudinary.Upload(_objImagen);

            return _objResultado;
        }
    }
}
