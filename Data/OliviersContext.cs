using GestionOliviers.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionOliviers.Data;

public class OliviersContext : DbContext
{
    public OliviersContext(DbContextOptions<OliviersContext> options) : base(options)
    {
    }

    public DbSet<Champ> Champs => Set<Champ>();
    public DbSet<Olivier> Oliviers => Set<Olivier>();
    public DbSet<Recolte> Recoltes => Set<Recolte>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Champ>()
            .HasMany(c => c.Oliviers)
            .WithOne(o => o.Champ)
            .HasForeignKey(o => o.ChampId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Olivier>()
            .HasMany(o => o.Recoltes)
            .WithOne(r => r.Olivier)
            .HasForeignKey(r => r.OlivierId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Recolte>()
            .Property(r => r.QuantiteKg)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Champ>()
            .Property(c => c.SuperficieHa)
            .HasPrecision(10, 2);
    }
}
