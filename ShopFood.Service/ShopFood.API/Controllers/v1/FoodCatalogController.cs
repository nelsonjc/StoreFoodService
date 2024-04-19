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
    public class FoodCatalogController : BaseController
    {
        private readonly IFoodCatalogBL _foodCatalogBL;

        public FoodCatalogController(IFoodCatalogBL foodCatalogBL)
        {
            _foodCatalogBL = foodCatalogBL;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(HttpResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<bool>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] FoodCatalogRequest request)
        {
            await _foodCatalogBL.InsertAsync(request);
            return await GetResponseAsync(HttpStatusCode.OK, ServiceMessages.OK, true);
        }

        [HttpGet]
        [Route("GetAllCustomer")]
        [Authorize]
        [ProducesResponseType(typeof(HttpResponse<IEnumerable<FoodCatalogCustomerDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<IEnumerable<FoodCatalogCustomerDto>>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllCustomer()
        {
            IEnumerable<FoodCatalogCustomerDto> result = await _foodCatalogBL.GetAllCustomerAsync();
            if (result != null)
                return await GetResponseAsync(HttpStatusCode.OK, ServiceMessages.OK, result);
            return await GetResponseAsync<HttpResponse<IEnumerable<FoodCatalogDto>>>(HttpStatusCode.Unauthorized, ServiceMessages.UNAUTHORIZED, null);
        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(HttpResponse<IEnumerable<FoodCatalogDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<IEnumerable<FoodCatalogDto>>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _foodCatalogBL.GetAllAsync();
            if (result != null)
                return await GetResponseAsync(HttpStatusCode.OK, ServiceMessages.OK, result);
            return await GetResponseAsync<HttpResponse<IEnumerable<FoodCatalogDto>>>(HttpStatusCode.Unauthorized, ServiceMessages.UNAUTHORIZED, null);
        }

        [HttpGet]
        [Route("GetById/id")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(HttpResponse<FoodCatalogDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<FoodCatalogDto>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(Guid idUser)
        {
            var result = await _foodCatalogBL.GetByIdAsync(idUser);
            if (result != null)
                return await GetResponseAsync(HttpStatusCode.OK, ServiceMessages.OK, result);
            return await GetResponseAsync<HttpResponse<FoodCatalogDto>>(HttpStatusCode.Unauthorized, ServiceMessages.UNAUTHORIZED, null);
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(HttpResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<bool>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update([FromBody] FoodCatalogRequest request)
        {
            await _foodCatalogBL.UpdateAsync(request);
            return await GetResponseAsync(HttpStatusCode.OK, ServiceMessages.OK, true);
        }

        [HttpDelete]
        [Route("Delete/id")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(HttpResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<bool>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _foodCatalogBL.DeleteAsync(id);
            return await GetResponseAsync(HttpStatusCode.OK, ServiceMessages.OK, true);
        }
    }
}
