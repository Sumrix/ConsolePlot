namespace ConsolePlot.Drawing.Tools;

/// <summary>
/// Represents a brush for drawing points directly in the console with a 1x1 resolution, 
/// where each character cell represents a single point. 
/// This brush can be used in both <see cref="ConsoleGraphics"/> and <see cref="VirtualGraphics"/>, 
/// but is optimized for console environments.
/// </summary>
public class ConsolePointBrush : IPointBrush
{
    /// <summary>
    /// Gets the character used to draw points.
    /// </summary>
    public char PointChar { get; }

    /// <summary>
    /// Gets the horizontal resolution of the brush.
    /// </summary>
    public int HorizontalResolution => 1;

    /// <summary>
    /// Gets the vertical resolution of the brush.
    /// </summary>
    public int VerticalResolution => 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsolePointBrush" /> class.
    /// </summary>
    /// <param name="pointChar">The character to use for drawing points.</param>
    public ConsolePointBrush(char pointChar)
    {
        PointChar = pointChar;
    }

    /// <summary>
    /// Renders a point using the brush.
    /// </summary>
    /// <param name="currentChar">The current character at the drawing position.</param>
    /// <param name="x">The x-coordinate within the character cell.</param>
    /// <param name="y">The y-coordinate within the character cell.</param>
    /// <returns>The new character to be drawn.</returns>
    public char RenderPoint(char currentChar, int x, int y) => PointChar;
}