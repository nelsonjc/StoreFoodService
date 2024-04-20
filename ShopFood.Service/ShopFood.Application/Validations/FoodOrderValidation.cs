using ShopFood.Domain.DTOs.Mail;
using Throw;

namespace ShopFood.Application.Validations
{
    /// <summary>
    /// Class to validate elements of Food Order
    /// </summary>
    public static class FoodOrderValidation
    {
        /// <summary>
        /// Method to do validation of Mail Configuration
        /// </summary>
        /// <param name="mailConfig">Parameter with data of mail configuration</param>
        /// <exception cref="Exception">Return can a exception if it does not comply with the validation</exception>
        public static void MailConfigurationValidate(MailConfigDto mailConfig)
        {
            mailConfig.Throw().IfNull(x => x);
            mailConfig.Remittance.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEmpty()
                .IfWhiteSpace();
            mailConfig.Password.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEmpty()
                .IfWhiteSpace();
            mailConfig.DisplayName.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                 .IfEmpty()
                 .IfWhiteSpace();
            mailConfig.Host.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEmpty()
                .IfWhiteSpace();
            mailConfig.Port.Throw(paramName => throw new Exception($"Param name: {paramName}. Int should not be negative or zero."))
             .IfNegativeOrZero();
        }
    }
}
