using Microsoft.EntityFrameworkCore;

namespace Data;

public class GameContext : DbContext
{
    private readonly string _connectionString;

    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<GameResult> GameResults { get; set; } = null!;

    public GameContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(this._connectionString);
    }
}