namespace Data;

public class GameResultService : IGameResultService
{
    private readonly string _connectionString;

    public GameResultService(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public GameResult? FindById(int id)
    {
        using var db = new GameContext(_connectionString);
        return (from gameResult in db.GameResults
            where gameResult.Id == id
            select gameResult).SingleOrDefault();
    }

    public IEnumerable<GameResult> GetAll()
    {
        using var db = new GameContext(_connectionString);
        return (from gameResult in db.GameResults
            select gameResult).ToList();
    }

    public void Add(GameResult gameResult)
    {
        using var db = new GameContext(_connectionString);
        db.GameResults.Add(gameResult);
        db.SaveChanges();
    }

    public IList<GameResult> GetByPlayerId(int id)
    {
        using var db = new GameContext(_connectionString);
        return (from gameResult in db.GameResults
            where gameResult.Player1.Id == id || gameResult.Player2.Id == id
            select gameResult).ToList();
    }
}