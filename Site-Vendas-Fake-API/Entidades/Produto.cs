using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site_Vendas_Fake_API.Entidades;

public class Produto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public required string Nome { get; set; }
    
    public required string Descricao { get; set; }
    
    public decimal Preco { get; set; }
    
    public required string UrlImagem { get; set; }
    
    [InverseProperty("Produto")]
    public virtual ICollection<ProdutoCategoria> ProdutoCategorias { get; set; } = new List<ProdutoCategoria>();
    
    [InverseProperty("Produto")]
    public virtual ICollection<ItemPedido> ItemPedidos { get; set; } = new List<ItemPedido>();
}