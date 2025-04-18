using Nop.Plugin.Misc.WebApi.Framework.Models;

namespace Nop.Plugin.Misc.WebApi.Frontend.Services;

public interface IAuthorizationUserService
{
    Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request);
}