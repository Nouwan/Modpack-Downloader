using Microsoft.EntityFrameworkCore;

namespace api.Domain;

public class DataContext : DbContext
{
    public DbSet<Modpack> Modpacks { get; set; }
    public DbSet<Version> Versions { get; set; }
    public DbSet<Forge> Forges { get; set; }
    public DbSet<Mod> Mods { get; set; }
    public DbSet<Config> Configs { get; set; }


    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
        optionsBuilder.UseNpgsql( connectionString
                                  ?? "Server=localhost;Port=5432;Database=Modpacks;User Id=postgres;Password=Secret01!;");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var modpack = modelBuilder.Entity<Modpack>();
        modpack.HasKey(mp => mp.Id);
        modpack.Property(mp => mp.Name).IsRequired();
        modpack.HasMany(mp => mp.Versions).WithOne().HasForeignKey(v => v.ModpackId);

        var version = modelBuilder.Entity<Version>();
        version.HasKey(v => v.Id);
        version.HasOne(v => v.Forge).WithOne().HasForeignKey<Forge>(f => f.VersionId);
        version.HasMany(v => v.Mods).WithOne().HasForeignKey(m => m.VersionId);
        version.HasMany(v => v.Configs).WithOne().HasForeignKey(m => m.VersionId);

        var mod = modelBuilder.Entity<Mod>();
        mod.HasKey(m => m.Id);
        mod.Property(m => m.Name).IsRequired();
        mod.Property(m => m.Url).IsRequired();

        var forge = modelBuilder.Entity<Forge>();
        forge.HasKey(f => f.Id);
        forge.Property(f => f.Url).IsRequired();

        var config = modelBuilder.Entity<Config>();
        config.HasKey(c => c.Id);
        config.Property(c => c.Content).IsRequired();
        config.Property(c => c.Path).IsRequired();
    }
}