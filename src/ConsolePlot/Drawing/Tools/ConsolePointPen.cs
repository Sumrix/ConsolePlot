using System;

namespace ConsolePlot.Drawing.Tools
{
    /// <summary>
    /// Represents a pen for drawing points.
    /// </summary>
    public class ConsolePointPen
    {
        /// <summary>
        /// Gets the brush used by this pen.
        /// </summary>
        public ConsolePointBrush Brush { get; }

        /// <summary>
        /// Gets the color of this pen.
        /// </summary>
        public ConsoleColor Color { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsolePointPen"/> class.
        /// </summary>
        /// <param name="brush">The brush to use for drawing points.</param>
        /// <param name="color">The color of the pen.</param>
        public ConsolePointPen(ConsolePointBrush brush, ConsoleColor color)
        {
            Brush = brush;
            Color = color;
        }
    }
}