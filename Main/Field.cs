using System.Drawing;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;

namespace Main;

public class Field
{
    public Size Size { get; }
    public Point TopLeftPoint { get; }

    private readonly Cell[,] _cells;

    private int border = 1;

    public Field(Size size, Point topLeftPoint)
    {
        this.Size = size;
        this.TopLeftPoint = topLeftPoint;

        _cells = new Cell[size.Width, size.Width];

        for(int i =0; i < this.Size.Width; i++)
        for(int j =0; j < this.Size.Height; j++)
        {
            _cells[i, j] = new Cell(position: new Point(i, j));
        }
        Console.WriteLine("We are here");
    }

    public (int Left, int Top) PositionToField((int Left, int Top) position)
    {
        return (position.Left + this.TopLeftPoint.X + border, position.Top + this.TopLeftPoint.Y + border);
    }

    public bool IsInside((int Left, int Top) position)
    {
        return position.Left >= border && position.Left < Size.Width + border && position.Top >= border && position.Top < Size.Height + border;
    }

    public Cell? GetFocusedCell()
    {
        var position = Console.GetCursorPosition();
        var fieldPosition = this.PositionToField(position);
        if (this.IsInside(fieldPosition))
        {
            return this._cells[fieldPosition.Left, fieldPosition.Top];
        }

        return null;
    }
    
    public void Draw()
    {
        this.DrawBorder();
        foreach (var cell in _cells)
        {
            cell.Draw();
        }
    }

    private void DrawBorder()
    {
        Console.SetCursorPosition(this.TopLeftPoint.X + border, this.TopLeftPoint.Y);
        for(int i = 0; i < this.Size.Width; i++)
            Console.Write("-");
        
        Console.SetCursorPosition(this.TopLeftPoint.X + border, this.TopLeftPoint.Y + this.Size.Height + border);
        for(int i = 0; i < this.Size.Width; i++)
            Console.Write("-");
        
        for (int i = 0; i < this.Size.Height; i++)
        {
            Console.SetCursorPosition(this.TopLeftPoint.X, this.TopLeftPoint.Y + border + i);
            Console.Write("|");
            Console.SetCursorPosition(this.TopLeftPoint.X + this.Size.Width + border, this.TopLeftPoint.Y + border + i);
            Console.Write("|");
        }
    }
}