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

    public async Task<Produto> BuscarPorId(int id)
    {
        var testes = await _context.Produtos.ToListAsync();
        return await _context.Produtos
             .Where(x => x.Id == id)
             .FirstOrDefaultAsync() ?? throw new EntityNotFoundException($"Nenhum produto encontrado para o Id `{id}`");
    }
}