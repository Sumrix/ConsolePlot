namespace ConsolePlot.Drawing.Tools;

/// <summary>
/// Provides a collection of predefined point brushes.
/// </summary>
public static class SystemPointBrushes
{
    public static IPointBrush Braille => new BrailleBrush();
    public static IPointBrush Quadrant => new QuadrantBrush();
    public static ConsolePointBrush Block => new ConsolePointBrush('█');
    public static ConsolePointBrush Star => new ConsolePointBrush('*');
    public static ConsolePointBrush Dot => new ConsolePointBrush('•');
}