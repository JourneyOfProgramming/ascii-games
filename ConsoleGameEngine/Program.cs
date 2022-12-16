using ConsoleGameEngine;

Window.Title = "Snake";
Window.WidthInCharacters = 60;
Window.HeightInCharacters = 30;

Game.Start = Start;
Game.Update = Update;

Game.Run();

static void Start()
{
    const string FontAwesomeBrandsPath = @"C:\YouTube\Resources\ASCII Games\FontAwesome\Font Awesome 6 Brands-Regular-400.otf";
    Fonts.Load<FontAwesome>(FontAwesomeBrandsPath, 30);

    Canvas.GetRandomCell().Charater = ConsoleCharacter.Create(FontAwesome.Apple);
}

static void Update()
{
}
