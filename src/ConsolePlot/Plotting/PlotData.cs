using System;
using System.Collections.Generic;
using System.Linq;
using ConsolePlot.Drawing;

namespace ConsolePlot.Plotting
{
    /// <summary>
    /// Represents the calculated data for plotting.
    /// </summary>
    internal class PlotData
    {
        /// <summary>
        /// Gets the axis intersection point.
        /// </summary>
        public Point Axis { get; }

        /// <summary>
        /// Gets the data bounds of the plot.
        /// </summary>
        public Bounds DataBounds { get; }

        /// <summary>
        /// Gets the drawing area of the plot.
        /// </summary>
        public Rectangle DrawingArea { get; }

        /// <summary>
        /// Gets the series to be plotted.
        /// </summary>
        public List<Series> Series { get; }

        /// <summary>
        /// Gets the X-axis ticks.
        /// </summary>
        public List<Tick> XTicks { get; }

        /// <summary>
        /// Gets the Y-axis ticks.
        /// </summary>
        public List<Tick> YTicks { get; }

        private PlotData(
            Bounds dataBounds,
            Rectangle drawingArea,
            List<Tick> xTicks,
            List<Tick> yTicks,
            Point axis,
            List<Series> series)
        {
            DataBounds = dataBounds;
            DrawingArea = drawingArea;
            XTicks = xTicks;
            YTicks = yTicks;
            Axis = axis;
            Series = series;
        }

        /// <summary>
        /// Calculates the plot data based on the provided series and settings.
        /// </summary>
        public static PlotData Calculate(List<Series> series, PlotSettings settings, int width, int height)
        {
            var initialBounds = CalculateDataBounds(series);

            // Check for the special case where all visual elements are disabled
            if (!settings.Ticks.Labels.IsVisible &&
                !settings.Ticks.IsVisible &&
                !settings.Axis.IsVisible &&
                !settings.Grid.IsVisible)
            {
                return new PlotData(initialBounds, new Rectangle(0, 0, width, height), new List<Tick>(), new List<Tick>(), new Point(0, 0), series);
            }

            var (adjustedYBounds, yTicks) = CalculateAdjustedBoundsAndTicks(settings, initialBounds.YMin,
                initialBounds.YMax, settings.Ticks.DesiredYStep, height, CalculateXTickLabelSize());
            var (adjustedXBounds, xTicks) = CalculateAdjustedBoundsAndTicks(settings, initialBounds.XMin,
                initialBounds.XMax, settings.Ticks.DesiredXStep, width, CalculateYTickLabelSize(yTicks));

            var adjustedBounds = new Bounds(adjustedXBounds.min, adjustedXBounds.max, adjustedYBounds.min,
                adjustedYBounds.max);
            var axisCross = CalculateAxisCross(xTicks, yTicks);
            var drawingArea = CalculateDrawingArea(settings, CalculateXTickLabelSize(), CalculateYTickLabelSize(yTicks),
                width, height);

            return new PlotData(adjustedBounds, drawingArea, xTicks, yTicks, axisCross, series);
        }

        private static ((double min, double max) bounds, List<Tick> ticks) CalculateAdjustedBoundsAndTicks(
            PlotSettings settings,
            double min,
            double max,
            int desiredStepSize,
            int size,
            int labelSize)
        {
            var tickStep = CalculateTickStep(min, max, desiredStepSize, size);
            var ticks = GenerateTicks(min, max, tickStep, settings.Ticks.Labels.Format, false);

            var drawingRange = CalculateDrawingRange(settings, labelSize, size);

            // Adjust the data bounds so that the ticks match the cells
            var (adjustedMin, adjustedMax) = AdjustDataBoundsToTicks(settings, min, max, ticks, drawingRange, labelSize);
            // The new bounds will be wider, so we need to generate new ticks.
            ticks = GenerateTicks(adjustedMin, adjustedMax, tickStep, settings.Ticks.Labels.Format, true);

            return ((adjustedMin, adjustedMax), ticks);
        }

        private static int CalculateXTickLabelSize()
        {
            return 1;
        }

        private static int CalculateYTickLabelSize(List<Tick> yTicks)
        {
            return yTicks.Max(t => t.Label.Length);
        }

        private static Bounds CalculateDataBounds(List<Series> series)
        {
            var xValues = series.SelectMany(s => s.Xs).Where(d => !double.IsInfinity(d) && !double.IsNaN(d));
            var yValues = series.SelectMany(s => s.Ys).Where(d => !double.IsInfinity(d) && !double.IsNaN(d));

            return new Bounds(xValues.Min(), xValues.Max(), yValues.Min(), yValues.Max());
        }

        private static double CalculateTickStep(double min, double max, int desiredStep, int size)
        {
            return NiceNumber((max - min) / (size / desiredStep), true);
        }

        private static List<Tick> GenerateTicks(
            double min,
            double max,
            double stepSize,
            string format,
            bool fitWithinBounds)
        {
            var minStep = (int)(fitWithinBounds ? Math.Ceiling(min / stepSize) : Math.Round(min / stepSize));
            var maxStep = (int)(fitWithinBounds ? Math.Floor(max / stepSize) : Math.Round(max / stepSize));

            return Enumerable.Range(minStep, maxStep - minStep + 1)
                .Select(step =>
                {
                    var tickValue = step * stepSize;
                    return new Tick(tickValue, tickValue.ToString(format));
                })
                .ToList();
        }

        private static double NiceNumber(double range, bool round)
        {
            var exponent = Math.Floor(Math.Log10(range));
            var fraction = range / Math.Pow(10, exponent);

            double niceFraction;

            if (round)
            {
                if (fraction < 1.5)
                    niceFraction = 1;
                else if (fraction < 3)
                    niceFraction = 2;
                else if (fraction < 7)
                    niceFraction = 5;
                else
                    niceFraction = 10;
            }
            else
            {
                if (fraction <= 1)
                    niceFraction = 1;
                else if (fraction <= 2)
                    niceFraction = 2;
                else if (fraction <= 5)
                    niceFraction = 5;
                else
                    niceFraction = 10;
            }

            return niceFraction * Math.Pow(10, exponent);
        }

        /// <summary>
        /// Adjusts the bounds of a graph to fit within a specified number of console characters.
        /// </summary>
        /// <param name="ticks">List of ticks to be displayed on the axis.</param>
        /// <param name="settings">Plot settings</param>
        /// <param name="initialMinValue">Initial minimum value of the data range.</param>
        /// <param name="initialMaxValue">Initial maximum value of the data range.</param>
        /// <param name="totalCharacters">Total number of console characters available for the graph.</param>
        /// <param name="axisLabelWidth">Width of the axis label in characters.</param>
        /// <returns>A tuple containing the adjusted minimum and maximum values for the graph.</returns>
        /// <remarks>
        /// This method ensures that:
        /// <list type="number">
        /// <item><description>All ticks and data points fit within the specified number of characters.</description></item>
        /// <item><description>The axis label (placed to the left of the first tick) doesn't overflow.</description></item>
        /// <item><description>Ticks align with character positions.</description></item>
        /// <item><description>The graph is centered if there's extra space.</description></item>
        /// </list>
        /// The method calculates the optimal character size and adjusts the value range accordingly.
        /// </remarks>
        private static (double min, double max) AdjustDataBoundsToTicks(
            PlotSettings settings,
            double initialMinValue,
            double initialMaxValue,
            List<Tick> ticks,
            int totalCharacters,
            int axisLabelWidth)
        {
            // Find the range of values
            var minTickValue = ticks.First().Value;
            var maxTickValue = ticks.Last().Value;
            var minValue = Math.Min(initialMinValue, minTickValue);
            var maxValue = Math.Max(initialMaxValue, maxTickValue);

            var cross = CalculateAxisCross(ticks);

            // Calculate character size hypotheses
            var characterSizeByFullRange = (maxValue - minValue) / (totalCharacters - 1);

            // Is the data range starts with the label attached to the axis?
            bool isLabelAtStart;
            double minCharacterSize;

            if (settings.Ticks.Labels.IsVisible && settings.Ticks.Labels.AttachToAxis)
            {
                // Choose the larger character size
                var characterSizeWithLabelAtStart = (maxValue - cross) / (totalCharacters - 1 - axisLabelWidth);
                isLabelAtStart = characterSizeWithLabelAtStart > characterSizeByFullRange;
                minCharacterSize = isLabelAtStart ? characterSizeWithLabelAtStart : characterSizeByFullRange;
            }
            else
            {
                isLabelAtStart = false;
                minCharacterSize = characterSizeByFullRange;
            }

            // Adjust character size based on tick intervals
            var tickInterval = (maxTickValue - minTickValue) / (ticks.Count - 1);
            var charactersPerTick = (int)Math.Floor(tickInterval / minCharacterSize);
            var adjustedCharacterSize = tickInterval / charactersPerTick;

            // Calculate the value of the first character
            var firstCharacterValue = isLabelAtStart
                ? minTickValue - axisLabelWidth * adjustedCharacterSize
                : minTickValue - Math.Ceiling((minTickValue - initialMinValue) / adjustedCharacterSize) *
                adjustedCharacterSize;

            // Center the content if there's extra space
            var usedCharacters = (int)Math.Ceiling((initialMaxValue - firstCharacterValue) / adjustedCharacterSize);
            var unusedCharacters = (totalCharacters - usedCharacters) / 2;
            firstCharacterValue -= unusedCharacters * adjustedCharacterSize;

            // Calculate the value of the last character
            var lastCharacterValue = firstCharacterValue + (totalCharacters - 1) * adjustedCharacterSize;

            return (firstCharacterValue, lastCharacterValue);
        }

        private static Point CalculateAxisCross(List<Tick> xTicks, List<Tick> yTicks)
        {
            return new Point(CalculateAxisCross(xTicks), CalculateAxisCross(yTicks));
        }

        private static double CalculateAxisCross(List<Tick> ticks)
        {
            return ticks.Min(t => Math.Abs(t.Value));
        }

        private static Rectangle CalculateDrawingArea(
            PlotSettings settings,
            int xLabelSize,
            int yLabelSize,
            int width,
            int height)
        {
            var drawingAreaX = CalculateDrawingRange(settings, yLabelSize, width);
            var drawingAreaY = CalculateDrawingRange(settings, xLabelSize, height);

            return new Rectangle(width - drawingAreaX, height - drawingAreaY, drawingAreaX, drawingAreaY);
        }

        private static int CalculateDrawingRange(PlotSettings settings, int labelSize, int size)
        {
            return settings.Ticks.Labels.IsVisible && !settings.Ticks.Labels.AttachToAxis
                ? size - labelSize
                : size;
        }
    }
}