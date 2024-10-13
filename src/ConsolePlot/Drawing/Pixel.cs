using System;

namespace ConsolePlot.Drawing
{
    /// <summary>
    /// Represents a pixel in the console image.
    /// </summary>
    public readonly struct Pixel
    {
        /// <summary>
        /// Gets the character of the pixel.
        /// </summary>
        public char Character { get; }

        /// <summary>
        /// Gets the foreground color of the pixel.
        /// </summary>
        public ConsoleColor ForegroundColor { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pixel"/> struct.
        /// </summary>
        /// <param name="character">The character of the pixel.</param>
        /// <param name="foregroundColor">The foreground color of the pixel.</param>
        public Pixel(char character, ConsoleColor foregroundColor)
        {
            Character = character;
            ForegroundColor = foregroundColor;
        }
    }
}