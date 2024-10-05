namespace ConsolePlot.Drawing.Tools;

/// <summary>
/// Represents a brush for drawing lines with different styles for vertical, horizontal, and crossing lines.
/// </summary>
public class LineBrush
{
    /// <summary>
    /// Gets the character used for vertical lines.
    /// </summary>
    public char Vertical { get; }

    /// <summary>
    /// Gets the character used for horizontal lines.
    /// </summary>
    public char Horizontal { get; }

    /// <summary>
    /// Gets the character used for line intersections.
    /// </summary>
    public char Cross { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LineBrush"/> class.
    /// </summary>
    /// <param name="vertical">The character for vertical lines.</param>
    /// <param name="horizontal">The character for horizontal lines.</param>
    /// <param name="cross">The character for line intersections.</param>
    public LineBrush(char vertical, char horizontal, char cross)
    {
        Vertical = vertical;
        Horizontal = horizontal;
        Cross = cross;
    }
}