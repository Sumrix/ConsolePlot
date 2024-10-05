namespace ConsolePlot.Drawing.Tools;

/// <summary>
/// Represents a brush that uses Braille characters to achieve higher resolution within a single console character.
/// </summary>
public class BrailleBrush : IPointBrush
{
    private const char BrailleMinChar = '\u2800';
    private const char BrailleMaxChar = '\u28FF';

    public int HorizontalResolution => 2;
    public int VerticalResolution => 4;

    public char RenderPoint(char currentChar, int x, int y)
    {
        if (!IsBrailleCharacter(currentChar))
            currentChar = BrailleMinChar;

        // Calculate the offset for the new dot
        int dotOffset = GetDotOffset(x, y);

        // Add the new dot to the existing character
        return (char)(currentChar | 1 << dotOffset);
    }

    private static bool IsBrailleCharacter(char c) => c is >= BrailleMinChar and <= BrailleMaxChar;

    private int GetDotOffset(int x, int y)
    {
        // Dot order in Braille characters:
        // 6 7
        // 2 5
        // 1 4
        // 0 3
        return (x, y) switch
        {
            (0, 0) => 6,
            (0, 1) => 2,
            (0, 2) => 1,
            (0, 3) => 0,
            (1, 0) => 7,
            (1, 1) => 5,
            (1, 2) => 4,
            (1, 3) => 3,
            _ => throw new ArgumentOutOfRangeException($"Invalid Braille cell coordinates: ({x}, {y})")
        };
    }
}