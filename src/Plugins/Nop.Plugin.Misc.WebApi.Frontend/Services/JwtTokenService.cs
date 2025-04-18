﻿using Nop.Core.Domain.Customers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Nop.Plugin.Misc.WebApi.Frontend.Services;

public partial class JwtTokenService : IJwtTokenService
{
    #region Fields

    private readonly CustomerSettings _customerSettings;

    #endregion

    #region Ctor

    public JwtTokenService(CustomerSettings customerSettings)
    {
        _customerSettings = customerSettings;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Generate new JWT token
    /// </summary>
    /// <param name="customer">The customer</param>
    /// <returns>JWT token</returns>
    public virtual string GetNewJwtToken(Customer customer)
    {
        // generate token that is valid for 7 days (by default)
        var currentTime = DateTimeOffset.Now;
        var expiresInSeconds = currentTime.AddDays(7).ToUnixTimeSeconds();

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Nbf, currentTime.ToUnixTimeSeconds().ToString()),
            new (JwtRegisteredClaimNames.Exp, expiresInSeconds.ToString()),
            new (WebApiFrontendDefaults.ClaimTypeName, customer.Id.ToString()),
            new (ClaimTypes.NameIdentifier, customer.CustomerGuid.ToString()),
        };

        if (_customerSettings.UsernamesEnabled)
        {
            if (!string.IsNullOrEmpty(customer.Username))
                claims.Add(new Claim(ClaimTypes.Name, customer.Username));
        }
        else
        {
            if (!string.IsNullOrEmpty(customer.Email))
                claims.Add(new Claim(ClaimTypes.Email, customer.Email));
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(WebApiFrontendDefaults.SecretKey);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), WebApiFrontendDefaults.JwtSignatureAlgorithm);
        var token = new JwtSecurityToken(new JwtHeader(signingCredentials), new JwtPayload(claims));

        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// Create a new secret key
    /// </summary>
    public virtual string NewSecretKey
    {
        get
        {
            //generate a cryptographic random number
            using var provider = RandomNumberGenerator.Create();
            var buff = new byte[WebApiFrontendDefaults.MinSecretKeyLength];
            provider.GetBytes(buff);

            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff).TrimEnd('=');
        }
    }

    #endregion
}