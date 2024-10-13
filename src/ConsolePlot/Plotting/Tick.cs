namespace ConsolePlot.Plotting
{
    internal class Tick
    {
        public double Value { get; }
        public string Label { get; }

        public Tick(double value, string label)
        {
            Value = value;
            Label = label;
        }
    }
}