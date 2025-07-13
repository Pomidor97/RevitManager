using backend.core.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Family> Families => Set<Family>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Section> Sections => Set<Section>();
    public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();
    public DbSet<RevitVersion> RevitVersions => Set<RevitVersion>();
    
    public DbSet<FamilyFile> FamilyFiles => Set<FamilyFile>();



    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Family>().ToTable("families");
        modelBuilder.Entity<FamilyFile>().ToTable("family_files");
        modelBuilder.Entity<Category>().ToTable("categories");
        modelBuilder.Entity<Section>().ToTable("sections");
        modelBuilder.Entity<Manufacturer>().ToTable("manufacturers");
        modelBuilder.Entity<RevitVersion>().ToTable("revit_versions");

        modelBuilder.Entity<FamilyFile>()
            .HasOne(f => f.Family)
            .WithMany(f => f.Attachments)
            .HasForeignKey(f => f.FamilyId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}