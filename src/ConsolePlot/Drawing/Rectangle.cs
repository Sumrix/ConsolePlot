namespace ConsolePlot.Drawing;

/// <summary>
/// Represents a rectangle with position and size.
/// </summary>
public readonly struct Rectangle
{
    /// <summary>
    /// Gets the X-coordinate of the top-left corner of the rectangle.
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Gets the Y-coordinate of the top-left corner of the rectangle.
    /// </summary>
    public int Y { get; }

    /// <summary>
    /// Gets the width of the rectangle.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Gets the height of the rectangle.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Gets the X-coordinate of the left edge of the rectangle.
    /// </summary>
    public int Left => X;

    /// <summary>
    /// Gets the Y-coordinate of the top edge of the rectangle.
    /// </summary>
    public int Top => Y + Height - 1;

    /// <summary>
    /// Gets the X-coordinate of the right edge of the rectangle.
    /// </summary>
    public int Right => X + Width - 1;

    /// <summary>
    /// Gets the Y-coordinate of the bottom edge of the rectangle.
    /// </summary>
    public int Bottom => Y;

    /// <summary>
    /// Initializes a new instance of the <see cref="Rectangle" /> struct.
    /// </summary>
    /// <param name="x">The x-coordinate of the top-left corner.</param>
    /// <param name="y">The y-coordinate of the top-left corner.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    public Rectangle(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Determines if the specified point defined by (x, y) coordinates is inside the rectangle.
    /// </summary>
    /// <param name="x">The x-coordinate of the point to test.</param>
    /// <param name="y">The t-coordinate of the point to test.</param>
    /// <returns>
    /// <see langword="true" /> if the point is inside the rectangle; otherwise, <see langword="false" />.
    /// </returns>
    public bool Contains(int x, int y)
    {
        return ContainsX(x) && ContainsY(y);
    }

    /// <summary>
    /// Determines if the specified x-coordinate is within the horizontal bounds of the rectangle.
    /// </summary>
    /// <param name="x">The x-coordinate to test.</param>
    /// <returns>
    /// <see langword="true" /> if the x-coordinate is within the rectangle's horizontal bounds; otherwise, <see langword="false" />.
    /// </returns>
    public bool ContainsX(int x)
    {
        return x >= Left && x <= Right;
    }

    /// <summary>
    /// Determines if the specified y-coordinate is within the vertical bounds of the rectangle.
    /// </summary>
    /// <param name="y">The y-coordinate to test.</param>
    /// <returns>
    /// <see langword="true" /> if the y-coordinate is within the rectangle's vertical bounds; otherwise, <see langword="false" />.
    /// </returns>
    public bool ContainsY(int y)
    {
        return y >= Bottom && y <= Top;
    }
}