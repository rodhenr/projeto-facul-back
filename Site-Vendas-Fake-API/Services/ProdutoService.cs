using Microsoft.EntityFrameworkCore;
using Site_Vendas_Fake_API.Database;
using Site_Vendas_Fake_API.Entidades;
using Site_Vendas_Fake_API.Exceptions;

namespace Site_Vendas_Fake_API.Services;

public class ProdutoService
{
    private readonly AppDbContextIdentity _context;
    
    public ProdutoService(AppDbContextIdentity context)
    {
        _context = context;
    }
    
    public async Task<List<Produto>> BuscarPorFiltro(string filtro)
    {
        var prods = await _context.Produtos
            .Where(x => EF.Functions.Like(x.Nome.ToLower(), $"%{filtro.ToLower()}%"))
            .ToListAsync();

        return prods;
    }

    public async Task<Produto> BuscarPorId(int id)
    {
        return await _context.Produtos
             .Where(x => x.Id == id)
             .FirstOrDefaultAsync() ?? throw new EntityNotFoundException($"Nenhum produto encontrado para o Id `{id}`");
    }
    
    public async Task<List<Produto>> BuscarPorDestaque()
    {
        return await _context.Produtos
            .AsNoTracking()
            .Take(8)
            .ToListAsync();
    }

    public async Task<List<Produto>> BuscarProdutosPorCategoria(int categoriaId)
    {
        return await _context.ProdutoCategorias
            .AsNoTracking()
            .Where(x => x.CategoriaId == categoriaId)
            .Select(x => x.Produto)
            .ToListAsync();
    }
}