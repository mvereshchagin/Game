using System.Drawing;
using Main;

var player1 = new Player("Vasya");
var player2 = new Player("Petya");

var fieldSize = new Size(10, 10);
var topLeftPoint = new Point(10, 10);
int winCount = 5;

var field = new Field(size: fieldSize, topLeftPoint: topLeftPoint);

var game = new Game(field, winCount, player1, player2);
game.OnGameFinished += (sender, player) =>
{
    // сохраняем в БД результаты и завершаем приложение
};

game.Run();
