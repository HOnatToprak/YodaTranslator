using Microsoft.EntityFrameworkCore;

namespace AFS.DatabaseModel;

public class AFSDatabaseContext : DbContext
{
    public DbSet<Translation> Translations { get; set; }

    public AFSDatabaseContext(DbContextOptions<AFSDatabaseContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new TranslationTypeConfiguration().Configure(modelBuilder.Entity<Translation>());
    }
}