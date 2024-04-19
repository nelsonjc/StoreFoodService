using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.DTOs.Results;
using ShopFood.Domain.Interfaces.Application.Implements;
using ShopFood.Domain.Variables;
using System.Net;

namespace ShopFood.API.Controllers.v1
{
    public class FoodOrderController : BaseController
    {
        private readonly IFoodOrderBL _foodOrderBL;

        public FoodOrderController(IFoodOrderBL foodOrderBL)
        {
            _foodOrderBL = foodOrderBL;
        }

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
    }
}
