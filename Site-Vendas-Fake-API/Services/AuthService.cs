using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Site_Vendas_Fake_API.Entidades;
using Site_Vendas_Fake_API.Exceptions;
using Site_Vendas_Fake_API.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Site_Vendas_Fake_API.Services;

public interface IAuthService
{
    Task<AutenticacaoResponse> Login(UsuarioLogin dados);
    Task Registrar(UsuarioCadastro dados);
}

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly JwtOptions _jwtOptions;
    
    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<JwtOptions> jwtOptions)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<AutenticacaoResponse> Login(UsuarioLogin dados)
    {
        var user = await _userManager.FindByEmailAsync(dados.Email) ?? throw new UserNotFoundException("Usuário não encontrado.");
        var result = await _signInManager.PasswordSignInAsync(user.UserName!, dados.Senha, false, false);

        if (!result.Succeeded) throw new BadRequestException(GetErrorMessage(result));
        
        var tokens = await GerarToken(user);
        return new AutenticacaoResponse(user.UserName, tokens.accessToken, tokens.refreshToken);
    }

    public async Task Registrar(UsuarioCadastro dados)
    {
        var user = await _userManager.FindByEmailAsync(dados.Email);

        if (user != null) throw new AuthorizationException("Email já cadastrado.");
        
        var appUser = new AppUser
        {
            UserName = dados.Email,
            Email = dados.Email,
            EmailConfirmed = true
        };
        
        var usuarioCriado = await _userManager.CreateAsync(appUser, dados.Senha);

        if (!usuarioCriado.Succeeded) throw new BadRequestException(usuarioCriado.Errors.FirstOrDefault()?.Description ?? "Falha ao registrar o usuário.");
        
        await _userManager.SetLockoutEnabledAsync(appUser, false);
        
        if (usuarioCriado.Errors.Any()) throw new AuthorizationException(usuarioCriado.Errors.First().Description);
    }

    private async Task<(string accessToken, string refreshToken)> GerarToken(AppUser usuario)
    {
        var accessTokenClaims = await GetClaims(usuario, false);
        var refreshTokenClaims = await GetClaims(usuario, true);

        var accessToken = GenerateToken(accessTokenClaims, DateTime.Now.AddSeconds(_jwtOptions.AccessTokenExpiration));
        var refreshToken = GenerateToken(refreshTokenClaims, DateTime.Now.AddSeconds(_jwtOptions.RefreshTokenExpiration));

        return (accessToken, refreshToken);
    }
    
    private string GenerateToken(IEnumerable<Claim> claims, DateTime expirationDate)
    {
        var jwt = new SecurityTokenDescriptor
        {
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            Subject = new ClaimsIdentity(claims),
            NotBefore = DateTime.Now,
            Expires = expirationDate,
            SigningCredentials = _jwtOptions.SigningCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(jwt);

        return tokenHandler.WriteToken(token);
    }

    private async Task<IList<Claim>> GetClaims(AppUser user, bool isRefreshToken)
    {
        var usCulture = new CultureInfo("en-US");
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString(usCulture)),
            new(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString(usCulture))
        };

        if (isRefreshToken)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Typ, "Refresh"));
            return claims;
        }

        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Typ, "Bearer"));
        claims.AddRange(userClaims);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }
    
    private static string GetErrorMessage(SignInResult result)
    {
        return result switch
        {
            _ when result.IsLockedOut => "Account blocked",
            _ when result.IsNotAllowed => "Not allowed",
            _ when result.RequiresTwoFactor => "Require two factor",
            _ => "Invalid user/password"
        };
    }
}