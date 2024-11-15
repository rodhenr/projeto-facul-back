using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site_Vendas_Fake_API.Entidades;

public class Pedido
{
    [Key]
    public int Id { get; set; }
    
    public DateTime Data { get; set; }
    public required string UsuarioId { get; set; }
    public required string SituacaoPedido { get; set; }
    public decimal TaxaEntrega { get; set; }
    
    [InverseProperty("Pedido")]
    public virtual PedidoPagamento PedidoPagamento { get; set; } = null!;
    
    [InverseProperty("Pedido")]
    public virtual ICollection<ItemPedido> ItemPedidos { get; set; } = new List<ItemPedido>();
}