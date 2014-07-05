using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace Jester.Exceptions
{
    [Serializable]
    public class JestFailedException: Exception
    {

        private JestFailedException(object actualValue, object expectedValue,int lineNum,string callerName)
            :base(string.Format("Test failed at line: {4} in Method: {5} Actual value: {0} with length: {1}, did not match expected value: {2} with length: {3}",
            actualValue.ToString(),actualValue.ToString().Length,expectedValue.ToString(),expectedValue.ToString().Length,lineNum,callerName))
        { 
        }

        private JestFailedException(Type exceptionType, int lineNum, string callerName)
            : base(string.Format("Test failed at line: {1} in method: {2}. Expected exception of type: {0}"
                ,exceptionType,lineNum,callerName)) { }

        private JestFailedException() { }

        public static JestFailedException GetInstance(object actualValue, object expectedValue)
        {
            StackFrame callStack = new StackFrame(2, true);
            return new JestFailedException(actualValue, expectedValue, callStack.GetFileLineNumber(), callStack.GetMethod().Name);
        }

        public static JestFailedException GetInstance(Type exType)
        {
            StackFrame callStack = new StackFrame(2, true);
            return new JestFailedException(exType, callStack.GetFileLineNumber(), callStack.GetMethod().Name);
        }

    }
}
