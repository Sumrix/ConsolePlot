using System;

namespace ConsolePlot.Drawing.Tools
{
    /// <summary>
    /// Represents a virtual pen for drawing operations.
    /// </summary>
    public class PointPen
    {
        /// <summary>
        /// Gets the brush used by this pen.
        /// </summary>
        public IPointBrush Brush { get; }

        /// <summary>
        /// Gets the color of this pen.
        /// </summary>
        public ConsoleColor Color { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointPen"/> class.
        /// </summary>
        /// <param name="brush">The brush to use for drawing.</param>
        /// <param name="color">The color of the pen.</param>
        public PointPen(IPointBrush brush, ConsoleColor color)
        {
            Brush = brush;
            Color = color;
        }
    }
}