using Microsoft.EntityFrameworkCore;
using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.Domain.DAO.Repository;
public class ContextRepository : DbContext
{
    public ContextRepository() { }
    public ContextRepository(DbContextOptions<ContextRepository> options) : base(options)
    {
        this.Database.SetCommandTimeout(int.MaxValue);
    }
    public virtual DbSet<FornecedorEntity> Fornecedores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
        modelBuilder.Entity<FornecedorEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        base.OnModelCreating(modelBuilder);
    }
}