using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site_Vendas_Fake_API.Entidades;
using Site_Vendas_Fake_API.Services;

namespace Site_Vendas_Fake_API.Controllers;

public class ProdutoController : BaseController
{
    private ProdutoService _produtoService;
    public ProdutoController(ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet("")]
    public async Task<List<Produto>> BuscarProdutos()
    {
        return [];
    }
    
    [HttpGet("{produtoId:int}")]
    public async Task<Produto> BuscarProdutoPorId(int produtoId)
    {
        return await _produtoService.BuscarPorId(produtoId);
    }
}