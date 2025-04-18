using Nop.Core.Domain.Customers;
using Nop.Plugin.Misc.WebApi.Framework.Models;
using Nop.Services.Customers;

namespace Nop.Plugin.Misc.WebApi.Frontend.Services;

public  partial class AuthorizationUserService : IAuthorizationUserService
{
    #region Fields

    private readonly CustomerSettings _customerSettings;
    private readonly ICustomerRegistrationService _customerRegistrationService;
    private readonly ICustomerService _customerService;
    private readonly IJwtTokenService _jwtTokenService;

    #endregion

    #region Ctor

    public AuthorizationUserService(CustomerSettings customerSettings,
        ICustomerRegistrationService customerRegistrationService,
        ICustomerService customerService,
        IJwtTokenService jwtTokenService)
    {
        _customerSettings = customerSettings;
        _customerRegistrationService = customerRegistrationService;
        _customerService = customerService;
        _jwtTokenService = jwtTokenService;
    }

    #endregion

    #region Utilities

    protected virtual AuthenticateResponse GetAuthenticateResponse(Customer customer)
    {
        return new AuthenticateResponse(_jwtTokenService.GetNewJwtToken(customer))
        {
            CustomerId = customer.Id,
            CustomerGuid = customer.CustomerGuid,
            Username = _customerSettings.UsernamesEnabled ? customer.Username : customer.Email
        };
    }

    #endregion

    #region Methods

    /// <summary>
    /// Generate JWT token for customer
    /// </summary>
    /// <param name="request">Authenticate request</param>
    /// <returns>JWT token as authenticate response</returns>
    public virtual async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request)
    {
        var loginResult = await _customerRegistrationService.ValidateCustomerAsync(request.Email, request.Password);

        if (loginResult != CustomerLoginResults.Successful)
            return null;

        var customer = await (_customerSettings.UsernamesEnabled
            ? _customerService.GetCustomerByUsernameAsync(request.Email)
            : _customerService.GetCustomerByEmailAsync(request.Email));

        _ = await _customerRegistrationService.SignInCustomerAsync(customer, null);

        return GetAuthenticateResponse(customer);
    }

    #endregion
}