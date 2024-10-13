namespace ConsolePlot.Drawing.Tools
{
    /// <summary>
    /// Provides a collection of predefined line brushes.
    /// </summary>
    public static class SystemLineBrushes
    {
        /// <summary>
        /// A brush for drawing thin lines.
        /// </summary>
        public static readonly LineBrush Thin = new LineBrush('│', '─', '┼');

        /// <summary>
        /// A brush for drawing bold lines.
        /// </summary>
        public static readonly LineBrush Bold = new LineBrush('┃', '━', '╋');

        /// <summary>
        /// A brush for drawing double lines.
        /// </summary>
        public static readonly LineBrush Double = new LineBrush('║', '═', '╬');

        /// <summary>
        /// A brush for drawing dotted lines.
        /// </summary>
        public static readonly LineBrush Dotted = new LineBrush('┊', '╌', '┼');

        /// <summary>
        /// A brush for drawing bold dotted lines.
        /// </summary>
        public static readonly LineBrush DottedBold = new LineBrush('┋', '╍', '╋');

        /// <summary>
        /// A brush for drawing dashed lines.
        /// </summary>
        public static readonly LineBrush Dashed = new LineBrush('╎', '╴', '┤');

        /// <summary>
        /// A brush for drawing bold dashed lines.
        /// </summary>
        public static readonly LineBrush DashedBold = new LineBrush('╏', '╸', '┫');
    }

}