namespace ConsolePlot.Drawing.Tools
{
    /// <summary>
    /// Represents a brush that uses quadrant characters for higher resolution drawing.
    /// </summary>
    public class QuadrantBrush : IPointBrush
    {
        private static readonly string QuadrantChars = " ▖▗▄▘▌▚▙▝▞▐▟▀▛▜█";

        /// <summary>
        /// Gets the horizontal resolution of the brush.
        /// </summary>
        public int HorizontalResolution => 2;

        /// <summary>
        /// Gets the vertical resolution of the brush.
        /// </summary>
        public int VerticalResolution => 2;

        /// <summary>
        /// Renders a point using quadrant characters.
        /// </summary>
        /// <param name="currentChar">The current character at the position.</param>
        /// <param name="x">The x-coordinate within the character cell (0 or 1).</param>
        /// <param name="y">The y-coordinate within the character cell (0 or 1).</param>
        /// <returns>The new character representing the updated quadrant state.</returns>
        public char RenderPoint(char currentChar, int x, int y)
        {
            var index = QuadrantChars.IndexOf(currentChar);
            if (index == -1)
            {
                index = 0;
            }

            index |= 1 << (y * 2 + x);
            return QuadrantChars[index];
        }
    }
}