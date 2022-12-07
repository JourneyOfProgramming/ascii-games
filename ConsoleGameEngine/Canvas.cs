namespace ConsoleGameEngine;

public static class Canvas
{
    public static int Width => Window.WidthInCharacters;
    public static int Height => Window.HeightInCharacters;

    public static Grid Grid { get; } = new();

    public static int GetRandomX()
        => Random.Shared.Next(Width);
    public static int GetRandomY()
        => Random.Shared.Next(Height);
    public static Point GetRandomPoint()
        => new()
        {
            X = GetRandomX(),
            Y = GetRandomY()
        };
}
