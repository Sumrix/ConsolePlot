# ConsolePlot

ConsolePlot is a lightweight .NET library for creating ASCII plots in the console. It provides a simple and flexible way to visualize data directly in your terminal.

## 🚀 Quick Start

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

<img src="images/quickstart_console.png" alt="Simple Plot" width="600">

## 📦 Installation and Usage

Please note that ConsolePlot is a pet project and is not available via NuGet. To use it in your own project, follow these manual installation steps:

1. Clone this repository:
   ```sh
   git clone https://github.com/sumrix/ConsolePlot.git
   ```

2. Copy the `ConsolePlot/src/ConsolePlot` directory into your project.
3. Add a reference to the `ConsolePlot.csproj` file in your project.

## 🧩 Features

- Easy-to-use API for creating plots
- Customizable axis, grid, and tick settings
- Support for multiple data series
- Various line and point styles

## 📊 Examples

You can find various usage examples in the [ConsolePlot.Examples](src/ConsolePlot.Examples) project.

To run the examples, you have two options:

### Option 1: Using Visual Studio
1. Clone this repository by clicking "Open with Visual Studio" on the GitHub page.
2. Open the solution and set `ConsolePlot.Examples` as the startup project.
3. Run the project.

### Option 2: Using the Command Line
```sh
git clone https://github.com/sumrix/ConsolePlot.git
cd ./ConsolePlot/src
dotnet build
dotnet run --project ConsolePlot.Examples
```

## 📚 Documentation

For more detailed information on how to use ConsolePlot, please refer to the [API documentation](docs/API.md).

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE.txt) file for details.