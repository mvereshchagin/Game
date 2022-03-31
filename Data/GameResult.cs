using System.Drawing;

namespace Data;

public class GameResult
{
    public int Id { get; set; }
    public Player WinPlayer { get; set; }
    public Player Player1 { get; set; }
    public Player Player2 { get; set; }
    public Size FieldSize { get; set; }
    public int WinCount { get; set; }
    public DateTime DateCreate { get; set; }
    public long Duration { get; set; }
}