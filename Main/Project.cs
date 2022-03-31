using System.Drawing;
using Data;

namespace Main;

public class Project
{
    private readonly ISettingsService _settingsService;
    private readonly IPlayerService _playerService;
    private readonly IGameResultService _gameResultService;

    private Player? _player1;
    private Player? _player2;
    
    public Project(ISettingsService settingsService, 
        IPlayerService playerService,
        IGameResultService gameResultService)
    {
        _settingsService = settingsService;
        _playerService = playerService;
        _gameResultService = gameResultService;
    }

    public void Run()
    {
        _player1 = SetPlayer(1);
        _player2 = SetPlayer(2);
        var gameParams = GetGameParams();
        var field = new Field(gameParams.size, new Point(0,0 ));
        var game = new Game(field, gameParams.winCount, _player1, _player2);
        game.OnGameFinished += GameOnOnGameFinished;
        game.Run();
    }

    private void GameOnOnGameFinished(object? sender, Player e)
    {
        SaveResult(sender as Game, e);
    }


    private void SaveResult(Game game, Player player)
    {
        var gameResult = new GameResult() {DateCreate = DateTime.Now, Player = player};
        _gameResultService.Add(gameResult: gameResult);
    }

    private Player SetPlayer(int number)
    {
        var player = AutoAuthorize(number);
        if (player is null)
        {
            Console.WriteLine("Register or Authorize?");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "A":
                    _player1 = RollAuthorize(number);
                    break;
                case "R":
                    _player1 = RollRegister(number);
                    break;
            }
        }

        return player;
    }

    private Player RollAuthorize(int number)
    {
        while (true)
        {
            string name = "";
            var player = _playerService.FindByName(name);
            if (player is not null) 
                return player;
        }
    }

    private Player RollRegister(int number)
    {
        while (true)
        {
            string name = "";
            var player = _playerService.FindByName(name);
            if (player is null)
                player = Register(name, number);
            return player;
        }
    }
    
    private Player? AutoAuthorize(int number)
    {
        var settings = _settingsService.Read();
        int? userId = number == 1 ? settings.Player1 : settings.Player2;
        if (userId is null)
            return null;

        var player = _playerService.FindById(userId.Value);
        return player;
    }

    private Player? Register(string name, int number)
    {
        var player = new Player {Name = name};
        _playerService.Add(player);
        
        var settings = _settingsService.Read();
        if (number == 1)
            settings.Player1 = player.Id;
        else
            settings.Player2 = player.Id;
        _settingsService.Update(settings);

        return player;
    }

    private (Size size, int winCount) GetGameParams()
    {
        var settings = _settingsService.Read();
        return (settings.FieldSize, settings.WinCount);
    }

}