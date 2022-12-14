using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WindowsSpecificDebugging;

public class WindowData : DataContext
{
    #region Constants

    private const string BootstrapIconsPath = @"C:\YouTube\Resources\ASCII Games\Bootstrap Icons\bootstrap-icons.ttf";
    private const string FontAwesomeBrandsPath = @"C:\YouTube\Resources\ASCII Games\FontAwesome\Font Awesome 6 Brands-Regular-400.otf";
    private const string FontAwesomeRegularPath = @"C:\YouTube\Resources\ASCII Games\FontAwesome\Font Awesome 6 Free-Regular-400.otf";
    private const string FontAwesomeSolidPath = @"C:\YouTube\Resources\ASCII Games\FontAwesome\Font Awesome 6 Free-Solid-900.otf";
    private const string BocartesFritosPath = @"C:\YouTube\Resources\ASCII Games\BocartesFritosRegular-WjOA.ttf";
    private const string DonjonikonsPath = @"C:\YouTube\Resources\ASCII Games\DonjonikonsRegular-VABy.ttf";

    private static readonly List<Range> FontAwesomeRegularIndices = new()
    {
        new Range(1, 1),
        new Range(3, 5),
        new Range(10, 11),
        new Range(13, 13),
        new Range(16, 25),
        new Range(28, 58),
        new Range(65, 90),
        new Range(137, 137),
        new Range(142, 142),
        new Range(1312, 1672),
    };
    private static readonly List<Range> FontAwesomeBrandsIndices = new()
    {
        new Range(1, 1),
        new Range(3, 5),
        new Range(10, 11),
        new Range(13, 13),
        new Range(16, 25),
        new Range(28, 58),
        new Range(65, 90),
        new Range(1312, 1807),
    };

    #endregion

    #region Properties

    public ICommand RuneLeftClickedCommand { get; }
    public ICommand RuneRightClickedCommand { get; }
    public ICommand RuneMiddleClickedCommand { get; }

    /// <summary>
    /// List of items displayed in the grid.
    /// </summary>
    public ObservableCollection<FontItem> FontItems { get; } = new();

    /// <summary>
    /// Font list that allows the user to pick one of them which then populates the grid (<see cref="FontItems"/>).
    /// </summary>
    public ObservableCollection<FontInformation> AvailableFonts { get; } = new()
    {
        new FontInformation
        {
            Name = "Bootstrap Icons",
            Path = BootstrapIconsPath,
            //MaximumItemsToLoad = 100
        },
        new FontInformation
        {
            Name = "FontAwesome",
            Path = FontAwesomeRegularPath,
            IndicesToInclude = FontAwesomeRegularIndices
        },
        new FontInformation
        {
            Name = "FontAwesome Brands",
            Path = FontAwesomeBrandsPath,
            IndicesToInclude = FontAwesomeBrandsIndices
        },
        new FontInformation
        {
            Name = "Donjonikons",
            Path = DonjonikonsPath
        },
        new FontInformation
        {
            Name = "BocartesFritos",
            Path = BocartesFritosPath
        },
    };

    private FontInformation? _selectedFont;

    /// <summary>
    /// Font that is currently selected in the list. Whenever a new one is selected,
    /// the grid gets populated with the runes (by using <see cref="LoadFontItemsForFont(FontInformation)"/>).
    /// </summary>
    public FontInformation? SelectedFont
    {
        get => _selectedFont;
        set
        {
            SetPropertyAndNotifySubscribers(ref _selectedFont, value);

            if (value is not null)
            {
                BeforeRunes = "";
                Runes = "";
                AfterRunes = "";
                Task.Run(() => LoadFontItemsForFont(value));
            }
        }
    }

    private FontFamily? _fontFamily = new("Segoe UI");

    /// <summary>
    /// Font family based on the currently selected font.
    /// </summary>
    public FontFamily? FontFamily
    {
        get => _fontFamily;
        set => SetPropertyAndNotifySubscribers(ref _fontFamily, value);
    }

    private string? _beforeRunes = "Select font on the left and then click";
    public string? BeforeRunes
    {
        get => _beforeRunes;
        set => SetPropertyAndNotifySubscribers(ref _beforeRunes, value);
    }

    private string? _runes = " ";
    public string? Runes
    {
        get => _runes;
        set => SetPropertyAndNotifySubscribers(ref _runes, value);
    }

    private string? _afterRunes = "on a rune using left, middle or right mouse button";
    public string? AfterRunes
    {
        get => _afterRunes;
        set => SetPropertyAndNotifySubscribers(ref _afterRunes, value);
    }

    #endregion

    public WindowData()
	{
        RuneLeftClickedCommand = new RelayCommand<FontItem>(OnRuneLeftClicked);
        RuneRightClickedCommand = new RelayCommand<FontItem>(OnRuneRightClicked);
        RuneMiddleClickedCommand = new RelayCommand<FontItem>(OnRuneMiddleClicked);
    }

    #region Interactions

    private void OnRuneLeftClicked(FontItem fontItem)
    {
        var unicodeValue = fontItem.Rune.Value;
        var unicodeValueString = $"0x{unicodeValue:X4}";

        BeforeRunes = "Unicode value of ";
        Runes = fontItem.Rune.ToString();
        AfterRunes = $": {unicodeValueString}";
        Clipboard.SetText(unicodeValueString);
    }

    private void OnRuneRightClicked(FontItem fontItem)
    {
        BeforeRunes = "Index of ";
        Runes = fontItem.Rune.ToString();
        AfterRunes = $": {fontItem.Index}";
        Clipboard.SetText(fontItem.Index.ToString());
    }

    private void OnRuneMiddleClicked(FontItem fontItem)
    {
        BeforeRunes = "";
        Runes = fontItem.Rune.ToString();
        AfterRunes = "";
        Clipboard.SetText(Runes);
    }

    #endregion

    public void LoadFontItemsForFont(FontInformation font)
    {
        RenderThreadDispatcher.Invoke(FontItems.Clear);

        FontSpy.LookAt(font,
            fontItem => RenderThreadDispatcher.Invoke(() =>
            {
                FontItems.Add(fontItem);
            }),
            fontFamily => RenderThreadDispatcher.Invoke(() =>
            {
                FontFamily = fontFamily;
            })
        );
    }
}
