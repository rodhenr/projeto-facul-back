using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site_Vendas_Fake_API.Entidades;

public class PedidoPagamento
{
    [Key]
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public required string FormaPagamento { get; set; }
    public required string SituacaoPagamento { get; set; }
    
    [ForeignKey("PedidoId")]
    [InverseProperty("PedidoPagamento")]
    public virtual Pedido Pedido { get; set; } = null!;
}