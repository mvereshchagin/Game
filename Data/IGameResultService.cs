namespace Data;

public interface IGameResultService
{
    GameResult? FindById(int id);
    IEnumerable<GameResult> GetAll();
    void Add(GameResult gameResult);

    IList<GameResult> GetByPlayerId(int id);
}