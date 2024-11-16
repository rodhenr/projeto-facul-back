using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site_Vendas_Fake_API.Entidades;

public class Categoria
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public required string Descricao { get; set; }
    
    [InverseProperty("Categoria")]
    public virtual ICollection<ProdutoCategoria> ProdutoCategorias { get; set; } = new List<ProdutoCategoria>();
}