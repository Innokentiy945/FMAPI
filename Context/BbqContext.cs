using Microsoft.EntityFrameworkCore;

namespace FMAPI.Context;

public class RestarauntContext : DbContext
{
    public RestarauntContext(DbContextOptions<RestarauntContext> options) : base(options)
    {
        
    }
    
    public DbSet<RestarauntModel> restaraunt { get; set; }
}

