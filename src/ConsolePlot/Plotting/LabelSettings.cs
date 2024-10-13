using System;

namespace ConsolePlot.Plotting
{
    /// <summary>
    /// Represents the label settings for tick marks.
    /// </summary>
    public class LabelSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether tick labels are visible.
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// Gets or sets the color of the tick labels.
        /// </summary>
        public ConsoleColor Color { get; set; } = ConsoleColor.White;

        /// <summary>
        /// Gets or sets a value indicating whether labels should be attached to the axis.
        /// </summary>
        public bool AttachToAxis { get; set; } = true;

        /// <summary>
        /// Gets or sets the string format used to format tick labels.
        /// </summary>
        public string Format { get; set; } = "G4";

        /// <summary>
        /// Validates the labels settings.
        /// </summary>
        public void Validate()
        {
            // No validation needed for current properties
        }
    }
}