using Microsoft.Extensions.Configuration;
using ShopFood.Domain.DTOs.Mail;
using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.DTOs.Results;
using ShopFood.Domain.Entities;
using ShopFood.Domain.Helpers;
using ShopFood.Domain.Interfaces.Application.Implements;
using ShopFood.Domain.Interfaces.Repository;
using System.Globalization;

namespace ShopFood.Application.Implements
{
    public class FoodOrderBL : IFoodOrderBL
    {
        private readonly IConfiguration _configuration;
        private readonly IFoodOrderRepository _foodOrderRepository;
        private readonly IUserBL _userBL;

        public FoodOrderBL(IConfiguration configuration, IFoodOrderRepository foodOrderRepository, IUserBL userBL)
        {
            _configuration = configuration;
            _foodOrderRepository = foodOrderRepository;
            _userBL = userBL;
        }

        public async Task InsertAsync(FoodOrderRequest request)
        {
            var resultOrder = await _foodOrderRepository.InsertAsync(request);            
            await SendMailConfirmOrder(resultOrder, request.UserCreatedId);
        }

        private async Task SendMailConfirmOrder(FoodOrderHead order, Guid userId)
        {
            var userData = await _userBL.GetByIdAsync(userId);

            if (!string.IsNullOrEmpty(userData.Username))
            {
                string strDetailTable = string.Empty;
                decimal totalPayable = 0;

                foreach (var detail in order.FoodOrderDetails)
                {
                    string strDetailItem = $"<tr><" +
                        $"td>{detail.ItemNumber}</td>" +
                        $"<td>{detail.FoodCatalogName}</td>" +
                        $"<td>{detail.FoodCatalogDescription}</td>" +
                        $"<td>{detail.Quantity}</td>" +
                        $"<td>${detail.Price}</td>" +
                        $"<td>${detail.Total}</td>" +
                        $"</tr>";

                    strDetailTable = string.IsNullOrEmpty(strDetailTable) ? strDetailItem : $"{strDetailTable} {strDetailItem}";
                    totalPayable += detail.Total;
                }

                CultureInfo culture = new CultureInfo("es-CO");
                string pathTemplate = CombinePaths(Environment.CurrentDirectory, @"ShopFood.Domain/Templates/FoodOrderConfirm.html");
                StreamReader sr = new StreamReader(pathTemplate);
                string htmlCode = await sr.ReadToEndAsync();
                htmlCode = htmlCode.Replace("[CUSTOMER]", userData.Name);
                htmlCode = htmlCode.Replace("[DATE_ORDER]", order.DateCreated.ToString("dddd, dd MMMM yyyy", culture));
                htmlCode = htmlCode.Replace("[DETAILS]", strDetailTable);
                htmlCode = htmlCode.Replace("[TOTAL]", totalPayable.ToString("c"));
                sr.Close();


                MailSendNotificationDto message = new()
                {
                    Configuration = GetMailConfig(),
                    Destinatary = new[] { userData.Username},
                    Subject = $"Confirmación de Pedido - Shop Food",
                    HTMLBody = htmlCode
                };

                NotificationHelper.SendMail(message);
            }
        }

        private string CombinePaths(string rootPath, string relativePath)
        {
            DirectoryInfo dir = new DirectoryInfo(rootPath);
            DirectoryInfo parentDir = Directory.GetParent(rootPath.EndsWith("\\") ? rootPath : string.Concat(rootPath, "\\"));
            return Path.Combine(parentDir.Parent.FullName, relativePath);
        }

        private MailConfigDto GetMailConfig()
        {
            var mailSettings = _configuration.GetSection("MailConfig");

            string? remittance = mailSettings["Remittance"];
            string? password = mailSettings["Password"];
            string? displayName = mailSettings["DisplayName"];
            string? host = mailSettings["Host"];
            string? port = mailSettings["Port"];
            string? defaultCredentials = mailSettings["DefaultCredentials"];
            string? enableSSL = mailSettings["EnableSSL"];

            return new MailConfigDto()
            {
                Remittance = remittance,
                Password = password,
                DisplayName = displayName,
                Host = host,
                Port = Convert.ToInt32(port),
                DefaultCredentials = Convert.ToBoolean(defaultCredentials),
                EnableSSL = Convert.ToBoolean(enableSSL) ,
                IsBodyHtml = true
            };
        }
    }
}
