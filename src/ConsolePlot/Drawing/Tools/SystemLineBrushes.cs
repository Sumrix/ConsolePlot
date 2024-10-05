namespace ConsolePlot.Drawing.Tools;

/// <summary>
/// Provides a collection of predefined line brushes.
/// </summary>
public static class SystemLineBrushes
{
    public static LineBrush Thin => new LineBrush('│', '─', '┼');
    public static LineBrush Bold => new LineBrush('┃', '━', '╋');
    public static LineBrush Double => new LineBrush('║', '═', '╬');
    public static LineBrush Dotted => new LineBrush('┊', '╌', '┼');
    public static LineBrush DottedBold => new LineBrush('┋', '╍', '╋');
    public static LineBrush Dashed => new LineBrush('╎', '╴', '┤');
    public static LineBrush DashedBold => new LineBrush('╏', '╸', '┫');
}