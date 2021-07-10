using INHelpers.Drawing;
using System;
using Xunit;

namespace INHelpers.Test
{
    public class ColorHelpersTest
    {
        [Fact]
        public void RandomColor()
        {
            var result = ColorHelpers.RandomColor();
        }
    }
}
