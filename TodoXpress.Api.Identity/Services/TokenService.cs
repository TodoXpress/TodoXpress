using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoXpress.Api.Identity.DTOs;
using TodoXpress.Api.Identity.Entities;
using TodoXpress.Api.Identity.Persistence;

namespace TodoXpress.Api.Identity;

internal class TokenService(
    IConfiguration config, 
    UserManager<User> userManager, 
    RoleManager<Role> roleManager,
    IdentityContext context)
{

    /// <summary>
    /// Creates an auth- and refresh token for a speciffic user.
    /// </summary>
    /// <param name="user">The user to create the tokens for.</param>
    /// <param name="clientId">The id of the client.</param>
    /// <returns>The tokens.</returns>
    public async Task<LoginResponse> CreateAuthTokenForUser(User user, Guid clientId)
    {
        var (token, expires) = await GenerateJwtTokenAsync(user);
        var refreshToken = GenerateRefreshTokenAsync(user, clientId);

        return new()
        {
            Token = token,
            ExpirationTime = expires,
            RefreshToken = await refreshToken
        };
    }

    /// <summary>
    /// Refreshes a Token with a speciffic refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token.</param>
    /// <param name="user">The user for wich the token should be refreshed.</param>
    /// <param name="clientId">The id of the client.</param>
    /// <returns>The new tokens.</returns>
    public async Task<LoginResponse> RefreshAuthTokenForUser(string refreshToken, User user, Guid clientId)
    {
        await this.DeleteExistingRefreshToken(refreshToken);
        return await CreateAuthTokenForUser(user, clientId);
    }

    /// <summary>
    /// Validates the refresh token.
    /// </summary>
    /// <param name="token">the freshtoken.</param>
    /// <param name="userId">The id of the user.</param>
    /// <param name="clientId">The id of the client.</param>
    /// <returns>A bool indicating wheather the token is valid or not.</returns>
    public async Task<bool> ValidateRefreshTokenasync(string token, Guid userId, Guid clientId)
    {
        var refreshToken = await context.RefreshTokens.FirstOrDefaultAsync(rt => Equals(rt.Token, token));

        return refreshToken is not null &&
            Equals(refreshToken.UserId, userId) &&
            Equals(refreshToken.ClientId, clientId) &&
            refreshToken.ExpiryDate > DateTime.UtcNow;
    }

    private async Task<(string token, DateTime expires)> GenerateJwtTokenAsync(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? string.Empty);

        if (!int.TryParse(config["TokenExpirationInMinutes"], out int expirationTimeInMin))
        {
            expirationTimeInMin = 60;
        }
        var expirationTime = DateTime.UtcNow.AddMinutes(expirationTimeInMin);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            IssuedAt = DateTime.UtcNow,
            Expires = expirationTime,
            Issuer = config["Issuer"],
            Audience = config["Audience"],
            TokenType = "JWT",
            CompressionAlgorithm =SecurityAlgorithms.HmacSha256Signature,
            Subject = new ClaimsIdentity(new[] 
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            }),
            Claims = new Dictionary<string, object>()
            {
                { ClaimTypes.NameIdentifier, user.Id.ToString() },
                { ClaimTypes.Email, user.Email ?? string.Empty },
                { ClaimTypes.Name, user.UserName ?? string.Empty },
                { ClaimTypes.Expiration, expirationTimeInMin.ToString() },
                { ClaimTypes.Version, config["Version"] ?? string.Empty },
            },
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        List<RoleDTO> rolesFromUser = [];
        foreach (var roleName in await userManager.GetRolesAsync(user))
        {
            var role = roleManager.Roles
                .Select(r => new RoleDTO
                {
                    Name = r.Name ?? string.Empty,
                    Permissions = r.Permissions
                        .Select(p => new PermissionDTO
                        {
                            Ressource = p.Ressource.Value,
                            Scopes = p.Scopes.Select(s => s.Value)
                                .ToList()
                        })
                        .ToList(),
                })
                .SingleOrDefault(r => Equals(r.Name, roleName));

            rolesFromUser.Add(role);
        }

        tokenDescriptor.Claims.Add(ClaimTypes.Role, JsonSerializer.Serialize(rolesFromUser));
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var serializedToken = tokenHandler.WriteToken(token);
        return (serializedToken, expirationTime);
    }

    private async Task<string> GenerateRefreshTokenAsync(User user, Guid clientId)
    {
        if(!int.TryParse(config["RefreshTokenExirationInMinutes"], out int refTokenExpirationMinutes))
        {
            refTokenExpirationMinutes = 3600;
        }

        var refreshToken = new RefreshToken()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            ClientId = clientId,
            Token = GenerateRefreshTokenString(),
            CreationDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddMinutes(refTokenExpirationMinutes)
        };

        await context.RefreshTokens.AddAsync(refreshToken);
        await context.SaveChangesAsync();

        return refreshToken.Token;
    }

    private async Task DeleteExistingRefreshToken(string token)
    {
        var refreshToken = await context.RefreshTokens
        .Where(rt => Equals(rt.Token, token))
        .SingleOrDefaultAsync();

        if (refreshToken is not null)
        {
            context.RefreshTokens.Remove(refreshToken);
            await context.SaveChangesAsync();
        }
    }

    private static string GenerateRefreshTokenString()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
