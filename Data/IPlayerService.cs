namespace Data;

public interface IPlayerService
{
    Player? FindById(int id);
    Player? FindByName(string name);
    void Add(Player player);
}