namespace ShopFood.Domain.Variables
{
    /// <summary>
    /// Class to variables of app config
    /// </summary>
    public static class AppConfig
    {

        #region Json Web Token
        public static string AppSetting_JWT_Section { get { return "Jwt"; } }
        public static string AppSetting_JWT_Secret { get { return "Secret"; } }
        public static string AppSetting_JWT_Audience { get { return "Audience"; } }
        public static string AppSetting_JWT_Issuer { get { return "Issuer"; } }
        public static string AppSetting_JWT_ExpirationInMinutes { get { return "ExpirationInMinutes"; } }
        #endregion

        public static string AppsettingsWithExt { get { return "appsettings.json"; } }                
        public static string AppSeeting_ConnectionString { get { return "ConnectionStrings:DbConnectionStrings"; } }
        public static string AppInsights_ConnectionString { get { return "APPLICATIONINSIGHTS_CONNECTION_STRING"; } }        
        public static string KeyVaultUri { get { return "VaultUri"; } }        
        public static string CultureName { get { return "es-Co"; } }
        public static string TamplateConfirm { get { return "TamplateConfirm"; } }
        public static string DateFormatLong { get { return "dddd, dd MMMM yyyy";  } }
        public static string HTMLTextFormatting { get { return "text/html"; } }
        public static string JSONFormatting { get { return "application/json"; } }        
        public static string AppSetting_IsDevelopment { get { return "IsDevelopment"; } }
        public static string AppSetting_MailConfig { get { return "MailConfig"; } }
        public static string AppSetting_Remittance { get { return "Remittance"; } }
        public static string AppSetting_Password { get { return "Password"; } }
        public static string AppSetting_DisplayName { get { return "DisplayName"; } }
        public static string AppSetting_Host { get { return "Host"; } }
        public static string AppSetting_Port { get { return "Port"; } }
        public static string AppSetting_DefaultCredentials { get { return "DefaultCredentials"; } }
        public static string AppSetting_EnableSSL { get { return "EnableSSL"; } }
        public static string AppSetting_ApiBaseUrl { get { return "ApiBaseUrl"; } }
    }
}
