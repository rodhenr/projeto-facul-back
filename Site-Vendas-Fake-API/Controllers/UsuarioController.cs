using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site_Vendas_Fake_API.Entidades;
using Site_Vendas_Fake_API.Models;
using Site_Vendas_Fake_API.Services;

namespace Site_Vendas_Fake_API.Controllers;

public class UsuarioController : BaseController
{
    private readonly PedidoService _pedidoService;
    private readonly UsuarioService _usuarioService;
    
    public UsuarioController(PedidoService pedidoService, UsuarioService usuarioService)
    {
        _pedidoService = pedidoService;
        _usuarioService = usuarioService;
    }
    
    [Authorize]
    [HttpGet("enderecos")]
    public async Task<UsuarioEndereco?> BuscarEnderecos()
    {
        return await _usuarioService.BuscarEndereco();
    }
    
    [Authorize]
    [HttpPost("enderecos")]
    public async Task CadastrarEndereco([FromBody] UsuarioEnderecoRequest dados)
    {
        await _usuarioService.CadastrarEndereco(dados);
    }
    
    [Authorize]
    [HttpGet("pedidos")]
    public async Task<List<PedidoDto>> BuscarPedidos()
    {
        return await _pedidoService.BuscarPedidos();
    }
}