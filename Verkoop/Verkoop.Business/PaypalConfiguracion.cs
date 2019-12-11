using PayPal.Api;
using System.Collections.Generic;

namespace Verkoop.Business
{
    /// <summary>
    /// Contiene la configuración de paypal.
    /// </summary>
    public class PaypalConfiguracion
    {
        /// <summary>
        /// Variables para almacenar el Id del cliente y su llave secreta.
        /// </summary>
        public readonly static string ClientId;
        public readonly static string ClientSecret;

        /// <summary>
        /// Constructor.
        /// </summary>
        static PaypalConfiguracion()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }
 
        /// <summary>
        /// Obtiene las propiedades del web.config.
        /// </summary>
        /// <returns>Devuelve las propiedades.</returns>
        public static Dictionary<string, string> GetConfig()
        {
            return ConfigManager.Instance.GetProperties();
        }

        /// <summary>
        /// Obtiene el token de acceso de Paypal.
        /// </summary>
        /// <returns>Devuelve el token de acceso.</returns>
        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential
        (ClientId, ClientSecret, GetConfig()).GetAccessToken();

            return accessToken;
        }

        /// <summary>
        /// Obtiene el contexto del Api de Paypal con el token de acceso.
        /// </summary>
        /// <returns>Devuelve el contexto de la Api.</returns>
        public static APIContext GetAPIContext()
        {
            APIContext apiContext = new APIContext(GetAccessToken())
            {
                Config = GetConfig()
            };
            return apiContext;
        }

    }
}
