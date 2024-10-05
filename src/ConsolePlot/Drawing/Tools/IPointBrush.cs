namespace ConsolePlot.Drawing.Tools;

/// <summary>
/// Defines the interface for a virtual brush used in high-resolution drawing operations, 
/// typically in environments where the drawing surface has a finer resolution than the console.
/// Implementations of this interface can be used in <see cref="VirtualGraphics"/> but not in <see cref="ConsoleGraphics"/>.
/// </summary>
public interface IPointBrush
{
    /// <summary>
    /// Gets the horizontal resolution of the brush.
    /// </summary>
    int HorizontalResolution { get; }

    /// <summary>
    /// Gets the vertical resolution of the brush.
    /// </summary>
    int VerticalResolution { get; }

    /// <summary>
    /// Renders a point using the brush.
    /// </summary>
    /// <param name="currentChar">The current character at the drawing position.</param>
    /// <param name="x">The x-coordinate within the character cell.</param>
    /// <param name="y">The y-coordinate within the character cell.</param>
    /// <returns>The new character to be drawn.</returns>
    char RenderPoint(char currentChar, int x, int y);
}