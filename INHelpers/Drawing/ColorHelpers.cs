using System.Drawing;
using System.Text;

namespace INHelpers.Drawing
{
    public class ColorHelpers
    {
        private static Random _Rnd = new Random((int)(int.MaxValue % DateTime.Now.Ticks));

        /// <summary>
        /// Returns a random fully opaque color
        /// </summary>
        public static Color RandomColor()
        {
            return Color.FromArgb(_Rnd.Next(0, 256), _Rnd.Next(0, 256), _Rnd.Next(0, 256));
        }


    }
}
