using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jester
{
    [System.AttributeUsage(System.AttributeTargets.Method,AllowMultiple=false)]
    public class JestSetupAttribute: Attribute
    {
        private static bool _isAlreadyUsed = false;

        
        public JestSetupAttribute()
        {
        }
    }
}
