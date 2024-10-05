using ConsolePlot.Drawing.Tools;

namespace ConsolePlot.Plotting;

/// <summary>
/// Represents the tick settings for the plot axes.
/// </summary>
public class TickSettings
{
    /// <summary>
    /// Gets or sets a value indicating whether ticks are visible.
    /// </summary>
    public bool IsVisible { get; set; } = true;

    /// <summary>
    /// Gets or sets the pen used to draw the ticks.
    /// </summary>
    public LinePen Pen { get; set; } = new LinePen(SystemLineBrushes.Thin, ConsoleColor.White);

    /// <summary>
    /// Gets or sets the desired step between ticks on the horizontal axis.
    /// </summary>
    public int DesiredXStep { get; set; } = 11;

    /// <summary>
    /// Gets or sets the desired step between ticks on the vertical axis.
    /// </summary>
    public int DesiredYStep { get; set; } = 3;

    /// <summary>
    /// Gets the label settings associated with the ticks.
    /// </summary>
    public LabelSettings Labels { get; } = new LabelSettings();

    /// <summary>
    /// Validates the tick settings.
    /// </summary>
    public void Validate()
    {
        Labels.Validate();
    }
}