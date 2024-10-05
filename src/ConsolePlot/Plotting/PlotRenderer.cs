using ConsolePlot.Drawing;

namespace ConsolePlot.Plotting;

/// <summary>
/// Renders the plot data onto a console image.
/// </summary>
internal class PlotRenderer
{
    private readonly ConsoleImage _image;
    private readonly PlotData _plotData;
    private readonly PlotSettings _settings;

    /// <summary>
    /// Initializes a new instance of the PlotRenderer class.
    /// </summary>
    public PlotRenderer(ConsoleImage image, PlotData plotData, PlotSettings settings)
    {
        _image = image;
        _plotData = plotData;
        _settings = settings;
    }

    /// <summary>
    /// Draws the plot on the console image.
    /// </summary>
    public void Draw()
    {
        var graphics = new ConsoleGraphics(_image);
        var converter = CreateConverter(_plotData.DataBounds, _plotData.DrawingArea);
        var graphGraphics = new GraphGraphics(graphics, converter);

        graphics.Clear();

        graphics.SetClip(_plotData.DrawingArea);

        if (_settings.Grid.IsVisible)
            DrawGrid(graphGraphics);

        if (_settings.Axis.IsVisible)
            DrawAxes(graphGraphics);

        if (_settings.Ticks.IsVisible)
            DrawTicks(graphGraphics);

        graphics.ResetClip();

        DrawSeries(graphGraphics);

        if (_settings.Ticks.Labels.IsVisible)
            DrawLabels(graphics, converter);
    }

    private void DrawGrid(GraphGraphics graphics)
    {
        foreach (var tick in _plotData.XTicks)
            graphics.DrawVertical(_settings.Grid.Pen, tick.Value);
        foreach (var tick in _plotData.YTicks)
            graphics.DrawHorizontal(_settings.Grid.Pen, tick.Value);
    }

    private void DrawAxes(GraphGraphics graphics)
    {
        graphics.DrawHorizontal(_settings.Axis.Pen, _plotData.Axis.Y);
        graphics.DrawVertical(_settings.Axis.Pen, _plotData.Axis.X);
    }

    private void DrawTicks(GraphGraphics graphics)
    {
        foreach (var tick in _plotData.XTicks)
            graphics.DrawVertical(_settings.Ticks.Pen, tick.Value, _plotData.Axis.Y);
        foreach (var tick in _plotData.YTicks)
            graphics.DrawHorizontal(_settings.Ticks.Pen, _plotData.Axis.X, tick.Value);
    }

    private void DrawSeries(GraphGraphics graphics)
    {
        foreach (var series in _plotData.Series)
        {
            graphics.DrawLines(series.Pen, series.Xs, series.Ys);
        }
    }

    private void DrawLabels(ConsoleGraphics graphics, CoordinateConverter converter)
    {
        DrawXLabels(graphics, converter);
        DrawYLabels(graphics, converter);
    }

    private void DrawXLabels(ConsoleGraphics graphics, CoordinateConverter converter)
    {
        var y = _settings.Ticks.Labels.AttachToAxis
            ? (int)Math.Round(converter.ConvertY(_plotData.Axis.Y))
            : 0;

        foreach (var tick in _plotData.XTicks)
        {
            var x = (int)Math.Round(converter.ConvertX(tick.Value));
            if (!_settings.Ticks.Labels.AttachToAxis || tick.Value != _plotData.Axis.X)
            {
                graphics.DrawString(tick.Label, _settings.Ticks.Labels.Color, x - tick.Label.Length / 2, y - 1, ensureVisible: true);
            }
        }
    }

    private void DrawYLabels(ConsoleGraphics graphics, CoordinateConverter converter)
    {
        var x = _settings.Ticks.Labels.AttachToAxis
            ? (int)Math.Round(converter.ConvertX(_plotData.Axis.X))
            : _plotData.YTicks.Select(t => t.Label.Length).Max();

        foreach (var tick in _plotData.YTicks)
        {
            var y = (int)Math.Round(converter.ConvertY(tick.Value));
            if (_settings.Ticks.Labels.AttachToAxis && tick.Value == _plotData.Axis.Y)
            {
                y -= 1;
            }
            graphics.DrawString(tick.Label, _settings.Ticks.Labels.Color, x - tick.Label.Length, y, ensureVisible: true);
        }
    }

    private CoordinateConverter CreateConverter(Bounds dataBounds, Rectangle drawingArea)
    {
        return new CoordinateConverter(
            dataBounds.XMin, dataBounds.XMax, drawingArea.Left, drawingArea.Right,
            dataBounds.YMin, dataBounds.YMax, drawingArea.Bottom, drawingArea.Top);
    }
}