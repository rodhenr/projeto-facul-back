using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site_Vendas_Fake_API.Models;
using Site_Vendas_Fake_API.Services;

namespace Site_Vendas_Fake_API.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("cadastro")]
    public async Task CadastrarUsuario([FromBody] UsuarioCadastro dados)
    {
        await _authService.Registrar(dados);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<AutenticacaoResponse> Login([FromBody] UsuarioLogin dados)
    {
        return await _authService.Login(dados);
    }
}