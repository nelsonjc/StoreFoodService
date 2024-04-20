using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.DTOs.Results;
using ShopFood.Domain.Interfaces.Application.Implements;
using ShopFood.Domain.Variables;
using System.Net;

namespace ShopFood.API.Controllers.v1
{
    /// <summary>
    /// Controller to food order
    /// </summary>
    public class FoodOrderController : BaseController
    {
        #region Variables
        private readonly IFoodOrderBL _foodOrderBL;
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor to food order controller
        /// </summary>
        /// <param name="foodOrderBL"></param>
        public FoodOrderController(IFoodOrderBL foodOrderBL)
        {
            _foodOrderBL = foodOrderBL;
        }
        #endregion

        #region Controllers
        [HttpPost]
        [Route("Create")]
        [Authorize]
        [ProducesResponseType(typeof(HttpResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<bool>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] FoodOrderRequest request)
        {
            await _foodOrderBL.InsertAsync(request);
            return await GetResponseAsync(HttpStatusCode.OK, ServiceMessages.OK, true);
        }

        [HttpGet]
        [Route("Confirm")]
        [ProducesResponseType(typeof(HttpResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<bool>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Confirm(Guid id)
        {
            var resultContent = await _foodOrderBL.ConfirmAsync(id);
            return Content(resultContent, AppConfig.HTMLTextFormatting);
        } 
        #endregion
    }
}
