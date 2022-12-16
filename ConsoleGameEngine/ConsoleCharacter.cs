namespace ConsoleGameEngine;

public class ConsoleCharacter
{
    public required Type FontMapType { get; init; }
    public required int UnicodeValue { get; init; }
    public required Color Color { get; init; }

    public static ConsoleCharacter Create<TFontMap>(TFontMap character)
        where TFontMap : struct, Enum
        => Create(character, Canvas.ForegroundColor);

    public static ConsoleCharacter Create<TFontMap>(TFontMap character, Color color)
        where TFontMap : struct, Enum
    {
        return new ConsoleCharacter
        {
            FontMapType = typeof(TFontMap),
            UnicodeValue = Convert.ToInt32(character),
            Color = color
        };
    }
}
