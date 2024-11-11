using System.ComponentModel.DataAnnotations;

namespace Site_Vendas_Fake_API.Entidades;

public class Produto
{
    [Key]
    public int Id { get; set; }
    
    public required string Nome { get; set; }
    
    public required string Descricao { get; set; }
    
    public decimal Preco { get; set; }
}