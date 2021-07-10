using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using INHelpers.ExtensionMethods;

namespace INHelpers.Test.ExtensionMethods
{
    public class ColorExtensionMethodsTest
    {

        [Fact]
        public void ToHex()
        {
            var red = Color.Red;
            Assert.Equal("#FF0000", red.ToHex());
        }

    }
}
