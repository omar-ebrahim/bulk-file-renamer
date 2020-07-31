using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulkFileRenamer
{
    class Program
    {
        static void Main(string[] args)
        {
            var doneEnteringPaths = false;
            List<string> filePaths = new List<string>();

            Console.WriteLine("Bulk file renamer");
            Console.WriteLine("-------------------------");

            while (!doneEnteringPaths)
            {
                var message = $"{(filePaths.Any() ? "Paste another directory" : "Enter a directory")} and hit Enter, or type Y or y to confirm";
                Console.WriteLine(message);
                var path = Console.ReadLine();
                if (path.ToUpper() == "Y")
                {
                    doneEnteringPaths = true;
                }
                else
                {
                    // Validate path
                    if (!Directory.Exists(path))
                    {
                        Console.WriteLine("Invalid directory path");
                        continue;
                    }
                    else
                    {
                        filePaths.Add(path);
                    }
                }
            }

            Console.WriteLine("Reading in files");
            foreach (var path in filePaths)
            {
                var files = new DirectoryInfo(path).GetFiles();
            }
            
        }
    }
}
