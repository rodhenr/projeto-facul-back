using Microsoft.EntityFrameworkCore;
using Site_Vendas_Fake_API.Database;
using Site_Vendas_Fake_API.Entidades;
using Site_Vendas_Fake_API.Models;

namespace Site_Vendas_Fake_API.Services;

public record PedidoDto(int Id, string Status, DateTime Data, decimal PrecoTotal, decimal TaxaEntrega, List<Item> Itens);
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
                x.SituacaoPedido,
                x.Data,
                x.ItemPedidos.Sum(y => y.Quantidade * y.Produto.Preco) + x.TaxaEntrega,
                x.TaxaEntrega,
                x.ItemPedidos.Select(i => new Item(
                    i.ProdutoId,
                    i.Produto.Nome,
                    i.Produto.Preco,
                    i.Quantidade
                )).ToList()
            ))
            .ToListAsync();
    }

    public async Task SalvarPedido(UsuarioPedidoRequest dados)
    {
        var usuarioId = _currentUserAccessor.UserId;
        
        var produtos = await _context.Produtos
            .Where(x => dados.Itens.Select(y => y.ProdutoId).Contains(x.Id))
            .ToListAsync();

        if (produtos.Count != dados.Itens.Count) throw new Exception("Produto não encontrado");

        var totais = produtos
            .Join(dados.Itens, produto => produto.Id, itens => itens.ProdutoId, (produto, itens) => new
            {
                produto.Id,
                produto.Preco,
                itens.Quantidade,
            })
            .ToList();

        var pedido = new Pedido
        {
            UsuarioId = usuarioId,
            Data = DateTime.Now.ToUniversalTime(),
            SituacaoPedido = "Em Análise",
            TaxaEntrega = dados.TaxaEntrega
        };

        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();

        var pedidoPagamento = new PedidoPagamento
        {
            PedidoId = pedido.Id,
            FormaPagamento = dados.FormaPagamento,
            SituacaoPagamento = "Em Análise"
        };
        
        _context.PedidoPagamentos.Add(pedidoPagamento);
        await _context.SaveChangesAsync();

        var itensPedido = totais
            .Select(item => new ItemPedido 
            { 
                PedidoId = pedido.Id, 
                ProdutoId = item.Id, 
                Quantidade = item.Quantidade
            })
            .ToList();

        _context.ItemPedidos.AddRange(itensPedido);
        await _context.SaveChangesAsync();
    }
}