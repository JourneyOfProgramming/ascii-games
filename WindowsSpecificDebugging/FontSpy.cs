using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace WindowsSpecificDebugging;

public static class FontSpy
{
    public static void LookAt(FontInformation font, Action<FontItem>? fontItemCallback = null, Action<FontFamily>? fontFamilyCallback = null)
    {
        var count = 0;
        var allFaceNames = new HashSet<string>();
        var fontFileName = System.IO.Path.GetFileName(font.Path);
        Debug.WriteLine($"Looking at {fontFileName}...");

        var families = Fonts.GetFontFamilies(font.Path);
        foreach (var family in families)
        {
            fontFamilyCallback?.Invoke(family);

            var typefaces = family.GetTypefaces();
            foreach (var typeface in typefaces)
            {
                var faceNames = string.Join(", ", typeface.FaceNames.Select(x => x.Value));
                if (!typeface.TryGetGlyphTypeface(out var glyph))
                {
                    Debug.WriteLine($"Couldn't get glyph typeface for '{faceNames}'");
                    continue;
                }

                if (font.AllowedFaceNames?.Contains(faceNames) == false)
                {
                    Debug.WriteLine($"Face name(s) '{faceNames}' not allowed. Skipping...");
                    continue;
                }

                allFaceNames.Add(faceNames);

                var index = 0;
                foreach (var kvp in glyph.CharacterToGlyphMap)
                {
                    if (font.MaximumItemsToLoad >= 0 && count >= font.MaximumItemsToLoad)
                    {
                        Debug.WriteLine($"Reached maximum of items to load ({font.MaximumItemsToLoad}). Exitting...");
                        return;
                    }

                    var rune = new Rune(kvp.Key);
                    if (font.IndicesToInclude?.Any(indexRange => index >= indexRange.Start.Value && index <= indexRange.End.Value) == false)
                    {
                        index++;
                        continue;
                    }

                    count++;

                    var fontItem = new FontItem
                    {
                        Rune = rune,
                        Index = index
                    };
                    fontItemCallback?.Invoke(fontItem);

                    index++;
                }
            }
        }

        var result = $"Finished looking at {fontFileName}: {string.Join(", ", allFaceNames)} ({count})";
        Debug.WriteLine(result);
    }
}
