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
    /// Controller to user
    /// </summary>
    public class UserController : BaseController
    {
        private readonly IUserBL _userBL;

        public UserController(IUserBL userBL)
        {
            _userBL = userBL;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(HttpResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<bool>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] UserRequest request)
        {
            await _userBL.InsertAsync(request);
            return await GetResponseAsync(HttpStatusCode.OK, ServiceMessages.OK, true);
        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(HttpResponse<IEnumerable<UserDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<IEnumerable<UserDto>>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userBL.GetAllAsync();
            if (result != null)
                return await GetResponseAsync(HttpStatusCode.OK, ServiceMessages.OK, result);
            return await GetResponseAsync<HttpResponse<IEnumerable<UserDto>>>(HttpStatusCode.Unauthorized, ServiceMessages.UNAUTHORIZED, null);
        }

        [HttpGet]
        [Route("GetById/id")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(HttpResponse<UserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<UserDto>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(Guid idUser)
        {
            var result = await _userBL.GetByIdAsync(idUser);
            if (result != null)
                return await GetResponseAsync(HttpStatusCode.OK, ServiceMessages.OK, result);
            return await GetResponseAsync<HttpResponse<UserDto>>(HttpStatusCode.Unauthorized, ServiceMessages.UNAUTHORIZED, null);
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(HttpResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<bool>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UserRequest request)
        {
            await _userBL.UpdateAsync(request);
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
            await _userBL.DeleteAsync(id);
            return await GetResponseAsync(HttpStatusCode.OK, ServiceMessages.OK, true);
        }
    }
}
