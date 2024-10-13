using System;

namespace ConsolePlot.Drawing
{
    /// <summary>
    /// Represents an abstract base class for graphics operations.
    /// </summary>
    /// <typeparam name="TPen">The type of pen used for drawing operations.</typeparam>
    public abstract class AbstractGraphics<TPen>
    {
        /// <summary>
        /// Represents the console image where graphics operations are drawn.
        /// </summary>
        protected readonly ConsoleImage Image;
        private Rectangle _clipBounds;

        /// <summary>
        /// Gets or sets the clipping bounds for drawing operations.
        /// </summary>
        public Rectangle ClipBounds
        {
            get => _clipBounds;
            set => SetClip(value);
        }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        public int ImageHeight { get; }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        public int ImageWidth { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractGraphics{TPen}" /> class.
        /// </summary>
        /// <param name="image">The image to draw on.</param>
        /// <param name="imageWidth">The width of the image.</param>
        /// <param name="imageHeight">The height of the image.</param>
        protected AbstractGraphics(ConsoleImage image, int imageWidth, int imageHeight)
        {
            Image = image;
            _clipBounds = new Rectangle(0, 0, imageWidth, imageHeight);
            ImageWidth = imageWidth;
            ImageHeight = imageHeight;
        }

        /// <summary>
        /// Clears the entire image with the specified character and color.
        /// </summary>
        /// <param name="clearChar">The character to fill the image with.</param>
        /// <param name="clearColor">The color to use for filling.</param>
        public void Clear(char clearChar = ' ', ConsoleColor clearColor = ConsoleColor.White)
        {
            for (var y = 0; y <= Image.Height; y++)
            {
                for (var x = 0; x <= Image.Width; x++)
                {
                    Image.SetPixel(x, y, clearChar, clearColor);
                }
            }
        }

        /// <summary>
        /// Gets the underlying <see cref="ConsoleImage" />.
        /// </summary>
        /// <returns>The <see cref="ConsoleImage" /> this graphics context is drawing on.</returns>
        public ConsoleImage GetImage() => Image;

        /// <summary>
        /// Resets the clipping rectangle to the full image bounds.
        /// </summary>
        public void ResetClip()
        {
            _clipBounds = new Rectangle(0, 0, ImageWidth, ImageHeight);
        }

        /// <summary>
        /// Sets the clipping rectangle for drawing operations.
        /// </summary>
        /// <param name="clip">The new clipping rectangle.</param>
        public void SetClip(Rectangle clip)
        {
            var x = Math.Max(0, clip.X);
            var y = Math.Max(0, clip.Y);
            var width = Math.Min(clip.Width, ImageWidth - x);
            var height = Math.Min(clip.Height, ImageHeight - y);

            _clipBounds = new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Draws a line connecting two points specified by the coordinate pairs.
        /// </summary>
        /// <param name="pen">Pen that determines the color and style of the line.</param>
        /// <param name="x1">The x-coordinate of the first point.</param>
        /// <param name="y1">The y-coordinate of the first point.</param>
        /// <param name="x2">The x-coordinate of the second point.</param>
        /// <param name="y2">The y-coordinate of the second point.</param>
        protected void DrawLineCore(TPen pen, int x1, int y1, int x2, int y2)
        {
            if (!ClipLine(ref x1, ref y1, ref x2, ref y2))
            {
                return; // Line is completely outside the clipping bounds
            }

            var dx = Math.Abs(x2 - x1);
            var dy = Math.Abs(y2 - y1);
            var sx = x1 < x2 ? 1 : -1;
            var sy = y1 < y2 ? 1 : -1;
            var err = dx - dy;

            while (true)
            {
                DrawPointCore(pen, x1, y1);
                if (x1 == x2 && y1 == y2)
                {
                    break;
                }

                var e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }

        /// <summary>
        /// Draws a single point at the specified coordinates.
        /// </summary>
        /// <param name="pen">The pen that determines the color and style of the point.</param>
        /// <param name="x">The x-coordinate of the point.</param>
        /// <param name="y">The y-coordinate of the point.</param>
        protected abstract void DrawPointCore(TPen pen, int x, int y);

        private bool ClipLine(ref int x1, ref int y1, ref int x2, ref int y2)
        {
            // Cohen-Sutherland line clipping algorithm
            const int INSIDE = 0; // 0000
            const int LEFT = 1; // 0001
            const int RIGHT = 2; // 0010
            const int BOTTOM = 4; // 0100
            const int TOP = 8; // 1000

            int ComputeOutCode(int x, int y)
            {
                var code = INSIDE;
                if (x < _clipBounds.Left)
                {
                    code |= LEFT;
                }
                else if (x > _clipBounds.Right)
                {
                    code |= RIGHT;
                }

                if (y < _clipBounds.Bottom)
                {
                    code |= BOTTOM;
                }
                else if (y > _clipBounds.Top)
                {
                    code |= TOP;
                }

                return code;
            }

            var outcode1 = ComputeOutCode(x1, y1);
            var outcode2 = ComputeOutCode(x2, y2);
            var accept = false;

            while (true)
            {
                if ((outcode1 | outcode2) == 0)
                {
                    accept = true;
                    break;
                }

                if ((outcode1 & outcode2) != 0)
                {
                    break;
                }

                int x = 0, y = 0;
                var outcodeOut = outcode1 != 0 ? outcode1 : outcode2;

                if ((outcodeOut & TOP) != 0)
                {
                    x = x1 + (x2 - x1) * (_clipBounds.Top - y1) / (y2 - y1);
                    y = _clipBounds.Top;
                }
                else if ((outcodeOut & BOTTOM) != 0)
                {
                    x = x1 + (x2 - x1) * (_clipBounds.Bottom - y1) / (y2 - y1);
                    y = _clipBounds.Bottom;
                }
                else if ((outcodeOut & RIGHT) != 0)
                {
                    y = y1 + (y2 - y1) * (_clipBounds.Right - x1) / (x2 - x1);
                    x = _clipBounds.Right;
                }
                else if ((outcodeOut & LEFT) != 0)
                {
                    y = y1 + (y2 - y1) * (_clipBounds.Left - x1) / (x2 - x1);
                    x = _clipBounds.Left;
                }

                if (outcodeOut == outcode1)
                {
                    x1 = x;
                    y1 = y;
                    outcode1 = ComputeOutCode(x1, y1);
                }
                else
                {
                    x2 = x;
                    y2 = y;
                    outcode2 = ComputeOutCode(x2, y2);
                }
            }

            return accept;
        }
    }
}