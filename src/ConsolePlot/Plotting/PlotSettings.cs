using ConsolePlot.Drawing.Tools;

namespace ConsolePlot.Plotting;

/// <summary>
/// Represents the overall plot settings.
/// </summary>
public class PlotSettings
{
    /// <summary>
    /// Gets the axis settings for the plot.
    /// </summary>
    public AxisSettings Axis { get; } = new AxisSettings();

    /// <summary>
    /// Gets the grid settings for the plot.
    /// </summary>
    public GridSettings Grid { get; } = new GridSettings();

    /// <summary>
    /// Gets the tick settings for the plot.
    /// </summary>
    public TickSettings Ticks { get; } = new TickSettings();

    /// <summary>
    /// Gets or sets the default brush used to draw series if none is provided.
    /// </summary>
    public IPointBrush DefaultGraphBrush { get; set; } = SystemPointBrushes.Braille;

    /// <summary>
    /// Validates all settings.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when any setting is invalid.</exception>
    public void Validate()
    {
        Axis.Validate();
        Grid.Validate();
        Ticks.Validate();
    }
}