using Microsoft.EntityFrameworkCore;
using Site_Vendas_Fake_API.Database;

namespace Site_Vendas_Fake_API.Services;

public record PedidoDto(int Id, string Status, decimal PrecoTotal, List<Item> Itens);
public record Item(int ProdutoId, string ProdutoNome, decimal ProdutoPreco, int ProdutoQuantidade);

public class PedidoService
{
    private readonly AppDbContextIdentity _context;
    private readonly CurrentUserAccessor _currentUserAccessor;
    
    public PedidoService(AppDbContextIdentity context, CurrentUserAccessor currentUserAccessor)
    {
        _context = context;
        _currentUserAccessor = currentUserAccessor;
    }

    public async Task<List<PedidoDto>> BuscarPedidos()
    {
        var usuarioId = _currentUserAccessor.UserId;
        
        return await _context.Pedidos
            .Where(x => x.UsuarioId == usuarioId)
            .Select(x => new PedidoDto(
                x.Id,
                x.Status,
                x.PrecoTotal,
                x.ItemPedidos.Select(i => new Item(
                    i.ProdutoId,
                    i.Produto.Nome,
                    i.Produto.Preco,
                    i.Quantidade
                )).ToList()
            ))
            .ToListAsync();
    }
}