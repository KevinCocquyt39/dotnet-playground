using System.Drawing;

namespace Playground.Console.Ean13;

public class CustomGenerator
{
    public static Bitmap Generate(string barcodeText, int width, int height)
    {
        if (barcodeText.Length != 12)
        {
            throw new ArgumentException("EAN-13 barcode text must be 12 digits long.");
        }

        // Create a bitmap with the specified width and height
        var barcodeBitmap = new Bitmap(width, height);

        // Create a graphics object to draw on the bitmap
        using (var g = Graphics.FromImage(barcodeBitmap))
        {
            // Clear the background
            g.FillRectangle(Brushes.White, 0, 0, width, height);

            // Define the EAN-13 barcode structure
            var encodingPatterns = GetEAN13EncodingPatterns(barcodeText);

            // Draw the barcode
            var x = 0;
            foreach (var c in barcodeText)
            {
                int barWidth = (c == '1') ? 2 : 1; // 1 represents a narrow bar, 2 represents a wide bar
                g.FillRectangle(Brushes.Black, x, 0, barWidth, height);
                x += barWidth;
            }

            // Add the guard bars (left and right)
            g.FillRectangle(Brushes.Black, 0, 0, 1, height);
            g.FillRectangle(Brushes.Black, x, 0, 1, height);
        }

        return barcodeBitmap;
    }

    // Define the encoding patterns for each digit in EAN-13
    private static string[] GetEAN13EncodingPatterns(string barcodeText)
    {
        // Define encoding patterns for digits 0-9 (left-hand side)
        string[] patterns = {
            "0001101", "0011001", "0010011", "0111101", "0100011",
            "0110001", "0101111", "0111011", "0110111", "0001011"
        };

        // Initialize the encoded patterns for the first digit (left guard)
        string[] encodedPatterns = { "101" };

        // Encode the first set of six digits (left-hand side)
        for (var i = 0; i < 6; i++)
        {
            var digit = int.Parse(barcodeText[i].ToString());
            encodedPatterns[0] += patterns[digit];
        }

        // Add the center guard pattern
        encodedPatterns[0] += "01010";

        // Encode the second set of six digits (right-hand side)
        for (var i = 6; i < 12; i++)
        {
            var digit = int.Parse(barcodeText[i].ToString());
            encodedPatterns[0] += patterns[digit];
        }

        // Add the right guard pattern
        encodedPatterns[0] += "101";

        return encodedPatterns;
    }
}
