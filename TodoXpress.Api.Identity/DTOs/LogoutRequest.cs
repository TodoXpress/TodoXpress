﻿namespace TodoXpress.Api.Identity.DTOs;

public record struct LogoutRequest
{
    public string UserId { get; set; }
    public string RefreshToken { get; set; }
}
