using System;

namespace ConsolePlot.Drawing.Tools
{
    /// <summary>
    /// Represents a brush that uses Braille characters to achieve higher resolution within a single console character.
    /// </summary>
    public class BrailleBrush : IPointBrush
    {
        private const char BrailleMinChar = '\u2800';
        private const char BrailleMaxChar = '\u28FF';

        /// <inheritdoc/>
        public int HorizontalResolution => 2;
        
        /// <inheritdoc/>
        public int VerticalResolution => 4;
        
        /// <inheritdoc/>
        public char RenderPoint(char currentChar, int x, int y)
        {
            if (!IsBrailleCharacter(currentChar))
                currentChar = BrailleMinChar;

            // Calculate the offset for the new dot
            int dotOffset = GetDotOffset(x, y);

            // Add the new dot to the existing character
            return (char)(currentChar | 1 << dotOffset);
        }

        private static bool IsBrailleCharacter(char c) => c >= BrailleMinChar && c <= BrailleMaxChar;

        private int GetDotOffset(int x, int y)
        {
            // Dot order in Braille characters:
            // 6 7
            // 2 5
            // 1 4
            // 0 3
            switch (x)
            {
                case 0:
                    switch (y)
                    {
                        case 0: return 6;
                        case 1: return 2;
                        case 2: return 1;
                        case 3: return 0;
                        default:
                            throw new ArgumentOutOfRangeException($"Invalid Braille cell coordinates: ({x}, {y})");
                    }
                case 1:
                    switch (y)
                    {
                        case 0: return 7;
                        case 1: return 5;
                        case 2: return 4;
                        case 3: return 3;
                        default:
                            throw new ArgumentOutOfRangeException($"Invalid Braille cell coordinates: ({x}, {y})");
                    }
                default:
                    throw new ArgumentOutOfRangeException($"Invalid Braille cell coordinates: ({x}, {y})");
            }
        }
    }
}