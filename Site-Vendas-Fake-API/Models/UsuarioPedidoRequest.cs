namespace Site_Vendas_Fake_API.Models;

public record ItemPedidoRequest(int ProdutoId, int Quantidade);
public record UsuarioPedidoRequest(List<ItemPedidoRequest> Itens, decimal TaxaEntrega, string FormaPagamento);