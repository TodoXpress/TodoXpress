﻿using System.Net;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TodoXpress.Api.Identity.Entities;
using TodoXpress.Api.Identity.Persistence;
using TodoXpress.Api.Identity.Services;
using TodoXpress.Api.Identity.Services.Interfaces;

namespace TodoXpress.Api.Identity;

public static class ServiceRegistration
{
    // public static IServiceCollection Add(this IServiceCollection services)
    // {

    //     return services;
    // }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(config => 
        {
            config.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "TodoXpress Identity API",
                Description = "API of the TodoXpress App for manageing the accounts",
                Contact = new OpenApiContact()
                {
                    Name = "Fabian Dasler",
                    Email = "fabian@todoxpress.com",
                }
            });

            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
            });

            config.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    ["TodoXpress"]
                }
            });

            // var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            // config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }

    public static IServiceCollection AddAspIdentity(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<IdentityContext>(options =>
            options.UseNpgsql(config.GetConnectionString("Default")));

        services
            .AddIdentity<User, Role>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddRoleStore<RoleStore<Role, IdentityContext, Guid>>()
            .AddRoleManager<RoleManager<Role>>()
            .AddUserStore<UserStore<User, Role, IdentityContext, Guid>>()
            .AddUserManager<UserManager<User>>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddConfiguredAuthentication(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? string.Empty))
            };
        });

        return services;
    }

    public static IServiceCollection AddConfiguredAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmailSender<User>>(sp => 
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var mail = config.GetSection("Mail");

            string from = mail.GetValue<string>("FromAddress") ?? string.Empty;
            string host = mail.GetValue<string>("Host") ?? string.Empty;
            int port = mail.GetValue<int>("Port");

            string user = mail.GetValue<string>("User") ?? string.Empty;
            string pwd = mail.GetValue<string>("Password") ?? string.Empty;

            return new EmailService()
            {
                Host = host,
                Port = port,
                FromAddress = from,
                Credentials = new NetworkCredential(user, pwd),
                EnableSsl = false
            };
        });

        services.AddScoped<IDataService<Permission>, DataService<Permission>>();
        services.AddScoped<IDataService<Ressource>, DataService<Ressource>>();
        services.AddScoped<IDataService<Scope>, DataService<Scope>>();

        return services;
    }
}
