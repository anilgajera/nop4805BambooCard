using Newtonsoft.Json;

namespace Nop.Plugin.Misc.WebApi.Framework.Models;

public class AuthenticateResponse
{
    public AuthenticateResponse(string token)
    {
        Token = token;
    }

    /// <summary>
    /// Gets or sets the username
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the customer identifier
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the customer GUID
    /// </summary>
    public Guid CustomerGuid { get; set; }

    /// <summary>
    /// Gets or sets the token
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Token { get; set; }
}