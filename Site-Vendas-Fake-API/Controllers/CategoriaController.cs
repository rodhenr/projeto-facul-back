using Microsoft.AspNetCore.Mvc;
using Site_Vendas_Fake_API.Entidades;
using Site_Vendas_Fake_API.Services;

namespace Site_Vendas_Fake_API.Controllers;

public class CategoriaController : BaseController
{
    private readonly CategoriaService _categoriaService;
    
    public CategoriaController(CategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<List<Categoria>> BuscarCategorias()
    {
        return await _categoriaService.BuscarCategorias();
    }
    
    [HttpGet("{categoriaId:int}")]
    public async Task<Categoria> BuscarCategorias([FromRoute] int categoriaId)
    {
        return await _categoriaService.BuscarCategoriaPorId(categoriaId);
    }
}