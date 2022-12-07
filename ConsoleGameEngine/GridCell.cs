using System.Text;

namespace ConsoleGameEngine;

public class GridCell
{
    public required Point Position { get; init; }
    public string Type { get; set; } = CellTypes.Ground;
    public object? Entity { get; set; }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append(Position);

        if (Entity is not null)
        {
            stringBuilder.Append(' ');

            stringBuilder.Append("Entity: ");
            stringBuilder.Append(Entity);
        }

        stringBuilder.Append(' ');

        stringBuilder.Append("(Type: ");
        stringBuilder.Append(Type);
        stringBuilder.Append(')');

        return stringBuilder.ToString();
    }
}
