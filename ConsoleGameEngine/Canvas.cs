namespace ConsoleGameEngine;

public static class Canvas
{
    public static int Width => Window.WidthInCharacters;
    public static int Height => Window.HeightInCharacters;

    public static Color ForegroundColor { get; set; } = Color.SKYBLUE;
    public static Color BackgroundColor { get; set; } = Color.BLACK;

    public static Grid Grid { get; } = new();

    public static int GenerateRandomX()
        => Random.Shared.Next(Width);
    public static int GenerateRandomY()
        => Random.Shared.Next(Height);
    public static Point GenerateRandomPoint()
        => new()
        {
            X = GenerateRandomX(),
            Y = GenerateRandomY()
        };

    public static GridCell GetRandomCell()
        => Grid[GenerateRandomPoint()];

    public static void Render()
    {
        for (var x = 0; x < Width; x++)
        {
            for(var y = 0; y < Height; y++)
            {
                var cell = Grid[x, y];

                var character = cell.Charater;
                if (character is null)
                {
                    continue;
                }

                var font = Fonts.Get(character.FontMapType);
                var position = Calculations.CalculatePositionInPixels(cell);
                DrawTextCodepoint(font.Data, character.UnicodeValue,
                    position, font.Size, character.Color);
            }
        }
    }
}
