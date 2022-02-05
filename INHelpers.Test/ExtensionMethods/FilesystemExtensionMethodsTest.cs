using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using INHelpers.ExtensionMethods;

namespace INHelpers.Test.ExtensionMethods
{
    public class FilesystemExtensionMethodsTest
    {

        [Fact]
        public void GetImageFiles()
        {
            var dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            dir.GetImageFiles();
        }

        [Fact]
        public void GetImageFiles_AllDirectories()
        {
            var dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            dir.GetImageFiles(SearchOption.AllDirectories);
        }

    }
}
