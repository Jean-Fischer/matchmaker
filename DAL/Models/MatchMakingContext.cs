using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public class MatchMakingContext : DbContext
{
    public MatchMakingContext(DbContextOptions<MatchMakingContext> options) : base(options)
    {
    }


    public DbSet<Match> Matches { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<MatchQueue> MatchQueues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>().HasData(
            new Player { Id = 1, Nickname = "Jean", Rank = 1100 },
            new Player { Id = 2, Nickname = "Martin", Rank = 1200 },
            new Player { Id = 3, Nickname = "Greg", Rank = 1200 },
            new Player { Id = 4, Nickname = "Cosmin", Rank = 1200 },
            new Player { Id = 5, Nickname = "Kevin", Rank = 1200 }
        );
    }
}