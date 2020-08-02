using BulkFileRenamer.Utils;
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
            List<FileInfo[]> filesToRename;

            Console.WriteLine("Bulk file renamer");
            Console.WriteLine("-------------------------");

            while (!doneEnteringPaths)
            {
                var message = $"{(filePaths.Any() ? "Paste another directory" : "Enter a directory")} and hit Enter, or type Y or y to confirm, or X or x to exit and hit Enter.";
                Console.WriteLine(message);
                var path = Console.ReadLine().Trim().TrimEnd('\\').TrimEnd('/');
                if (path.ToUpper() == "X")
                {
                    return; // leave the application
                }
                else if (path.ToUpper() == "Y")
                {
                    if (!filePaths.Any())
                    {
                        Console.WriteLine("You have not entered any paths.");
                        continue;
                    }
                    doneEnteringPaths = true;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(path))
                    {
                        Console.WriteLine("You have not entered a path");
                        continue;
                    }
                    else if (path.IndexOfAny(Path.GetInvalidPathChars()) > 0 || !Directory.Exists(path))
                    {
                        // Path.GetInvalidPathChars() does not return any characters
                        // on MacOS but will in Windows so check both
                        Console.WriteLine("Invalid directory path");
                        continue;
                    }
                    else if (FileUtils.PathIsRoot(path))
                    {
                        Console.WriteLine("This path is root, cannot use root drive");
                        continue;
                    }
                    else if (filePaths.Any(x => x == path))
                    {

                        Console.WriteLine("You have already added this path");
                        continue;
                    }
                    else
                    {
                        filePaths.Add(path);
                    }
                }
            }

            Console.WriteLine("Reading in files...");
            filesToRename = FileUtils.GetFiles(filePaths).ToList();

            Console.WriteLine($"Files read in. {filesToRename.Sum(x => x.Count())}");
            Console.WriteLine("Specify a directory to output the files to. This must be different to the retrieval directories");
            Console.WriteLine("The specified output directory does not need to exist. This will be created automatically.");

            bool validOutputDirectory = false;
            string outputDirectory = "";
            while (!validOutputDirectory)
            {
                outputDirectory = Console.ReadLine();
                validOutputDirectory = !filePaths.Any(x => x == outputDirectory);
            }

            Console.WriteLine($"The output directory is {outputDirectory}");
            Console.WriteLine("Enter a name to rename all the files to");
            Console.WriteLine("Every file will be renamed to [currentIndex]_[selectedFileName].extension");

            bool isValidOutputName = false;
            string newFileName = "";
            while (!isValidOutputName)
            {
                newFileName = Console.ReadLine();
                if (!FileUtils.NewFilenameIsValid(newFileName.Trim()))
                {
                    Console.WriteLine($"Filename is not valid. Enter a valid filename. This cannot contain the following characters: {Path.GetInvalidFileNameChars()}");
                }
                else
                {
                    isValidOutputName = true;
                }
            }

            Console.WriteLine("Press Enter to start the bulk rename");
            Console.ReadLine();

            DirectoryInfo createdDirectory = FileUtils.CreateSubdirectory(outputDirectory);
            int index = 0;

            foreach (FileInfo[] fileList in filesToRename)
            {
                foreach (FileInfo file in fileList)
                {
                    File.Copy(file.FullName, Path.Combine(createdDirectory.FullName, $"{newFileName}_{index}{file.Extension}"));
                    Console.WriteLine($"Copied {file.FullName} to {createdDirectory.FullName} as {newFileName}_{index}{file.Extension}");
                    index++;
                }
            }

            Console.WriteLine("Done! Press any key to exit the application.");
            Console.Read();
        }
    }
}
