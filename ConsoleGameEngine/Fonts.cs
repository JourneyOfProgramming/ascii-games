namespace ConsoleGameEngine;

public static class Fonts
{
    private static readonly Dictionary<Type, FontData> _fonts = new();

    public static FontData Get<TFontMap>()
        => Get(typeof(TFontMap));

    public static FontData Get(Type fontMapType)
    {
        return _fonts[fontMapType];
    }

    public static void Load<TFontMap>(string fontPath, int fontSize)
        where TFontMap : struct, Enum
    {
        var unicodeValues = Enum.GetValues<TFontMap>()
            .Select(enumValue => Convert.ToInt32(enumValue))
            .ToArray();

        var font = LoadFontEx(fontPath, fontSize, unicodeValues, unicodeValues.Length);
        _fonts.Add(typeof(TFontMap), new FontData
        {
            Data = font,
            FontMapType = typeof(TFontMap),
            Size = fontSize
        });
    }

    public static void UnloadAll()
    {
        foreach (var (_, font) in _fonts)
        {
            UnloadFont(font.Data);
        }
        _fonts.Clear();
    }
}
