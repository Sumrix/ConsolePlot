namespace ConsolePlot.Drawing;

/// <summary>
/// Represents an image that can be drawn on the console.
/// </summary>
public class ConsoleImage
{
    private readonly Pixel[,] _buffer;

    /// <summary>
    /// Gets the width of the image.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Gets the height of the image.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleImage"/> class with the specified width and height.
    /// </summary>
    /// <param name="width">The width of the image.</param>
    /// <param name="height">The height of the image.</param>
    public ConsoleImage(int width, int height)
    {
        Width = width;
        Height = height;
        _buffer = new Pixel[height, width];
    }

    /// <summary>
    /// Sets a pixel at the specified coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate of the pixel.</param>
    /// <param name="y">The y-coordinate of the pixel.</param>
    /// <param name="c">The character to set.</param>
    /// <param name="foregroundColor">The foreground color of the pixel.</param>
    public void SetPixel(int x, int y, char c, ConsoleColor foregroundColor)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            _buffer[y, x] = new Pixel(c, foregroundColor);
        }
    }

    /// <summary>
    /// Gets the pixel at the specified coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate of the pixel.</param>
    /// <param name="y">The y-coordinate of the pixel.</param>
    /// <returns>The pixel at the specified coordinates.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when coordinates are out of bounds.</exception>
    public Pixel GetPixel(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            throw new ArgumentOutOfRangeException(nameof(x), "Coordinates are out of bounds.");
        }
        return _buffer[y, x];
    }

    /// <summary>
    /// Renders the image to the console.
    /// </summary>
    public void Render()
    {
        for (int y = Height - 1; y >= 0; y--)
        {
            for (int x = 0; x < Width; x++)
            {
                var pixel = _buffer[y, x];
                Console.ForegroundColor = pixel.ForegroundColor;
                Console.Write(pixel.Character);
            }
            Console.WriteLine();
        }
        Console.ResetColor();
    }
}