using OfficeOpenXml;

namespace Playground.Console.ExcelToRedirectUrls;

public class Processor
{
    public static void Execute(string fileName, string fromDomain)
    {
        var excelFilePath = $"{fileName}.xlsx";
        var textFilePath = $"{fileName}_output.txt";

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage(new FileInfo(excelFilePath));

        using var writer = new StreamWriter(textFilePath); // Create or overwrite the text file and write content to it.

        var worksheet = package.Workbook.Worksheets[0]; // Assuming you want to read the first worksheet

        var rowCount = worksheet.Dimension.Rows;
        var colCount = worksheet.Dimension.Columns;

        for (var row = 1; row <= rowCount; row++)
        {
            // for (var col = 1; col <= colCount; col++)
            // {
            //     var cellValue = worksheet.Cells[row, col].Value;
            //     writer.WriteLine($"Row {row}, Column {col}: {cellValue}");
            // }

            var fromUrl = worksheet.Cells[row, 2].Value;
            var toUrl = worksheet.Cells[row, 3].Value;

            if (fromUrl is null || toUrl is null)
            {
                continue;
            }

            fromDomain = fromDomain.EndsWith("/") ? fromDomain : $"{fromDomain}/";

            WriteToFile(writer, fromDomain, fromUrl.ToString(), toUrl.ToString());
        }
    }

    private static void WriteToFile(StreamWriter writer, string fromDomain, string? fromUrl, string? toUrl)
    {
        var fromUrlWithoutDomain = fromUrl?.Replace(fromDomain, "");
        var rule = $""".AddRedirect("^{fromUrlWithoutDomain}", "{toUrl}")""";
        writer.WriteLine(rule);
    }
}
