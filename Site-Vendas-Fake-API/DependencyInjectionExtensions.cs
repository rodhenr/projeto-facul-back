using System.Text;
using CommunityToolkit.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Site_Vendas_Fake_API.Database;
using Site_Vendas_Fake_API.Models;

namespace Site_Vendas_Fake_API;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        AddIdentityServices(services, configuration);
        AddJwtAuthenticationServices(services, configuration);
        
        return services;
    }
    
    private static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContextIdentity>(options => 
            options.UseOracle(configuration.GetConnectionString("DefaultConnection"), 
                b => b.MigrationsAssembly(typeof(AppDbContextIdentity).Assembly.FullName)));

        services.AddIdentity<AppUser, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContextIdentity>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
        });

        return services;
    }
    
    private static IServiceCollection AddJwtAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var issuer = configuration["Issuer"];
        var audience = configuration["Audience"];
        var apiKey = configuration["ApiKey"];
        
        Guard.IsNotNullOrEmpty(issuer);
        Guard.IsNotNullOrEmpty(audience);
        Guard.IsNotNullOrEmpty(apiKey);

        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(apiKey));
        
        // Configure JwtOptions
        services.Configure<JwtOptions>(options =>
        {
            options.Issuer = issuer;
            options.Audience = audience;
            options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            options.AccessTokenExpiration = 3000;
            options.RefreshTokenExpiration = 3600;
        });

        // Configure JWT
        services
            .AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; })
            .AddJwtBearer(options =>
            {
                options.Audience = audience;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        // Add policy
        services.AddAuthorizationBuilder()
            .SetFallbackPolicy(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("Typ", "Bearer")
                .Build())
            .AddPolicy("RefreshToken", policy => policy.RequireClaim("typ", "Refresh"))
            .AddPolicy("Admin", policy => policy.RequireRole("Admin"));

        return services;
    }
}