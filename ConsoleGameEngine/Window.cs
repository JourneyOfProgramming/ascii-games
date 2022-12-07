namespace ConsoleGameEngine;

public static class Window
{
    private static bool _isOpen;
    private static int _widthInCharacters;
    private static int _heightInCharacters;

    public static string Title { get; set; } = "Console Game Engine Project";
    public static int WidthInCharacters
    { 
        get => _widthInCharacters;
        set
        {
            if (value == _widthInCharacters) return;

            _widthInCharacters = value;
            WidthInPixels = Calculations.WidthInPixelsForCharacters(value);
        }    
    }
    public static int HeightInCharacters
    {
        get => _heightInCharacters;
        set
        {
            if (value == _heightInCharacters) return;

            _heightInCharacters = value;
            HeightInPixels = Calculations.HeightInPixelsForCharacters(value);
        }
    }

    public static int WidthInPixels { get; private set; }
    public static int HeightInPixels { get; private set; }

    public static void Open()
    {
        if (_isOpen) return;

        InitWindow(WidthInPixels, HeightInPixels, Title);
        _isOpen = true;
    }

    public static void Close()
    {
        if (!_isOpen) return;

        CloseWindow();
        _isOpen = false;
    }
}
