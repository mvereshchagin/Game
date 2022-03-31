namespace Data;

public class PlayerService : IPlayerService
{
    private readonly string _connectionString;

    public PlayerService(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public Player? FindById(int id)
    {
        using var db = new GameContext(_connectionString);
        return (from player in db.Players
            where player.Id == id
            select player).SingleOrDefault();
    }

    public Player? FindByName(string name)
    {
        using var db = new GameContext(_connectionString);
        return (from player in db.Players
            where String.Equals(player.Name, name, StringComparison.InvariantCultureIgnoreCase)
            select player).SingleOrDefault();
    }

    public void Add(Player player)
    {
        using var db = new GameContext(_connectionString);
        db.Players.Add(player);
        db.SaveChanges();
    }
}