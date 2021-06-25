using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INHelpers.ExtensionMethods
{


    /// <summary>
    /// A colletion of extension methods to help working with the file system
    /// </summary>
    public static class FilesystemExtensionMethods
    {

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
