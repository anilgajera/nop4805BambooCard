using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.WebApi.Frontend.Helpers;

namespace Nop.Plugin.Misc.WebApi.Frontend.Controllers;

[ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
[Area("api")]
[Route("api/[controller]/[action]", Order = int.MaxValue)]
[ApiExplorerSettings(GroupName = WebApiFrontendDefaults.VERSION)]
[EnableCors(WebApiFrontendDefaults.CORS_POLICY_NAME)]
[ApiController]
[Authorize]
[Produces("application/json")]
public abstract class BaseNopWebApiController : ControllerBase
{
}