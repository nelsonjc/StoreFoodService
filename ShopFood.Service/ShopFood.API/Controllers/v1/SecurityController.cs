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
    /// Controller to security
    /// </summary>
    public class SecurityController : BaseController
    {
        #region Varibles
        private readonly ISecurityBL _securityBL;

        #endregion

        #region Constructor
        public SecurityController(ISecurityBL securityBL)
        {
            _securityBL = securityBL;
        }

        #endregion Constructor

        #region Controllers

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        [ProducesResponseType(typeof(HttpResponse<UserAuthenticationResultDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<UserAuthenticationResultDto>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            UserAuthenticationResultDto result = await _securityBL.AuthenticateAsync(request);
            if (result != null)
                return await GetResponseAsync(HttpStatusCode.OK, ServiceMessages.OK, result);
            return await GetResponseAsync<UserAuthenticationResultDto>(HttpStatusCode.Unauthorized, ServiceMessages.UNAUTHORIZED, null);
        }

        #endregion Public
    }
}
