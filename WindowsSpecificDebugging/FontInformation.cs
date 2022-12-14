using System;
using System.Collections.Generic;

namespace WindowsSpecificDebugging;

public class FontInformation
{
    public const string RegularFaceName = "Regular";

    public required string Name { get; init; }
    public required string Path { get; init; }
    public IEnumerable<string>? AllowedFaceNames { get; set; } = new List<string> { RegularFaceName };
    public IEnumerable<Range>? IndicesToInclude { get; set; }
    public int MaximumItemsToLoad { get; set; } = -1;
}
