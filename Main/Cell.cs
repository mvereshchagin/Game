using System.Drawing;

namespace Main;

public struct Cell
{
    public CellStatus Status { get; set; }
    public Point Position { get; set; }

    public Cell(Point position)
    {
        this.Position = position;
        this.Status = CellStatus.Empty;
    }
    
    public void Draw()
    {}
}