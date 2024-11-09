namespace Site_Vendas_Fake_API.Entidades;

public class Pedidos
{
    public int Id { get; set; }
    public required string UsuarioId { get; set; }
    public required string Status { get; set; }
    public decimal PrecoTotal { get; set; }
}