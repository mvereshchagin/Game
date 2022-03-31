using Main;

var configuration = new Configuration();
var project = new Project(
    configuration.SettingsService,
    configuration.PlayerService,
    configuration.GameResultService);
project.Run();

