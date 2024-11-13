using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site_Vendas_Fake_API.Entidades;

public class UsuarioEndereco
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public required string UsuarioId { get; set; }
    
    [MaxLength(200)]
    public required string Rua { get; set; }
    
    [MaxLength(30)]
    public required string Numero { get; set; }
    
    [MaxLength(50)]
    public required string Bairro { get; set; }
    
    [MaxLength(100)]
    public required string Cidade { get; set; }
    
    [MaxLength(2)]
    public required string Uf { get; set; }
    
    [MaxLength(8)]
    public required string Cep { get; set; }
}