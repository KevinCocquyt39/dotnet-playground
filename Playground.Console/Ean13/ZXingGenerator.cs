using System.Drawing;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace Playground.Console.Ean13;

public class ZXingGenerator
{
    public static Bitmap Generate(string barcodeText, int width, int height)
    {
        var barcodeWriter = new BarcodeWriter();
        barcodeWriter.Format = BarcodeFormat.EAN_13;

        // Set encoding options (if needed)
        var encodingOptions = new EncodingOptions
        {
            Width = width,
            Height = height,
            Margin = 10 // You can adjust the margin as needed
        };

        barcodeWriter.Options = encodingOptions;

        // Generate the barcode bitmap
        var barcodeBitmap = barcodeWriter.Write(barcodeText);

        return barcodeBitmap;
    }
}
