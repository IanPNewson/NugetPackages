using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace INHelpers.Json.Net
{
    public static class ExtensionMethods
    {

        public static object Identity(this JObject obj)
        {
            return obj;
        }

    }
}
