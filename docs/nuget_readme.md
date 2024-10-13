## About

ConsolePlot is a .NET library for creating ASCII plots in the console.

More documenation is available at the [API documentation](https://github.com/Sumrix/ConsolePlot/blob/master/docs/API.md).

## Quick Start

Here's a simple example to get you started:

```csharp
using ConsolePlot;

Console.OutputEncoding = System.Text.Encoding.UTF8;

double[] xs = [1, 2, 3, 4, 5];
double[] ys = [1, 4, 9, 16, 25];

Plot plt = new Plot(80, 22);
plt.AddSeries(xs, ys);
plt.Draw();
plt.Render();
```

This will create a simple plot in your console:

![Simple Plot](https://raw.githubusercontent.com/Sumrix/ConsolePlot/refs/heads/master/images/nuget_quickstart_console.png)

More examples in the [ConsolePlot.Examples](https://github.com/Sumrix/ConsolePlot/tree/master/src/ConsolePlot.Examples) project.

## Features

- Customizable axis, grid, ticks and chart lines.
- Support for multiple data series.
- Adaptive scaling: automatically adjusts the plot to fit the console window, ensuring round axis labels, optimal tick placement, and alignment with console cells.

## Feedback

Bug reports and contributions are welcome at the [GitHub repository](https://github.com/Sumrix/ConsolePlot/).