using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace PhotoSizer
{
    class Program
    {
        const string PhotoLibraryOriginals = "/Users/williamdenton/Pictures/iCloud Photo Library.photoslibrary/originals";
        static void Main(string[] args)
        {
            var movies = new DirectoryInfo(PhotoLibraryOriginals)
                .GetDirectories()
                .SelectMany(s => s.GetFiles("*.mov"))
                .ToList();

            if (!Directory.Exists("media"))
            {
                Directory.CreateDirectory("media");
            }

            long inputBytes = 0;
            long outputBytes = 0;

            foreach (var sourceFile in (movies.OrderByDescending(f => f.Length).Take(100)))
            {
                var destFile = $"media/{Path.GetFileNameWithoutExtension(sourceFile.Name)}-compressed.mp4";

                var psEncode = new ProcessStartInfo
                {
                    FileName = "./HandBrakeCLI",
                    Arguments = $"--preset \"Fast 720p30\" --input \"{sourceFile.FullName}\" --output \"{destFile}\""
                };
                using var encode = Process.Start(psEncode);
                encode.WaitForExit();

                var psExif = new ProcessStartInfo
                {
                    FileName = "exiftool",
                    Arguments = $"-TagsFromFile \"{sourceFile.FullName}\" -all:all \"{destFile}\" -overwrite_original"
                };
                using var exif = Process.Start(psExif);

                exif.WaitForExit();

                var outputFile = new FileInfo(destFile);

                inputBytes += sourceFile.Length;
                outputBytes += outputFile.Length;

                Console.WriteLine($"Input {inputBytes / 1024 / 1024:0000.0}mb. Output {outputBytes / 1024 / 1024:0000.0}mb. Saved {(inputBytes - outputBytes) / 1024 / 1024:0000.0}mb");

            }

            Console.WriteLine($"Total=======================");
            Console.WriteLine($"Input  {inputBytes / 1024 / 1024:0000.0}mb");
            Console.WriteLine($"Output {outputBytes / 1024 / 1024:0000.0}mb");
            Console.WriteLine($"Saved  {(inputBytes - outputBytes) / 1024 / 1024:0000.0}mb");

        }
    }
}
