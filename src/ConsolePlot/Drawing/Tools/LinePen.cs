using System;

namespace ConsolePlot.Drawing.Tools
{
    /// <summary>
    /// Represents a pen for drawing lines with a specific brush and color.
    /// </summary>
    public class LinePen
    {
        /// <summary>
        /// Gets the brush used by this pen.
        /// </summary>
        public LineBrush Brush { get; }

        /// <summary>
        /// Gets the color of this pen.
        /// </summary>
        public ConsoleColor Color { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinePen"/> class.
        /// </summary>
        /// <param name="brush">The brush to use for drawing lines.</param>
        /// <param name="color">The color of the pen.</param>
        public LinePen(LineBrush brush, ConsoleColor color)
        {
            Brush = brush;
            Color = color;
        }
    }
}