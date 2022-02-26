using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BoolFormat = INHelpers.InterpolatedStrings.BoolFormatInterpolatedFormatStringHandler;

namespace INHelpers.Test.InterpolatedStrings
{
    public class BoolFormatInterpolatedFormatStringHandlerTest
    {

        [Fact] public void TrueStringTest()
        {
            BoolFormat result = $"{true:yes:no}";
            Assert.Equal("yes", (string)result);
        }

        [Fact] public void FalseStringTest()
        {
            BoolFormat result = $"{false:yes:no}";
            Assert.Equal("no", (string)result);
        }

        [Fact] public void InvalidFormatTest_MissingCCondition()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                BoolFormat result = $"{true:yes}";
            });
        }

        [Fact] public void InvalidFormatTest_UnsupportedCondition()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                BoolFormat result = $"{true:yes:no:maybe}";
            });
        }


        [Fact] public void OtherTypesTest()
        {
            var d = 1.39d;
            var date = DateTime.Now;
            var expected = $"{date:HH:mm}, {d:0.0}";
            BoolFormat result = $"{date:HH:mm}, {d:0.0}";
            Assert.Equal(expected, result);
        }

    }
}
