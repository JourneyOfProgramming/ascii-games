namespace ConsoleGameEngine;

public static class Game
{
    public static Action? Update { get; set; }

    public static void Run()
    {
        Window.Open();

        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.WHITE);

            Update?.Invoke();

            EndDrawing();
        }

        Window.Close();
    }
}
