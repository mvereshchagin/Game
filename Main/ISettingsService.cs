namespace Main;

public interface ISettingsService
{
    Settings Read();
    void Update(Settings settings);
}