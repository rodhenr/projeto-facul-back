using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Site_Vendas_Fake_API.Entidades;

namespace Site_Vendas_Fake_API.Database;

public class AppDbContextIdentity(DbContextOptions<AppDbContextIdentity> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Categoria> Categorias { get; set; }
    
    public DbSet<ItemPedido> ItemPedidos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoPagamento> PedidoPagamentos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<ProdutoCategoria> ProdutoCategorias { get; set; }
    
    public DbSet<UsuarioEndereco> UsuarioEnderecos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Categoria>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<ItemPedido>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<PedidoPagamento>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Produto>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<ProdutoCategoria>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<UsuarioEndereco>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<ItemPedido>()
            .HasOne(x => x.Pedido)
            .WithMany(x => x.ItemPedidos)
            .HasForeignKey(mg => mg.PedidoId);
        
        modelBuilder.Entity<ItemPedido>()
            .HasOne(x => x.Produto)
            .WithMany(x => x.ItemPedidos)
            .HasForeignKey(mg => mg.ProdutoId);
        
        modelBuilder.Entity<ProdutoCategoria>()
            .HasOne(x => x.Produto)
            .WithMany(x => x.ProdutoCategorias)
            .HasForeignKey(mg => mg.ProdutoId);
        
        modelBuilder.Entity<ProdutoCategoria>()
            .HasOne(x => x.Categoria)
            .WithMany(x => x.ProdutoCategorias)
            .HasForeignKey(mg => mg.CategoriaId);
    }
}