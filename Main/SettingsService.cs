using System.ComponentModel;
using System.Drawing;
using Microsoft.Extensions.Configuration;
using WritableJsonConfiguration;

namespace Main;

public class SettingsService : ISettingsService
{
    private readonly IConfigurationRoot _appConfig;
    
    public SettingsService()
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        this._appConfig = configurationBuilder.Add<WritableJsonConfigurationSource>(
            (Action<WritableJsonConfigurationSource>) (s =>
            {
                s.FileProvider = null;
                s.Path = "appsettings.json";
                s.Optional = false;
                s.ReloadOnChange = true;
                s.ResolveFileProvider();
            })).Build();
    }

    public Settings Read()
    {
        Settings settings = new Settings();
        settings.ConnectionString = _appConfig.GetConnectionString("Game");
        settings.Player1 = _appConfig["player1"] is not null ? Convert.ToInt32(_appConfig["player1"]) : null;
        settings.Player2 = _appConfig["player2"] is not null ? Convert.ToInt32(_appConfig["player2"]) : null;
        settings.WinCount = _appConfig["WinCount"] is not null ? Convert.ToInt32(_appConfig["WinCount"]) : 0;
        if (_appConfig["FieldSize"] is not null)
        {
            var sizeStr = _appConfig["FieldSize"];
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Size));
            settings.FieldSize = (Size)tc.ConvertFromString(sizeStr);
        }
        return settings;
    }

    public void Update(Settings settings)
    {
        this._appConfig["player1"] = settings.Player1?.ToString();
        this._appConfig["player2"] = settings.Player2?.ToString();
        this._appConfig["WinCount"] = settings.WinCount.ToString();
        
        TypeConverter tc = TypeDescriptor.GetConverter(typeof(Size));
        string sizeString = tc.ConvertToString(settings.FieldSize);
        this._appConfig["FieldSize"] = sizeString;
    }
}