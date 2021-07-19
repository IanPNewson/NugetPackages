using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace INHelpers.ExtensionMethods
{

    /// <summary>
    /// Extensiom methods for classes or related in the System.Drawing namespace
    /// </summary>
    public static class DrawingExtensionMethods
    {

        public static Point GetCenter(this Rectangle rect)
        {
            return new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
        }

    }
}
