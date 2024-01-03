using TodoXpress.Api.Identity.DTOs;
using TodoXpress.Api.Identity.Entities;

namespace TodoXpress.Api.Identity.Services.Interfaces;

public interface ITokenService
{
    /// <summary>
    /// Creates an auth- and refresh token for a speciffic user.
    /// </summary>
    /// <param name="user">The user to create the tokens for.</param>
    /// <param name="clientId">The id of the client.</param>
    /// <returns>The tokens.</returns>
    Task<LoginResponse> CreateAuthTokenForUser(User user, Guid clientId);

    /// <summary>
    /// Refreshes a Token with a speciffic refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token.</param>
    /// <param name="user">The user for wich the token should be refreshed.</param>
    /// <param name="clientId">The id of the client.</param>
    /// <returns>The new tokens.</returns>
    Task<LoginResponse> RefreshAuthTokenForUser(string refreshToken, User user, Guid clientId);

    /// <summary>
    /// Validates the refresh token.
    /// </summary>
    /// <param name="token">the freshtoken.</param>
    /// <param name="userId">The id of the user.</param>
    /// <param name="clientId">The id of the client.</param>
    /// <returns>A bool indicating wheather the token is valid or not.</returns>
    Task<bool> ValidateRefreshTokenAsync(string token, Guid userId, Guid clientId);
}
