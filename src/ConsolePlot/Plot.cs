using ConsolePlot.Drawing;
using ConsolePlot.Drawing.Tools;
using ConsolePlot.Plotting;

namespace ConsolePlot;

/// <summary>
/// Represents a plot that can be drawn on a console.
/// </summary>
public class Plot
{
    private static readonly ConsoleColor[] AvailableColors =
    [
        ConsoleColor.Blue,
        ConsoleColor.Green,
        ConsoleColor.Cyan,
        ConsoleColor.Red,
        ConsoleColor.Magenta,
        ConsoleColor.Yellow,
        ConsoleColor.DarkBlue,
        ConsoleColor.DarkGreen,
        ConsoleColor.DarkCyan,
        ConsoleColor.DarkRed,
        ConsoleColor.DarkMagenta,
        ConsoleColor.DarkYellow
    ];
    private readonly ConsoleImage _image;
    private readonly PlotSettings _settings;

    /// <summary>
    /// Gets the axis settings for the plot.
    /// </summary>
    public AxisSettings Axis => _settings.Axis;

    /// <summary>
    /// Gets the grid settings for the plot.
    /// </summary>
    public GridSettings Grid => _settings.Grid;

    /// <summary>
    /// Gets the tick settings for the plot.
    /// </summary>
    public TickSettings Ticks => _settings.Ticks;

    /// <summary>
    /// Gets the collection of data series added to the plot.
    /// </summary>
    public List<Series> Series { get; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Plot" /> class.
    /// </summary>
    /// <param name="width">The width of the plot in console characters.</param>
    /// <param name="height">The height of the plot in console characters.</param>
    /// <exception cref="ArgumentException">Thrown when width or height is not positive.</exception>
    public Plot(int width, int height)
    {
        if (width <= 0 || height <= 0)
        {
            throw new ArgumentException("Width and height must be positive.");
        }

        _image = new(width, height);
        _settings = new();
    }

    /// <summary>
    /// Adds a new series to the plot.
    /// </summary>
    /// <param name="xs">The X values of the series.</param>
    /// <param name="ys">The Y values of the series.</param>
    /// <param name="pen">The pen to use for drawing the series (optional).</param>
    /// <returns>The <see cref="Plot" /> instance for method chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when xs and ys have different lengths.</exception>
    public Series AddSeries(
        IReadOnlyCollection<double> xs,
        IReadOnlyCollection<double> ys,
        PointPen? pen = null)
    {
        if (xs.Count != ys.Count)
        {
            throw new ArgumentException("X and Y collections must have the same length.");
        }

        pen ??= new(_settings.DefaultGraphBrush ?? SystemPointBrushes.Braille, GetNextAvailableColor());

        var series = new Series(xs, ys, pen);
        Series.Add(new(xs, ys, pen));
        return series;
    }

    /// <summary>
    /// Draws the plot on the console image.
    /// </summary>
    public void Draw()
    {
        _settings.Validate();
        var plotData = PlotData.Calculate(Series, _settings, _image.Width, _image.Height);
        var renderer = new PlotRenderer(_image, plotData, _settings);

        renderer.Draw();
    }

    /// <summary>
    /// Gets the underlying <see cref="ConsoleImage" />.
    /// </summary>
    /// <returns>The <see cref="ConsoleImage" /> this plotting context is drawing on.</returns>
    public ConsoleImage GetImage() => _image;

    /// <summary>
    /// Renders the plot on the console.
    /// </summary>
    public void Render() => _image.Render();

    private ConsoleColor GetNextAvailableColor()
    {
        var usedColors = new HashSet<ConsoleColor>();

        // Include colors of existing series
        usedColors.UnionWith(Series.Select(s => s.Pen.Color));

        // Include axis color if visible
        if (_settings.Axis.IsVisible)
        {
            usedColors.Add(_settings.Axis.Pen.Color);
        }

        // Include grid color if visible
        if (_settings.Grid.IsVisible)
        {
            usedColors.Add(_settings.Grid.Pen.Color);
        }

        // Return the first available color
        return AvailableColors.First(c => !usedColors.Contains(c));
    }
}