using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Site_Vendas_Fake_API.Entidades;
using Site_Vendas_Fake_API.Models;

namespace Site_Vendas_Fake_API.Database;

public class AppDbContextIdentity(DbContextOptions<AppDbContextIdentity> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Pedidos> Pedidos { get; set; }
}