namespace Site_Vendas_Fake_API.Models;

public class UsuarioCadastro
{
    public required string Nome { get; set; }
    public required string Usuario { get; set; }
    public required string Email { get; set; }
    public required string Senha { get; set; }
    public required string ConfirmacaoSenha { get; set; }
}