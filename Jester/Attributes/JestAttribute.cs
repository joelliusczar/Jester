using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jester
{


    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class JestAttribute: System.Attribute
    {
    }

    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class JestCompareAttribute : System.Attribute
    {
    }


    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class JestClassAttribute : System.Attribute
    {
    }

    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false)]
    public class JestSetupAttribute : Attribute
    {
        #pragma warning disable 0414
        private static bool _isAlreadyUsed = false;
        #pragma warning restore 0414

        public JestSetupAttribute()
        {
        }
    }
}
