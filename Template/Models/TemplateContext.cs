using Microsoft.EntityFrameworkCore;

namespace Template.Models
{
  public class TemplateContext : DbContext
  {
    public virtual DbSet<Model> Models { get; set; }

    public TemplateContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}