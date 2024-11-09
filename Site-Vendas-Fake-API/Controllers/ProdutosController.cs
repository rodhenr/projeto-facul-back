using Microsoft.AspNetCore.Mvc;

namespace Site_Vendas_Fake_API.Controllers;

public class ProdutosController : BaseController
{
    public ProdutosController()
    {
        
    }

    [HttpGet("produto")]
    public async Task BuscarProdutos()
    {
        
    }
    
    [HttpGet("produto/{produtoId:int}")]
    public async Task BuscarProduto(int produtoId)
    {
        
    }
}