using System;
using System.Collections.Generic;
using System.IO;

namespace BulkFileRenamer.Utils
{
    public class FileUtils
    {
        /// <summary>
        /// Checks whether the given path is root
        /// </summary>
        public static bool PathIsRoot(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            return (directory.Parent == null || Path.GetPathRoot(path) == path);
        }

        /// <summary>
        /// Retrieves the files in the given directory paths
        /// </summary>
        /// <param name="paths">The directory paths</param>
        public static IEnumerable<FileInfo[]> GetFiles(List<string> paths)
        {
            foreach (var path in paths)
            {
                yield return new DirectoryInfo(path).GetFiles();
            }
        }

        /// <summary>
        /// Checks that the given name is valid for a file.
        /// </summary>
        /// <param name="name">New file name</param>
        public static bool NewFilenameIsValid(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            else if (name.IndexOfAny(Path.GetInvalidFileNameChars()) > 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates a subdirectory with today's date within the given directory
        /// </summary>
        /// <param name="directoryPath">The directory to create the subdirectory in</param>
        public static DirectoryInfo CreateSubdirectory(string directoryPath)
        {
            var directory = Directory.CreateDirectory(directoryPath);
            return directory.CreateSubdirectory(DateTime.Now.ToString("YYYY-MM-dd_HH-mm-SS"));
        }
    }
}
