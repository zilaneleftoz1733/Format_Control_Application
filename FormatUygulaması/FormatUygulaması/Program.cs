using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;

class Program
{
    static Dictionary<string, byte[]> fileSignatures = new Dictionary<string, byte[]>
{
{ "JPEG", new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 } },
{ "PNG", new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } },
{ "GIF", new byte[] { 0x47, 0x49, 0x46, 0x38 } },
{ "BMP", new byte[] { 0x42, 0x4D } }
// Diğer dosya türlerinin imzalarını ekleyebilirsiniz.
};
    static string GetFileFormat(string filePath)
    {
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
        {
            byte[] fileHeader = new byte[4];
            fileStream.Read(fileHeader, 0, 4);

            foreach (var signature in fileSignatures)
            {
                if (StructuralComparisons.StructuralEqualityComparer.Equals(fileHeader, signature.Value))
                {
                    return signature.Key;
                }
            }
        }

        return "Bilinmeyen Format";
    }

    static void Main(string[] args)
    {
        string filePath = "C:\\Users\\lzila\\source\\repos\\FormatUygulaması\\FormatUygulaması\\File\\apple.png"; // Kontrol edilecek dosyanın yolu

        string fileFormat = GetFileFormat(filePath);
        Console.WriteLine($"Dosya Formatı: {fileFormat}");
    }
}