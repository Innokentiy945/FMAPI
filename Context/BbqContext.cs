using FMAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FMAPI.Context;


public class BbqContext : DbContext
{
    public BbqContext(DbContextOptions<BbqContext> options) : base(options)
    {
    }

    public DbSet<BbqModel> bbq { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BbqModel>(entity =>
        {
            entity.ToTable("restaurants");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("uuid");

            entity.Property(x => x.Latitude)
                .HasColumnName("latitude")
                .HasColumnType("numeric");

            entity.Property(x => x.Longitude)
                .HasColumnName("longitude")
                .HasColumnType("numeric");
        });
    }
}

