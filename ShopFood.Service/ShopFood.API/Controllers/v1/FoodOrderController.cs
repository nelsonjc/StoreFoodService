using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopFood.Application.Implements;
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

        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(HttpResponse<IEnumerable<FoodOrderHeadDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<IEnumerable<FoodOrderHeadDto>>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _foodOrderBL.GetAllAsync();
            if (result != null)
                return await GetResponseAsync(HttpStatusCode.OK, ServiceMessages.OK, result);
            return await GetResponseAsync<HttpResponse<IEnumerable<FoodOrderHeadDto>>>(HttpStatusCode.Unauthorized, ServiceMessages.UNAUTHORIZED, null);
        }

        [HttpGet]
        [Route("GetById/id")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(HttpResponse<FoodOrderHeadDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<FoodOrderHeadDto>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _foodOrderBL.GetByIdAsync(id);
            if (result != null)
                return await GetResponseAsync(HttpStatusCode.OK, ServiceMessages.OK, result);
            return await GetResponseAsync<HttpResponse<FoodOrderHeadDto>>(HttpStatusCode.Unauthorized, ServiceMessages.UNAUTHORIZED, null);
        }

        #endregion
    }
}
