using ConsolePlot.Drawing.Tools;

namespace ConsolePlot.Examples;

internal static class Program
{
    private static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose an example to run:");
            Console.WriteLine("1. Basic Example");
            Console.WriteLine("2. Multiple Series Example");
            Console.WriteLine("3. All Settings Demonstration");
            Console.WriteLine("4. Non-Unicode Output Example");
            Console.WriteLine("5. Exit");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    BasicExample();
                    break;
                case "2":
                    MultipleSeries();
                    break;
                case "3":
                    AllSettingsDemonstration();
                    break;
                case "4":
                    NonUnicodeExample();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    private static void BasicExample()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Create a new plot with width 80 and height 22
        var plt = new Plot(80, 22);

        // Generate some sample data
        double[] xs = [1, 2, 3, 4, 5];
        double[] ys = [1, 4, 9, 16, 25];

        // Add the data series to the plot
        plt.AddSeries(xs, ys);

        // Draw the plot (prepare the image)
        plt.Draw();

        // Render the plot to the console
        plt.Render();
    }

    private static void MultipleSeries()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var plt = new Plot(80, 22);

        // Generate x values from -3 to 3 with 0.1 step
        var xs = Enumerable.Range(-30, 61).Select(i => i * 0.1).ToArray();

        // Calculate sin(x) values
        var sinYs = xs.Select(Math.Sin).ToArray();

        // Calculate 1/x values, replacing infinity with NaN and limiting the range
        var reciprocalYs = xs.Select(x => {
            if (x == 0 ) return double.NaN;
            var y = 1 / x;
            // Limit the range of y values
            return y is > 3 or < -3 ? double.NaN : y;
        }).ToArray();

        // Add sin(x) series with blue color
        plt.AddSeries(xs, sinYs, new PointPen(SystemPointBrushes.Braille, ConsoleColor.Blue));

        // Add 1/x series with red color
        plt.AddSeries(xs, reciprocalYs, new PointPen(SystemPointBrushes.Braille, ConsoleColor.Red));

        plt.Draw();
        plt.Render();

        Console.WriteLine("Blue: sin(x)");
        Console.WriteLine("Red: 1/x (limited to range [-3, 3])");
    }

    private static void AllSettingsDemonstration()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var plt = new Plot(80, 22);

        // Axis settings
        plt.Axis.IsVisible = true;
        plt.Axis.Pen = new LinePen(SystemLineBrushes.Double, ConsoleColor.Yellow);

        // Grid settings
        plt.Grid.IsVisible = true;
        plt.Grid.Pen = new LinePen(SystemLineBrushes.Dotted, ConsoleColor.DarkGray);

        // Tick settings
        plt.Ticks.IsVisible = true;
        plt.Ticks.Pen = new LinePen(SystemLineBrushes.Thin, ConsoleColor.Cyan);
        plt.Ticks.DesiredXStep = 10;
        plt.Ticks.DesiredYStep = 5;

        // Label settings
        plt.Ticks.Labels.IsVisible = true;
        plt.Ticks.Labels.Color = ConsoleColor.Green;
        plt.Ticks.Labels.AttachToAxis = false;
        plt.Ticks.Labels.Format = "F2";

        // Generate sample data
        var xs = Enumerable.Range(0, 100).Select(i => i * 0.1).ToArray();
        var ys = xs.Select(x => Math.Sin(x) * Math.Exp(-x * 0.1)).ToArray();

        // Add data series with custom point style
        plt.AddSeries(xs, ys, new PointPen(SystemPointBrushes.Star, ConsoleColor.Magenta));

        plt.Draw();
        plt.Render();
    }

    private static void NonUnicodeExample()
    {
        // Note: We're not setting Console.OutputEncoding here

        var plt = new Plot(80, 22);

        // Use non-Unicode line brush
        var nonUnicodeBrush = new LineBrush('|', '-', '+');

        // Axis settings
        plt.Axis.IsVisible = true;
        plt.Axis.Pen = new LinePen(nonUnicodeBrush, ConsoleColor.White);

        // Grid settings
        plt.Grid.IsVisible = true;
        plt.Grid.Pen = new LinePen(nonUnicodeBrush, ConsoleColor.DarkGray);

        // Tick settings
        plt.Ticks.IsVisible = true;
        plt.Ticks.Pen = new LinePen(nonUnicodeBrush, ConsoleColor.White);

        // Generate sample data
        var xs = Enumerable.Range(0, 50).Select(i => i * 0.2).ToArray();
        var ys = xs.Select(Math.Sin).ToArray();

        // Add data series with Star point style (non-Unicode)
        plt.AddSeries(xs, ys, new PointPen(SystemPointBrushes.Star, ConsoleColor.Yellow));

        plt.Draw();
        plt.Render();
    }
}