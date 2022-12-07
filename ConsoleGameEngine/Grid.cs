namespace ConsoleGameEngine;

public class Grid
{
    public GridCellStack this[int x] => CellStacks[x];

    public GridCell this[Point point] => this[point.X, point.Y];
    public GridCell this[int x, int y] => CellStacks[x][y];

    public List<GridCellStack> CellStacks { get; }

	public Grid()
	{
        CellStacks = Enumerable.Repeat(0, Canvas.Width)
            .Select((_, x) => new GridCellStack(x))
            .ToList();
    }

    public override string ToString()
        => string.Join(Environment.NewLine, CellStacks);
}
