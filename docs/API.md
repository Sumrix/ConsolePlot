# ConsolePlot API Documentation

## Plot Class

The `Plot` class is the main entry point for creating and customizing plots.

```csharp
public class Plot
{
    public Plot(int width, int height);

    public AxisSettings Axis { get; }
    public GridSettings Grid { get; }
    public TickSettings Ticks { get; }
    public List<Series> Series { get; }

    public Series AddSeries(IReadOnlyCollection<double> xs, IReadOnlyCollection<double> ys, PointPen? pen = null);
    public void Draw();
    public ConsoleImage GetImage();
    public void Render();
}
```

### Constructor

- `Plot(int width, int height)`: Creates a new plot with the specified width and height.

### Properties

- `Axis`: Gets the axis settings for the plot.
- `Grid`: Gets the grid settings for the plot.
- `Ticks`: Gets the tick settings for the plot.
- `Series`: Gets the collection of data series added to the plot.

### Methods

- `AddSeries(IReadOnlyCollection<double> xs, IReadOnlyCollection<double> ys, PointPen? pen = null)`: Adds a new series to the plot. Returns the created `Series` object.
- `Draw()`: Prepares the plot image.
- `GetImage()`: Returns the `ConsoleImage` object representing the plot.
- `Render()`: Renders the plot to the console.

## AxisSettings Class

```csharp
public class AxisSettings
{
    public bool IsVisible { get; set; }
    public LinePen Pen { get; set; }
}
```

- `IsVisible`: Determines whether the axis is visible.
- `Pen`: The pen used to draw the axis.

## GridSettings Class

```csharp
public class GridSettings
{
    public bool IsVisible { get; set; }
    public LinePen Pen { get; set; }
}
```

- `IsVisible`: Determines whether the grid is visible.
- `Pen`: The pen used to draw the grid.

## TickSettings Class

```csharp
public class TickSettings
{
    public bool IsVisible { get; set; }
    public LinePen Pen { get; set; }
    public int DesiredXStep { get; set; }
    public int DesiredYStep { get; set; }
    public LabelSettings Labels { get; }
}
```

- `IsVisible`: Determines whether ticks are visible.
- `Pen`: The pen used to draw the ticks.
- `DesiredXStep`: The desired step between ticks on the x-axis.
- `DesiredYStep`: The desired step between ticks on the y-axis.
- `Labels`: Gets the label settings for the ticks.

## LabelSettings Class

```csharp
public class LabelSettings
{
    public bool IsVisible { get; set; }
    public ConsoleColor Color { get; set; }
    public bool AttachToAxis { get; set; }
    public string Format { get; set; }
}
```

- `IsVisible`: Determines whether labels are visible.
- `Color`: The color of the labels.
- `AttachToAxis`: Determines whether labels should be attached to the axis.
- `Format`: The string format used for label values.

## Drawing Tools

### SystemLineBrushes

```csharp
public static class SystemLineBrushes
{
    public static LineBrush Thin { get; }
    public static LineBrush Bold { get; }
    public static LineBrush Double { get; }
    public static LineBrush Dotted { get; }
    public static LineBrush DottedBold { get; }
    public static LineBrush Dashed { get; }
    public static LineBrush DashedBold { get; }
}
```

Provides a set of predefined line brushes for various line styles.

### SystemPointBrushes

```csharp
public static class SystemPointBrushes
{
    public static IPointBrush Braille { get; }
    public static IPointBrush Quadrant { get; }
    public static ConsolePointBrush Block { get; }
    public static ConsolePointBrush Star { get; }
    public static ConsolePointBrush Dot { get; }
}
```

Provides a set of predefined point brushes for various point styles.

### LinePen and PointPen

```csharp
public class LinePen
{
    public LineBrush Brush { get; }
    public ConsoleColor Color { get; }

    public LinePen(LineBrush brush, ConsoleColor color);
}

public class PointPen
{
    public IPointBrush Brush { get; }
    public ConsoleColor Color { get; }

    public PointPen(IPointBrush brush, ConsoleColor color);
}
```

`LinePen` and `PointPen` are used to define the appearance of lines and points in the plot.

## Usage Example

```csharp
using ConsolePlot;
using ConsolePlot.Drawing.Tools;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var plt = new Plot(80, 22);

var xs = Enumerable.Range(-30, 61).Select(i => i * 0.1).ToArray();
var ys = xs.Select(Math.Sin).ToArray();

plt.AddSeries(xs, ys, new PointPen(SystemPointBrushes.Braille, ConsoleColor.Blue));

plt.Axis.IsVisible = true;
plt.Grid.IsVisible = true;
plt.Ticks.IsVisible = true;
plt.Ticks.Labels.IsVisible = true;

plt.Draw();
plt.Render();
```

This example creates a simple sine wave plot with customized settings.