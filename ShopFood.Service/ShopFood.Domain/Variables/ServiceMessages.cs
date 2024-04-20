using Azure;

namespace ShopFood.Domain.Variables
{
    /// <summary>
    /// Class to Service Messages Response HTTP
    /// </summary>
    public static class ServiceMessages
    {
        #region HTTP
        public static string OK { get { return "Process successfully executed"; } }
        public static string ERROR { get { return "Internal server error"; } }
        public static string UNAUTHORIZED { get { return "Unauthorized"; } }
        public static string FAIL { get { return "Error executing the process"; } }
        public static string NOT_FOUND { get { return "Not Found"; } }
        public static string ERROR_METHOD { get { return "An error occurred in the method"; } }
        #endregion

        public static string ERROR_MAIL_DESTINY { get { return "No hay destinatarios para enviar el e-mail"; } }

    }
}
