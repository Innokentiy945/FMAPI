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

        modelBuilder.Entity<BbqModel>()
            .Property(x => x.Location)
            .HasColumnType("geography(Point,4326)");
    }
}

