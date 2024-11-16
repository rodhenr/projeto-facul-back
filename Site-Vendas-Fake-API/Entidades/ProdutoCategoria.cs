using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site_Vendas_Fake_API.Entidades;

public class ProdutoCategoria
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int ProdutoId { get; set; }
    
    public int CategoriaId { get; set; }
    
    [ForeignKey("ProdutoId")]
    [InverseProperty("ProdutoCategorias")]
    public virtual Produto Produto { get; set; } = null!;
    
    [ForeignKey("CategoriaId")]
    [InverseProperty("ProdutoCategorias")]
    public virtual Categoria Categoria { get; set; } = null!;
}