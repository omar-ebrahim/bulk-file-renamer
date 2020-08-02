using System.Collections.Generic;
using System.IO;

namespace BulkFileRenamer.Utils
{
    public class FileUtils
    {
        public FileUtils()
        {
        }

        public static bool PathIsRoot(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            return (directory.Parent == null || Path.GetPathRoot(path) == path);
        }

        public static IEnumerable<FileInfo[]> GetFiles(List<string> paths)
        {
            foreach (var path in paths)
            {
                yield return new DirectoryInfo(path).GetFiles();
            }
        }

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
    }
}
