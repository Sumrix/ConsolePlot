using ConsolePlot.Drawing.Tools;

namespace ConsolePlot.Drawing;

/// <summary>
/// Represents a graphics context for drawing directly onto a console using characters as pixels.
/// </summary>
public class ConsoleGraphics : AbstractGraphics<ConsolePointPen>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleGraphics" /> class with the specified image.
    /// </summary>
    /// <param name="image">The <see cref="ConsoleImage" /> to draw on.</param>
    public ConsoleGraphics(ConsoleImage image) : base(image, image.Width, image.Height)
    {
    }

    /// <summary>
    /// Draws a horizontal line between two x-coordinates at the specified y-coordinate.
    /// </summary>
    /// <param name="pen"><see cref="LinePen" /> that determines the color and style of the line.</param>
    /// <param name="x1">The first x-coordinate.</param>
    /// <param name="x2">The second x-coordinate.</param>
    /// <param name="y">The y-coordinate of the line.</param>
    public void DrawHorizontal(LinePen pen, int x1, int x2, int y)
    {
        if (!ClipBounds.ContainsY(y))
        {
            return;
        }

        if (x1 > x2)
        {
            (x1, x2) = (x2, x1);
        }

        var start = Math.Max(ClipBounds.Left, x1);
        var end = Math.Min(ClipBounds.Right, x2);

        for (var x = start; x <= end; x++)
        {
            DrawHorizontalCore(pen, x, x, y);
        }
    }

    /// <summary>
    /// Draws a horizontal line character at the specified coordinates.
    /// </summary>
    /// <param name="pen"><see cref="LinePen" /> that determines the color and style of the line.</param>
    /// <param name="x">The x-coordinate of the line character.</param>
    /// <param name="y">The y-coordinate of the line character.</param>
    public void DrawHorizontal(LinePen pen, int x, int y)
    {
        if (ClipBounds.Contains(x, y))
        {
            DrawHorizontalCore(pen, x, x, y);
        }
    }

    /// <summary>
    /// Draws a horizontal line across the entire clipping area at the specified y-coordinate.
    /// </summary>
    /// <param name="pen"><see cref="LinePen" /> that determines the color and style of the line.</param>
    /// <param name="y">The y-coordinate of the line.</param>
    public void DrawHorizontal(LinePen pen, int y)
    {
        if (ClipBounds.ContainsY(y))
        {
            DrawHorizontalCore(pen, ClipBounds.Left, ClipBounds.Right, y);
        }
    }

    /// <summary>
    /// Draws a line connecting two points specified by the coordinate pairs.
    /// </summary>
    /// <param name="pen"><see cref="ConsolePointPen" /> that determines the color and style of the line.</param>
    /// <param name="x1">The x-coordinate of the first point.</param>
    /// <param name="y1">The y-coordinate of the first point.</param>
    /// <param name="x2">The x-coordinate of the second point.</param>
    /// <param name="y2">The y-coordinate of the second point.</param>
    public void DrawLine(ConsolePointPen pen, int x1, int y1, int x2, int y2)
    {
        DrawLineCore(pen, x1, y1, x2, y2);
    }

    /// <summary>
    /// Draws a single point at the specified coordinates.
    /// </summary>
    /// <param name="pen"><see cref="ConsolePointPen" /> that determines the color and style of the point.</param>
    /// <param name="x">The x-coordinate of the point.</param>
    /// <param name="y">The y-coordinate of the point.</param>
    public void DrawPoint(ConsolePointPen pen, int x, int y)
    {
        DrawPointCore(pen, x, y);
    }

    /// <summary>
    /// Draws the specified string at the specified location with the specified color and,
    /// optionally, in the specified direction while ensuring visibility if required.
    /// </summary>
    /// <param name="s">String to draw.</param>
    /// <param name="color">The color of the text.</param>
    /// <param name="x">The x-coordinate of the drawn text.</param>
    /// <param name="y">The y-coordinate of the drawn text.</param>
    /// <param name="direction">The direction of the drawn text.</param>
    /// <param name="ensureVisible">Whether to ensure the entire text is visible within the image bounds.</param>
    /// <exception cref="InvalidOperationException">Thrown when the text exceeds image dimensions and ensureVisible is true.</exception>
    public void DrawString(
        string s,
        ConsoleColor color,
        int x,
        int y,
        TextDirection direction = TextDirection.Horizontal,
        bool ensureVisible = false)
    {
        if (ensureVisible)
        {
            if (direction == TextDirection.Horizontal)
            {
                if (s.Length > ClipBounds.Width)
                {
                    throw new InvalidOperationException("Text length exceeds clipping width.");
                }

                x = Math.Clamp(x, ClipBounds.Left, ClipBounds.Right - s.Length + 1);
                y = Math.Clamp(y, ClipBounds.Bottom, ClipBounds.Top);
            }
            else
            {
                if (s.Length > ClipBounds.Height)
                {
                    throw new InvalidOperationException("Text length exceeds clipping height.");
                }

                x = Math.Clamp(x, ClipBounds.Left, ClipBounds.Right);
                y = Math.Clamp(y, ClipBounds.Bottom + s.Length - 1, ClipBounds.Top);
            }
        }

        for (var i = 0; i < s.Length; i++)
        {
            var currentX = direction == TextDirection.Horizontal ? x + i : x;
            var currentY = direction == TextDirection.Horizontal ? y : y - i;

            if (ClipBounds.Contains(currentX, currentY))
            {
                Image.SetPixel(currentX, currentY, s[i], color);
            }
            else if (ensureVisible)
            {
                break; // Stop drawing if out of bounds and visibility is ensured
            }
        }
    }

    /// <summary>
    /// Draws a vertical line between two y-coordinates at the specified x-coordinate.
    /// </summary>
    /// <param name="pen"><see cref="LinePen" /> that determines the color and style of the line.</param>
    /// <param name="x">The x-coordinate of the line.</param>
    /// <param name="y1">The first y-coordinate of the line.</param>
    /// <param name="y2">The second y-coordinate of the line.</param>
    public void DrawVertical(LinePen pen, int x, int y1, int y2)
    {
        if (!ClipBounds.ContainsX(x))
        {
            return;
        }

        if (y1 > y2)
        {
            (y1, y2) = (y2, y1);
        }

        var start = Math.Max(ClipBounds.Bottom, y1);
        var end = Math.Min(ClipBounds.Top, y2);

        for (var y = start; y <= end; y++)
        {
            DrawVerticalCore(pen, x, y, y);
        }
    }

    /// <summary>
    /// Draws a vertical line character at the specified coordinates.
    /// </summary>
    /// <param name="pen"><see cref="LinePen" /> that determines the color and style of the line.</param>
    /// <param name="x">The x-coordinate of the line character.</param>
    /// <param name="y">The y-coordinate of the line character.</param>
    public void DrawVertical(LinePen pen, int x, int y)
    {
        if (ClipBounds.Contains(x, y))
        {
            DrawVerticalCore(pen, x, y, y);
        }
    }

    /// <summary>
    /// Draws a vertical line across the entire clipping area at the specified x-coordinate.
    /// </summary>
    /// <param name="pen"><see cref="LinePen" /> that determines the color and style of the line.</param>
    /// <param name="x">The x-coordinate of the line.</param>
    public void DrawVertical(LinePen pen, int x)
    {
        if (ClipBounds.ContainsX(x))
        {
            DrawVerticalCore(pen, x, ClipBounds.Bottom, ClipBounds.Top);
        }
    }

    protected override void DrawPointCore(ConsolePointPen pen, int x, int y)
    {
        if (ClipBounds.Contains(x, y))
        {
            Image.SetPixel(x, y, pen.Brush.PointChar, pen.Color);
        }
    }

    private void DrawHorizontalCore(LinePen pen, int start, int end, int y)
    {
        for (var x = start; x <= end; x++)
        {
            var pixel = Image.GetPixel(x, y);
            var symbol = pixel.Character == pen.Brush.Vertical || pixel.Character == pen.Brush.Cross
                ? pen.Brush.Cross
                : pen.Brush.Horizontal;
            Image.SetPixel(x, y, symbol, pen.Color);
        }
    }

    private void DrawVerticalCore(LinePen pen, int x, int start, int end)
    {
        for (var y = start; y <= end; y++)
        {
            var pixel = Image.GetPixel(x, y);
            var symbol = pixel.Character == pen.Brush.Horizontal || pixel.Character == pen.Brush.Cross
                ? pen.Brush.Cross
                : pen.Brush.Vertical;
            Image.SetPixel(x, y, symbol, pen.Color);
        }
    }
}