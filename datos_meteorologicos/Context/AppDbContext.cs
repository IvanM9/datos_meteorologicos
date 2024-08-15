using datos_meteorologicos.Models;
using Microsoft.EntityFrameworkCore;

namespace datos_meteorologicos.Context;

public class AppDbContext: DbContext
{
    public DbSet<DatosSensores> DatosSensores { get; set; }
    public DbSet<ParametrosSensores> ParametrosSensores { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        DotNetEnv.Env.Load();
        var connectionString = Environment.GetEnvironmentVariable("DB_URL");
        optionsBuilder.UseNpgsql(connectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ParametrosSensores>()
            .HasKey(p => p.Id);
        
        modelBuilder.Entity<DatosSensores>()
            .HasKey(d => d.Id);
        
        modelBuilder.Entity<DatosSensores>()
            .HasOne<ParametrosSensores>(s => s.ParametrosSensores)
            .WithMany()
            .HasForeignKey(s => s.ParametroSensoresId);
        
        modelBuilder.Entity<DatosSensores>()
            .Property(d => d.FechaDato)
            .HasColumnType("timestamp without time zone");
    }
}