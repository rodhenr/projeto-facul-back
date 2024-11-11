using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Site_Vendas_Fake_API.Entidades;
using Site_Vendas_Fake_API.Models;

namespace Site_Vendas_Fake_API.Database;

public class AppDbContextIdentity(DbContextOptions<AppDbContextIdentity> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Pedidos> Pedidos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.Property(e => e.EmailConfirmed)
                .HasColumnType("NUMBER(1)");

            entity.Property(e => e.PhoneNumberConfirmed)
                .HasColumnType("NUMBER(1)");

            entity.Property(e => e.LockoutEnabled)
                .HasColumnType("NUMBER(1)");

            entity.Property(e => e.TwoFactorEnabled)
                .HasColumnType("NUMBER(1)");
        });
    }
}