using System;
using System.Configuration;
using System.IO;
using Archiver.Archive;

namespace Archiver
{
    public static class Program
    {
        private static void Main()
        {
            var frequenciesPath = ConfigurationManager.AppSettings.Get("FrequenciesPath");
            
            var firstExampleSourcePath = ConfigurationManager.AppSettings.Get("FirstExampleSourcePath");
            var firstExampleCompressedPath = ConfigurationManager.AppSettings.Get("FirstExampleCompressedPath");
            var firstExampleDecompressedPath = ConfigurationManager.AppSettings.Get("FirstExampleDecompressedPath");
            
            var secondExampleSourcePath = ConfigurationManager.AppSettings.Get("SecondExampleSourcePath");
            var secondExampleCompressedPath = ConfigurationManager.AppSettings.Get("SecondExampleCompressedPath");
            var secondExampleDecompressedPath = ConfigurationManager.AppSettings.Get("SecondExampleDecompressedPath");
            
            try
            {
                HuffmanArchiver archiver = new(frequenciesPath);
                
                CompressAndDecompress(archiver, firstExampleSourcePath, firstExampleCompressedPath, firstExampleDecompressedPath);
                CompressAndDecompress(archiver, secondExampleSourcePath, secondExampleCompressedPath, secondExampleDecompressedPath);
            }
            catch (IOException exception)
            {
                Console.WriteLine($"An error occurred: {exception.Message}");
            }
        }

        private static void CompressAndDecompress(HuffmanArchiver archiver, string sourceFilePath, string compressedFilePath, string decompressedFilePath)
        {   
            var fileInfo = new FileInfo(sourceFilePath);
            Console.WriteLine($"First example file size is {fileInfo.Length} bytes");
            archiver.CompressFile(sourceFilePath, compressedFilePath);
                
            fileInfo = new FileInfo(compressedFilePath);
            Console.WriteLine($"First example compressed file size is {fileInfo.Length} bytes");
            archiver.DecompressFile(compressedFilePath, decompressedFilePath);
                
            fileInfo = new FileInfo(decompressedFilePath);
            Console.WriteLine($"First example decompressed file size is {fileInfo.Length} bytes\n");
        }
    }
}