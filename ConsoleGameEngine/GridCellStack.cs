using System.Text;

namespace ConsoleGameEngine;

public class GridCellStack
{
	public GridCell this[int y] => Cells[y];

    public List<GridCell> Cells { get; }

	public GridCellStack(int x)
	{
		Cells = Enumerable.Repeat(0, Canvas.Height)
			.Select((_, y) => new GridCell
			{
				Position = new Point
				{
					X = x,
					Y = y
				}
			})
			.ToList();
	}

	public override string ToString()
		=> string.Join(Environment.NewLine, Cells);
}
