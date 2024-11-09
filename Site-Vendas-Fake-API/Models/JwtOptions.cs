using Microsoft.IdentityModel.Tokens;

namespace Site_Vendas_Fake_API.Models;

public class JwtOptions
{
    public const string Section = "JwtOptions";
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required SigningCredentials SigningCredentials { get; set; }
    public int AccessTokenExpiration { get; set; }
    public int RefreshTokenExpiration { get; set; }
}