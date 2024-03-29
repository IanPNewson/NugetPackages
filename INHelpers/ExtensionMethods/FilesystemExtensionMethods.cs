﻿using System.IO;

namespace INHelpers.ExtensionMethods
{


    /// <summary>
    /// A colletion of extension methods to help working with the file system
    /// </summary>
    public static class FilesystemExtensionMethods
    {

        /// <summary>
        /// Recurses over all entries in a directory, calling back for each directory and file found
        /// </summary>
        [CantTest("Depends on system IO primarily")]
        public static void Recurse(this DirectoryInfo dir, Action<DirectoryInfo>? dirs = null, Action<FileInfo>? files = null)
        {
            if (dir is null)
            {
                throw new ArgumentNullException(nameof(dir));
            }

            foreach (var file in dir.GetFiles())
            {
                files?.Invoke(file);
            }

            foreach (var sub in dir.GetDirectories())
            {
                dirs?.Invoke(sub);

                Recurse(sub, dirs, files);
            }
        }


        public static readonly string[] ImageExtensions = new string[]
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".gif",
            ".bmp"
        };

        /// <summary>
        /// Returns an array of files which are believed to be images based on their extension.
        /// </summary>
        public static FileInfo[] GetImageFiles(this DirectoryInfo dir, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return dir.GetFiles("*", searchOption)
                .Where(f => ImageExtensions.Any(ext => ext.Equals(f.Extension, StringComparison.OrdinalIgnoreCase)))
                .ToArray();
        }

        /// <summary>
        /// Creates a new directory representing a subdirectory of this directory
        /// </summary>
        /// <param name="dir">The parent directory</param>
        /// <param name="name">Name of the subdirectory</param>
        /// <param name="create">True if the subdirectory should be created if it does not exist. True is default.</param>
        /// <returns>A DirectoryInfo representing the subdirectory</returns>
        public static DirectoryInfo Subdir(this DirectoryInfo dir, string name, bool create = true)
        {
            var subDir = new DirectoryInfo(dir.FullName + "\\" + name);
            if (create) subDir.EnsureExists();
            return subDir;
        }

        /// <summary>
        /// Creates a representation of a file in this directory by name
        /// </summary>
        /// <param name="dir">The parent (containing) directory</param>
        /// <param name="name">Name of the file</param>
        /// <returns>A FileInfo object representing a file within this directory, which may or may not exist</returns>
        public static FileInfo File(this DirectoryInfo dir, string name)
        {
            return new FileInfo(dir.FullName + "\\" + name);
        }

        /// <summary>
        /// Creates a directory if it doesn't exist, including all necessary parent directories
        /// </summary>
        /// <returns>The same DirectoryInfo, except now it's guaranteed to exist</returns>
        public static DirectoryInfo EnsureExists(this DirectoryInfo info)
        {
            if (!info.Exists)
            {
                info.Parent?.EnsureExists();
                info.Create();
            }
            info.Refresh();
            return info;
        }

    }

}
