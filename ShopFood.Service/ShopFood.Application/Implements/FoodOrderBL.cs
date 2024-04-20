using Microsoft.Extensions.Configuration;
using ShopFood.Application.Validations;
using ShopFood.Domain.DTOs.Mail;
using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.Entities;
using ShopFood.Domain.Helpers;
using ShopFood.Domain.Interfaces.Application.Implements;
using ShopFood.Domain.Interfaces.Repository;
using ShopFood.Domain.Utils;
using ShopFood.Domain.Variables;
using System.Globalization;

namespace ShopFood.Application.Implements
{
    /// <summary>
    /// Class to Food Order business logic
    /// </summary>
    public class FoodOrderBL : IFoodOrderBL
    {
        #region Variables
        private readonly IConfiguration _configuration;
        private readonly IFoodOrderRepository _foodOrderRepository;
        private readonly IUserBL _userBL;
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor to Food Order bussiness logic
        /// </summary>
        /// <param name="configuration">Parameter configuration type of Microsoft.Extensions.Configuration</param>
        /// <param name="foodOrderRepository">Parameter type of Repository to get and set data base</param>
        /// <param name="userBL">Parameter to get and set data of user business logic</param>
        public FoodOrderBL(IConfiguration configuration, IFoodOrderRepository foodOrderRepository, IUserBL userBL)
        {
            _configuration = configuration;
            _foodOrderRepository = foodOrderRepository;
            _userBL = userBL;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method to do a food order
        /// </summary>
        /// <param name="request">Parameter type of request data</param>
        public async Task InsertAsync(FoodOrderRequest request)
        {
            var resultOrder = await _foodOrderRepository.InsertAsync(request);
            await SendMailConfirmOrder(resultOrder, request.UserCreatedId);
        }

        /// <summary>
        /// Method to do confirm a food order
        /// </summary>
        /// <param name="id">Paremeter type of Guid with id food order head</param>
        /// <returns>Html string content with reesponse</returns>
        public async Task<string> ConfirmAsync(Guid id)
        {
            await _foodOrderRepository.ConfirmAsync(id);
            return ResourceData.HTMLConfirmOK;
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Method for sending an e-mail requesting order confirmation
        /// </summary>
        /// <param name="order">Parameter of type FoodOrderHead with the information in detail of the food order.</param>
        /// <param name="userId">Parameter of type Guid with the id of the user who owns the food order.</param>
        /// <returns></returns>
        private async Task SendMailConfirmOrder(FoodOrderHead order, Guid userId)
        {
            //Get data user
            var userData = await _userBL.GetByIdAsync(userId);

            //Validate if user contains e-mail
            if (!string.IsNullOrEmpty(userData.Username))
            {
                //Build message to send mail
                MailSendNotificationDto message = new()
                {
                    Configuration = await GetMailConfig(),
                    Destinatary = [userData.Username],
                    Subject = ResourceData.MailConfirm_Subject,
                    HTMLBody = await GetTemplateHTMLBodyConfirm(order, userData.Name)
                };

                //Send mail
                NotificationHelper.SendMail(message);
            }
        }

        /// <summary>
        /// Method to get template HTML Body Confirm
        /// </summary>
        /// <param name="order">Parameter of type FoodOrderHead with the information in detail of the food order.</param>
        /// <param name="nameUser">Parameter of type string with the name of the user who owns the food order.</param>
        /// <returns>Template HTML</returns>
        private async Task<string> GetTemplateHTMLBodyConfirm(FoodOrderHead order, string nameUser)
        {
            decimal totalPayable = 0;
            string strDetailTable = string.Empty;

            foreach (var detail in order.FoodOrderDetails)
            {
                string strDetailItem = $"<tr>" +
                    $"<td>{detail.ItemNumber}</td>" +
                    $"<td>{detail.FoodCatalogName}</td>" +
                    $"<td>{detail.FoodCatalogDescription}</td>" +
                    $"<td>{detail.Quantity}</td>" +
                    $"<td>${detail.Price}</td>" +
                    $"<td>${detail.Total}</td>" +
                    $"</tr>";

                strDetailTable = string.IsNullOrEmpty(strDetailTable) ? strDetailItem : $"{strDetailTable} {strDetailItem}";
                totalPayable += detail.Total;
            }

            CultureInfo culture = new(AppConfig.CultureName);
            string pathTemplate = Utils.CombinePaths(_configuration[AppConfig.TamplateConfirm]);

            StreamReader sr = new(pathTemplate);
            string htmlCode = await sr.ReadToEndAsync();
            htmlCode = htmlCode.Replace(ResourceData.TamplateConfirm_Customer, nameUser);
            htmlCode = htmlCode.Replace(ResourceData.TamplateConfirm_DateOrder, order.DateCreated.ToString(AppConfig.DateFormatLong, culture));
            htmlCode = htmlCode.Replace(ResourceData.TamplateConfirm_Details, strDetailTable);
            htmlCode = htmlCode.Replace(ResourceData.TamplateConfirm_Total, totalPayable.ToString("c"));
            htmlCode = htmlCode.Replace(ResourceData.TamplateConfirm_Link, GetLinkConfirmOrder(order.Id));
            sr.Close();

            return htmlCode;
        }

        /// <summary>
        /// Method to get a mail configuration
        /// </summary>
        /// <returns>Mail config data</returns>
        private async Task<MailConfigDto> GetMailConfig()
        {
            var mailSettings = _configuration.GetSection(AppConfig.AppSetting_MailConfig);

            string? remittance = mailSettings[AppConfig.AppSetting_Remittance];
            string? password = await AzureKeyVaultHelper.GetSecretAsync(mailSettings[AppConfig.AppSetting_Password]);
            string? displayName = mailSettings[AppConfig.AppSetting_DisplayName];
            string? host = mailSettings[AppConfig.AppSetting_Host];
            string? port = mailSettings[AppConfig.AppSetting_Port];
            string? defaultCredentials = mailSettings[AppConfig.AppSetting_DefaultCredentials];
            string? enableSSL = mailSettings[AppConfig.AppSetting_EnableSSL];

            var mailConfig = new MailConfigDto()
            {
                Remittance = remittance,
                Password = password,
                DisplayName = displayName,
                Host = host,
                Port = Convert.ToInt32(port),
                DefaultCredentials = Convert.ToBoolean(defaultCredentials),
                EnableSSL = Convert.ToBoolean(enableSSL),
                IsBodyHtml = true
            };

            FoodOrderValidation.MailConfigurationValidate(mailConfig);
            return mailConfig;
        }

        /// <summary>
        /// Method to build link to comfirm a food order
        /// </summary>
        /// <param name="id">Parameter of type Guid with ID of the food order head.</param>
        /// <returns>Link URL result</returns>
        private string GetLinkConfirmOrder(Guid id)
        {
            string baseUrl = _configuration[AppConfig.AppSetting_ApiBaseUrl];
            return $"{baseUrl}{ResourceData.LinkConfirmOrder}{id}";
        }
        #endregion
    }
}
