using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site_Vendas_Fake_API.Entidades;

public class ItemPedido
{
    [Key]
    public int Id { get; set; }
    
    public int PedidoId { get; set; }
    
    public int ProdutoId { get; set; }
    
    public int Quantidade { get; set; }
    
    [ForeignKey("PedidoId")]
    [InverseProperty("ItemPedidos")]
    public virtual Pedido Pedido { get; set; } = null!;
    
    [ForeignKey("ProdutoId")]
    [InverseProperty("ItemPedidos")]
    public virtual Produto Produto { get; set; } = null!;
}