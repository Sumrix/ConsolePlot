namespace ConsolePlot.Drawing.Tools
{
    /// <summary>
    /// Provides a collection of predefined point brushes.
    /// </summary>
    public static class SystemPointBrushes
    {
        /// <summary>
        /// A brush for drawing Braille points.
        /// </summary>
        public static readonly IPointBrush Braille = new BrailleBrush();

        /// <summary>
        /// A brush for drawing quadrant points.
        /// </summary>
        public static readonly IPointBrush Quadrant = new QuadrantBrush();

        /// <summary>
        /// A brush for drawing block characters.
        /// </summary>
        public static readonly ConsolePointBrush Block = new ConsolePointBrush('█');

        /// <summary>
        /// A brush for drawing star characters.
        /// </summary>
        public static readonly ConsolePointBrush Star = new ConsolePointBrush('*');

        /// <summary>
        /// A brush for drawing dot characters.
        /// </summary>
        public static readonly ConsolePointBrush Dot = new ConsolePointBrush('•');
    }
}