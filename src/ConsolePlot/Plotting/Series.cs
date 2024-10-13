using System;
using System.Collections.Generic;
using System.Linq;
using ConsolePlot.Drawing.Tools;

namespace ConsolePlot.Plotting
{
    /// <summary>
    /// Represents a series of data points in a plot.
    /// </summary>
    public class Series
    {
        /// <summary>
        /// Gets the X values of the series.
        /// </summary>
        public IReadOnlyList<double> Xs { get; }

        /// <summary>
        /// Gets the Y values of the series.
        /// </summary>
        public IReadOnlyList<double> Ys { get; }

        /// <summary>
        /// Gets the pen used to draw the series.
        /// </summary>
        public PointPen Pen { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Series"/> class.
        /// </summary>
        /// <param name="xs">The X values of the series.</param>
        /// <param name="ys">The Y values of the series.</param>
        /// <param name="pen">The pen to use for drawing the series.</param>
        /// <exception cref="ArgumentException">Thrown when xs and ys have different lengths or when pen is null.</exception>
        public Series(IEnumerable<double> xs, IEnumerable<double> ys, PointPen pen)
        {
            Xs = xs.ToList();
            Ys = ys.ToList();

            if (Xs.Count != Ys.Count)
                throw new ArgumentException("X and Y collections must have the same length.");

            Pen = pen ?? throw new ArgumentNullException(nameof(pen));
        }
    }
}