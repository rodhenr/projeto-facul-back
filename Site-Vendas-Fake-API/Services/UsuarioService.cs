using Microsoft.EntityFrameworkCore;
using Site_Vendas_Fake_API.Database;
using Site_Vendas_Fake_API.Entidades;
using Site_Vendas_Fake_API.Models;

namespace Site_Vendas_Fake_API.Services;

public class UsuarioService
{
    private readonly AppDbContextIdentity _context;
    private readonly CurrentUserAccessor _currentUserAccessor;
    
    public UsuarioService(AppDbContextIdentity context, CurrentUserAccessor currentUserAccessor)
    {
        _context = context;
        _currentUserAccessor = currentUserAccessor;
    }

    public async Task<UsuarioEndereco?> BuscarEndereco()
    {
        var usuarioId = _currentUserAccessor.UserId;
        
        return await _context.UsuarioEnderecos
            .Where(x => x.UsuarioId == usuarioId)
            .FirstOrDefaultAsync();
    }
    
    public async Task CadastrarEndereco(UsuarioEnderecoRequest dados)
    {
        var enderecoUsuario = await BuscarEndereco();

        if (enderecoUsuario is not null) throw new Exception("Usuário já possui endereço cadastrado");
        
        var usuarioId = _currentUserAccessor.UserId;

        var endereco = new UsuarioEndereco
        {
            Id = 0,
            UsuarioId = usuarioId,
            Rua = dados.Rua,
            Cidade = dados.Cidade,
            Bairro = dados.Bairro,
            Numero = dados.Numero,
            Uf = dados.Uf,
            Cep = dados.Cep
        };

        _context.UsuarioEnderecos.Add(endereco);
        await _context.SaveChangesAsync();
    }
    
    public async Task AtualizarEndereco(UsuarioEnderecoRequest dados)
    {
        var enderecoUsuario = await BuscarEndereco() ?? throw new Exception("Nenhum endereço encontrado.");

        enderecoUsuario.Rua = dados.Rua;
        enderecoUsuario.Cidade = dados.Cidade;
        enderecoUsuario.Bairro = dados.Bairro;
        enderecoUsuario.Numero = dados.Numero;
        enderecoUsuario.Uf = dados.Uf;
        enderecoUsuario.Cep = dados.Cep;

        await _context.SaveChangesAsync();
    }
}