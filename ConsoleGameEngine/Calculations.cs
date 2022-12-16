namespace ConsoleGameEngine;

public static class Calculations
{
    public static int WidthInPixelsForCharacters(int numberOfCharacters)
    {
        return numberOfCharacters * 20;
    }

    public static int HeightInPixelsForCharacters(int numberOfCharacters)
    {
        return numberOfCharacters * 20;
    }

    internal static Vector2 CalculatePositionInPixels(GridCell cell)
    {
        return new Vector2
        {
            X = cell.Position.X * 20,
            Y = cell.Position.Y * 20
        };
    }
}
