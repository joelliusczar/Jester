using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jester.Exceptions
{
    [Serializable]
    public class ExpectedExceptionException : Exception
    {
        public ExpectedExceptionException()
            : base(string.Format("Was expecting an exception but exception was not thrown."))
        { }

        public ExpectedExceptionException(Type exceptionType)
            : base(string.Format("Was expecting an exception of type:{0} but exception was not thrown.", exceptionType)) { }
    }
}
