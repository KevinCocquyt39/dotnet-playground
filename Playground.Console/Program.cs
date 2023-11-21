// See https://aka.ms/new-console-template for more information

// using System.Drawing.Imaging;
// using Playground.Console.Ean13;
//
// Console.WriteLine("EAN13 generation...");
//
// // Donck EAN13 code: 5400340169897
// var barcodeText = "54003401698"; // Replace with your EAN-13 barcode data
// var width = 300; // Adjust the width of the barcode image
// var height = 150; // Adjust the height of the barcode image
//
// // var barcodeBitmap = CustomGenerator.Generate(barcodeText, width, height);
// var barcodeBitmap = ZXingGenerator.Generate(barcodeText, width, height);
//
// // Save the barcode bitmap to a file or display it as needed
// barcodeBitmap.Save("_EAN13Barcode.png", ImageFormat.Png);

using Playground.Console.ExcelToRedirectUrls;

Console.WriteLine("RedirectUrls generation...");

Processor.Execute("_RedirectUrls", "https://vanparys.com");
