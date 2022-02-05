using System;
using System.Windows.Forms;

namespace WinFormsDsl
{
    public static class Controls
    {

        public static T Ctrl<T>()
            where T : UserControl
        {

        }

    }
}
