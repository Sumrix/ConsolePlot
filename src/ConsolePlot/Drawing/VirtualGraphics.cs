using System;
using ConsolePlot.Drawing.Tools;

namespace ConsolePlot.Drawing
{
    /// <summary>
    /// Represents a graphics context for drawing on a virtual image with higher resolution.
    /// </summary>
    public class VirtualGraphics : AbstractGraphics<PointPen>
    {
        private readonly PointPen _pen;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualGraphics"/> class.
        /// </summary>
        /// <param name="image">The image to draw on.</param>
        /// <param name="pen">The virtual pen to use for drawing.</param>
        public VirtualGraphics(ConsoleImage image, PointPen pen)
            : base(
                image,
                image.Width * pen.Brush.HorizontalResolution,
                image.Height * pen.Brush.VerticalResolution)
        {
            _pen = pen;
        }

        /// <summary>
        /// Draws a line connecting two points specified by the coordinate pairs.
        /// </summary>
        /// <param name="x1">The x-coordinate of the first point.</param>
        /// <param name="y1">The y-coordinate of the first point.</param>
        /// <param name="x2">The x-coordinate of the second point.</param>
        /// <param name="y2">The y-coordinate of the second point.</param>
        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            DrawLineCore(_pen, x1, y1, x2, y2);
        }

        /// <summary>
        /// Draws a single point at the specified coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the point.</param>
        /// <param name="y">The y-coordinate of the point.</param>
        public void DrawPoint(int x, int y)
        {
            DrawPointCore(_pen, x, y);
        }
        
        /// <inheritdoc/>
        protected override void DrawPointCore(PointPen pen, int x, int y)
        {
            var bufferX = Math.DivRem(x, pen.Brush.HorizontalResolution, out var subX);
            var bufferY = Math.DivRem(y, pen.Brush.VerticalResolution, out var subY);

            if (ClipBounds.Contains(bufferX, bufferY))
            {
                var pixel = Image.GetPixel(bufferX, bufferY);
                var oldChar = pixel.ForegroundColor == pen.Color ? pixel.Character : ' ';
                var newChar = pen.Brush.RenderPoint(oldChar, subX, subY);
                Image.SetPixel(bufferX, bufferY, newChar, pen.Color);
            }
        }
    }
}