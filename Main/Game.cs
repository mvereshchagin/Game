using Data;

namespace Main;

public class Game
{
    public event EventHandler<Player>? OnGameFinished; 

    public int WinCount { get; }
    public Field Field { get; }
    public Player Player1 { get; }
    public Player Player2 { get; }

    private Player _currentPlayer;
    
    
    public Game(Field field, int winCount, Player player1, Player player2)
    {
        this.Field = field;
        this.WinCount = winCount;
        this.Player1 = player1;
        this.Player2 = player2;
    }

    public void Run()
    {
        this.Field.Draw();
        
        // Console.WriteLine("Press ESC to stop");
        do {
            while (! Console.KeyAvailable)
            {
                var position = Console.GetCursorPosition();
                
                switch (@Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                        if(position.Left - 1 >= 0)
                            Console.SetCursorPosition(position.Left - 1, position.Top);
                        break;
                    case ConsoleKey.RightArrow:
                        if( position.Left + 1 >= 0 )
                            Console.SetCursorPosition(position.Left + 1, position.Top);
                        break;
                    case ConsoleKey.UpArrow:
                        if(position.Top - 1 >= 0)
                            Console.SetCursorPosition(position.Left, position.Top - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        if(position.Top + 1 > 0)
                            Console.SetCursorPosition(position.Left, position.Top + 1);
                        break;
                    case ConsoleKey.Enter:
                        HandleEnter();
                        break;
                }

                Console.CursorVisible = true;
            }       
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
    }

    private void HandleEnter()
    {
        if(CheckWin())
            OnGameFinished?.Invoke(this, _currentPlayer);
        SwitchPlayer();
    }

    private bool CheckWin()
    {
        return false;
    }

    private void SwitchPlayer()
    {
        if (_currentPlayer == Player1)
        {
            _currentPlayer = Player2;
            return;
        }

        if (_currentPlayer == Player2)
        {
            _currentPlayer = Player1;
            return;
        }
    }
}