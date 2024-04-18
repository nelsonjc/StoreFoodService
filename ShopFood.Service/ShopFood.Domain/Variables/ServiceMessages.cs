namespace ShopFood.Domain.Variables
{
    public static class ServiceMessages
    {
        public static string OK { get { return "Process successfully executed"; } }
        public static string ERROR { get { return "Internal server error"; } }
        public static string UNAUTHORIZED { get { return "Unauthorized"; } }
        public static string FAIL { get { return "Error executing the process"; } }
        public static string NOT_FOUND { get { return "Not Found"; } }
        public static string ERROR_METHOD { get { return "An error occurred in the method"; } }
    }
}
