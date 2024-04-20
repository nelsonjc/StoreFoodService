namespace ShopFood.Domain.Variables
{
    /// <summary>
    /// Class to Resource Data
    /// </summary>
    public static class ResourceData
    {
        public static string HTMLConfirmOK { get { return "<html><head><meta charset=\"UTF-8\"><meta name=\"viewport\"content=\"width=device-width,initial-scale=1.0\"><title>Confirmación de Pedido</title></head><body><h1>Hola!</h1><p>Tu pedido ha sido confirmado!</p></body></html>"; } }
        public static string TamplateConfirm_Customer { get { return "[CUSTOMER]"; } }
        public static string TamplateConfirm_DateOrder { get { return "[DATE_ORDER]"; } }
        public static string TamplateConfirm_Details { get { return "[DETAILS]"; } }
        public static string TamplateConfirm_Total { get { return "[TOTAL]"; } }
        public static string TamplateConfirm_Link { get { return "[LINK]"; } }
        public static string MailConfirm_Subject { get { return "Confirmación de Pedido - Shop Food"; } }
        public static string LinkConfirmOrder { get { return "FoodOrder/Confirm?id="; } }
    }
}
