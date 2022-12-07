using ConsoleGameEngine;

Window.Title = "Snake";
Window.WidthInCharacters = 60;
Window.HeightInCharacters = 30;

var randomPoint = Canvas.GetRandomPoint();
Canvas.Grid[randomPoint].Entity = "Apple";
var gridCell = Canvas.Grid[randomPoint];

Game.Update = Update;

Game.Run();

static void Update()
{
    DrawText("Hello, world!", 12, 12, 20, Color.BLACK);
}
