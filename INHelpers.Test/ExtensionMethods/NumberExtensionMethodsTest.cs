using INHelpers.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace INHelpers.Test.ExtensionMethods
{
    public class NumberExtensionMethodsTest
    {

        [Fact] public void ToHumanByteSize_GB()
        {
            var input = (long)Math.Pow(1024, 3);
            var result = input.ToHumanByteSize();
            Assert.Equal("1 GB", result);
        }

        [Fact] public void ToHumanByteSize_MB()
        {
            var input = (long)Math.Pow(1024, 2);
            var result = input.ToHumanByteSize();
            Assert.Equal("1 MB", result);
        }

        [Fact] public void ToHumanByteSize_KB()
        {
            var input = 1024;
            var result = input.ToHumanByteSize();
            Assert.Equal("1 KB", result);
        }

    }
}
