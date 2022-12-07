using ConsoleGameEngine;

Window.Title = "Snake";
Window.WidthInCharacters = 60;
Window.HeightInCharacters = 30;

Game.Update = Update;

Game.Run();

static void Update()
{
    DrawText("Hello, world!", 12, 12, 20, Color.BLACK);
}
