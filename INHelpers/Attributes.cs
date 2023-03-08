using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INHelpers
{

    /// <summary>
    /// Used to indicate why a method doesn't have a test method
    /// </summary>
    public class CantTestAttribute : Attribute
    {

        public CantTestAttribute(string reason) { }

    }
}
