namespace ConsoleGameEngine;

public static class Game
{
    public static Action? Start { get; set; }
    public static Action? Update { get; set; }

    public static void Run()
    {
        Window.Open();

        Start?.Invoke();

        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Canvas.BackgroundColor);

            Update?.Invoke();

            Canvas.Render();

            EndDrawing();
        }

        Window.Close();
    }
}
