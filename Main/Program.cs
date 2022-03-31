using System.Drawing;
using Main;
using Microsoft.VisualBasic.CompilerServices;

var configuration = new Configuration();
var project = new Project(configuration.SettingsService,
    configuration.PlayerService,
    configuration.GameResultService);
project.Run();

