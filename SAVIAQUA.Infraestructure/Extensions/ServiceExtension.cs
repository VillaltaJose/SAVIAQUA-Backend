using System.Data;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using SAVIAQUA.Application.Services;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Core.Interfaces.Services;
using SAVIAQUA.Core.Options;
using SAVIAQUA.Infraestructure.Repositories;

namespace SAVIAQUA.Infraestructure.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection SetupAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SecurityOptions>(configuration.GetSection("SecurityOptions"));
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["SecurityOptions:Issuer"],
                ValidAudience = configuration["SecurityOptions:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecurityOptions:SecretKey"] ?? ""))
            };
        });

        return services;
    }
    
    public static IServiceCollection AddDbProvider(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DataBase") ?? "";
        services.AddScoped<IDbConnection>((sp) => new NpgsqlConnection(connectionString));

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IAutenticacionRepository, AutenticacionRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IPasswordService, PasswordService>();
        services.AddTransient<IAutenticacionService, AutenticacionService>();
        
        return services;
    }
}