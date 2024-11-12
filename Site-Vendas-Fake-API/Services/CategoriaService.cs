using Microsoft.EntityFrameworkCore;
using Site_Vendas_Fake_API.Database;
using Site_Vendas_Fake_API.Entidades;

namespace Site_Vendas_Fake_API.Services;

public class CategoriaService
{
    private readonly AppDbContextIdentity _context;

    public CategoriaService(AppDbContextIdentity context)
    {
        _context = context;
    }

    public async Task<List<Categoria>> BuscarCategorias()
    {
        return await _context.Categorias.ToListAsync();
    }
    
    public async Task<Categoria> BuscarCategoriaPorId(int categoriaId)
    {
        return await _context.Categorias.FirstOrDefaultAsync(x => x.Id == categoriaId) ?? throw new Exception($"Categoria não encontrada para Id {categoriaId}");
    }
}