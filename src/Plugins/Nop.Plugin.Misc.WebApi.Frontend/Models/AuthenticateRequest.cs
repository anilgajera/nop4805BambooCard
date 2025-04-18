namespace Nop.Plugin.Misc.WebApi.Framework.Models;

public class AuthenticateRequest
{
    /// <summary>
    /// Gets or sets the email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password
    /// </summary>
    public string Password { get; set; }
}