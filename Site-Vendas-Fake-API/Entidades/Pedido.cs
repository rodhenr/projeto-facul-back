using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site_Vendas_Fake_API.Entidades;

public class Pedido
{
    [Key]
    public int Id { get; set; }
    
    public required string UsuarioId { get; set; }
    
    public required string Status { get; set; }
    
    public decimal PrecoTotal { get; set; }
    
    [InverseProperty("Pedido")]
    public virtual ICollection<ItemPedido> ItemPedidos { get; set; } = new List<ItemPedido>();
}