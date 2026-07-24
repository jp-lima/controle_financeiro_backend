namespace ControleFinanceiro.Data; 
using Microsoft.EntityFrameworkCore; 
using ControleFinanceiro.Models; 

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
     
  public DbSet<Usuario>   Usuarios   {get; set;}
  public DbSet<Transacao> Transacoes {get; set;} 
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Persiste o enum TipoTransacao como string no banco (ex: "Receita"), em vez de valores numericos, 
    // servindo para melhorar a legibilidade
        modelBuilder.Entity<Transacao>()
            .Property(t => t.Tipo)
            .HasConversion<string>();
  }
}


