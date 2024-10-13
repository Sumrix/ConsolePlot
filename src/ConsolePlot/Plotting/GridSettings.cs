using System;
using ConsolePlot.Drawing.Tools;

namespace ConsolePlot.Plotting
{
    /// <summary>
    /// Represents the grid settings for the plot.
    /// </summary>
    public class GridSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether the grid is visible.
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// Gets or sets the pen used to draw the grid.
        /// </summary>
        public LinePen Pen { get; set; } = new LinePen(SystemLineBrushes.Dashed, ConsoleColor.DarkGray);

        /// <summary>
        /// Validates the grid settings.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the pen is null.</exception>
        public void Validate()
        {
            if (Pen == null)
                throw new InvalidOperationException("Grid pen cannot be null.");
        }
    }
}