using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.WebApi.Framework.Models;
using Nop.Plugin.Misc.WebApi.Frontend.Services;

namespace Nop.Plugin.Misc.WebApi.Frontend.Controllers;

[Area("api")]
[Route("api/[controller]/[action]", Order = int.MaxValue)]
[ApiExplorerSettings(GroupName = WebApiFrontendDefaults.VERSION)]
[EnableCors(WebApiFrontendDefaults.CORS_POLICY_NAME)]
[ApiController]
public partial class AuthenticateController : ControllerBase
{
    #region Fields

    private readonly IAuthorizationUserService _authorizationUserService;

    #endregion

    #region Ctor

    public AuthenticateController(
        IAuthorizationUserService authorizationUserService)
    {
        _authorizationUserService = authorizationUserService;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Authenticate user
    /// </summary>
    /// <param name="request"></param>
    [HttpPost]
    [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public virtual async Task<IActionResult> GetToken([FromBody] AuthenticateRequest request)
    {
        var response = await _authorizationUserService.AuthenticateAsync(request);

        if (response == null)
            return new JsonResult(new { success = false, message= "Username or password is incorrect", data = string.Empty }) { StatusCode = StatusCodes.Status401Unauthorized };

        return new JsonResult(new { data = response }) { StatusCode = StatusCodes.Status200OK };
    }

    #endregion
}