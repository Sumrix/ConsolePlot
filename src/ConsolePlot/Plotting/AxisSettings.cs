using ConsolePlot.Drawing.Tools;

namespace ConsolePlot.Plotting;

/// <summary>
/// Represents the axis settings for the plot.
/// </summary>
public class AxisSettings
{
    /// <summary>
    /// Gets or sets a value indicating whether the axis is visible.
    /// </summary>
    public bool IsVisible { get; set; } = true;

    /// <summary>
    /// Gets or sets the pen used to draw the axis.
    /// </summary>
    public LinePen Pen { get; set; } = new LinePen(SystemLineBrushes.Thin, ConsoleColor.White);

    /// <summary>
    /// Validates the axis settings.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the pen is null.</exception>
    public void Validate()
    {
        if (Pen == null)
            throw new InvalidOperationException("Axis pen cannot be null.");
    }
}