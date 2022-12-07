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
}
