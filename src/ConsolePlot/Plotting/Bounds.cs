namespace ConsolePlot.Plotting
{
    internal class Bounds
    {
        public double XMin { get; }
        public double XMax { get; }
        public double YMin { get; }
        public double YMax { get; }

        public Bounds(double xMin, double xMax, double yMin, double yMax)
        {
            XMin = xMin;
            XMax = xMax;
            YMin = yMin;
            YMax = yMax;
        }
    }
}