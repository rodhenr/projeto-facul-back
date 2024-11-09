using Microsoft.AspNetCore.Identity;
using Site_Vendas_Fake_API.Exceptions;
using Site_Vendas_Fake_API.Models;

namespace Site_Vendas_Fake_API.Services;

public interface IAuthService
{
    Task Login(UsuarioLogin dados);
    Task Registrar(UsuarioCadastro dados);
}

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;

    public AuthService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Login(UsuarioLogin dados)
    {
        
    }

    public async Task Registrar(UsuarioCadastro dados)
    {
        var user = await _userManager.FindByEmailAsync(dados.Email);

        if (user != null) throw new AuthorizationException("Email já cadastrado.");
        
        var appUser = new AppUser
        {
            UserName = dados.Usuario,
            Email = dados.Email,
            EmailConfirmed = true
        };
        
        var usuarioCriado = await _userManager.CreateAsync(appUser, dados.Senha);

        if (!usuarioCriado.Succeeded) throw new BadRequestException(usuarioCriado.Errors.FirstOrDefault()?.Description ?? "Falha ao registrar o usuário.");
        
        await _userManager.SetLockoutEnabledAsync(appUser, false);
        
        if (usuarioCriado.Errors.Any()) throw new AuthorizationException(usuarioCriado.Errors.First().Description);
    }
}