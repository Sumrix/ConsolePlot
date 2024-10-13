using System;
using System.Collections.Generic;
using ConsolePlot.Drawing;
using ConsolePlot.Drawing.Tools;

namespace ConsolePlot.Plotting
{
    /// <summary>
    /// Provides drawing capabilities for graphs using double-precision coordinates.
    /// </summary>
    internal class GraphGraphics
    {
        private readonly ConsoleGraphics _graphics;
        private readonly CoordinateConverter _converter;

        public GraphGraphics(ConsoleGraphics graphics, CoordinateConverter converter)
        {
            _graphics = graphics;
            _converter = converter;
        }

        public void DrawLines(PointPen pen, IReadOnlyList<double> xs, IReadOnlyList<double> ys)
        {
            var graphics = new VirtualGraphics(_graphics.GetImage(), pen);
            var converter = new CoordinateConverter(
                _converter.SourceX.Min,
                _converter.SourceX.Max,
                _converter.TargetX.Min * pen.Brush.HorizontalResolution,
                _converter.TargetX.Max * pen.Brush.HorizontalResolution,
                _converter.SourceY.Min,
                _converter.SourceY.Max,
                _converter.TargetY.Min * pen.Brush.VerticalResolution,
                _converter.TargetY.Max * pen.Brush.VerticalResolution);

            int? x1 = null, y1 = null;

            for (int i = 0; i < xs.Count; i++)
            {
                var (x2, y2) = Convert(xs[i], ys[i]);

                if (x1 != null && y1 != null && x2 != null && y2 != null)
                    graphics.DrawLine(x1.Value, y1.Value, x2.Value, y2.Value);

                x1 = x2;
                y1 = y2;
            }

            (int?, int?) Convert(double x, double y)
            {
                if (double.IsNaN(x) || double.IsNaN(y)) return (null, null);

                return (ConvertInfinity(x) ?? (int)Math.Round(converter.ConvertX(x)),
                    ConvertInfinity(y) ?? (int)Math.Round(converter.ConvertY(y)));
            }

            int? ConvertInfinity(double value)
            {
                if (double.IsPositiveInfinity(value)) return int.MaxValue;
                if (double.IsNegativeInfinity(value)) return int.MinValue;
                return null;
            }
        }

        public void DrawVertical(LinePen pen, double x) =>
            _graphics.DrawVertical(pen, ConvertX(x));

        public void DrawVertical(LinePen pen, double x, double y) =>
            _graphics.DrawVertical(pen, ConvertX(x), ConvertY(y));

        public void DrawHorizontal(LinePen pen, double y) =>
            _graphics.DrawHorizontal(pen, ConvertY(y));

        public void DrawHorizontal(LinePen pen, double x, double y) =>
            _graphics.DrawHorizontal(pen, ConvertX(x), ConvertY(y));

        private int ConvertX(double x) => (int)Math.Round(_converter.ConvertX(x));
        private int ConvertY(double y) => (int)Math.Round(_converter.ConvertY(y));
    }
}