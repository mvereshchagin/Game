using Data;

namespace Main;

public class Configuration
{
    public ISettingsService SettingsService { get; private set; } = null!;
    public IPlayerService PlayerService { get; private set; } = null!;
    
    public IGameResultService GameResultService { get; private set; } = null!;
    

    public Configuration()
    {
        this.Setup();
    }

    public void Setup()
    {
        this.SettingsService = new SettingsService();
        var settings = SettingsService.Read();
        this.PlayerService = new PlayerService(settings.ConnectionString);
        this.GameResultService = new GameResultService(settings.ConnectionString);
    } 
}