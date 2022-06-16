using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public class MatchMakingContext : DbContext
{
    
    
    // public string DbPath { get; }

    public MatchMakingContext(DbContextOptions<MatchMakingContext> options) : base(options)
    {
        
        // var folder = Environment.SpecialFolder.LocalApplicationData;
        // var path = Environment.GetFolderPath(folder);
        // DbPath = System.IO.Path.Join(path, "matchmaking.db");
    }
    
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    //     => options.UseSqlite($"Data Source={DbPath}");
    
    public DbSet<Match> Matches { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<MatchQueue> MatchQueues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>().HasData(
            new Player{Id=1, Nickname = "Testos"},
            new Player{Id=2, Nickname = "Testas"}
        );
        
    }
}