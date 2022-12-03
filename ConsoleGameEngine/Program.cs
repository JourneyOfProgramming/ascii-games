using Raylib_cs;
using static Raylib_cs.Raylib;

InitWindow(800, 480, "Hello World");

while (!WindowShouldClose())
{
    BeginDrawing();
    ClearBackground(Color.WHITE);

    DrawText("Hello, world!", 12, 12, 20, Color.BLACK);

    EndDrawing();
}

CloseWindow();
