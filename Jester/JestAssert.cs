using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jester.Exceptions;

namespace Jester
{
    public class JestAssert
    {
        public static bool IsEqual(string expectedInput, string actualInput)
        {
            if (expectedInput == actualInput) {
                return true;    
            }

            throw JestFailedException.GetInstance(expectedInput, actualInput);
        }

        public static bool IsEqual(int expectedInput, int actualInput)
        {
            if (expectedInput == actualInput)
            {
                return true;
            }

            throw JestFailedException.GetInstance(expectedInput, actualInput);
        }

        public static bool IsEqual(bool expectedInput, bool actualInput)
        {
            if (expectedInput == actualInput)
            {
                return true;
            }

            throw JestFailedException.GetInstance(expectedInput, actualInput);
        }

        public static bool IsEqual(uint expectedInput, uint actualInput)
        {
            if (expectedInput == actualInput)
            {
                return true;
            }

            throw JestFailedException.GetInstance(expectedInput, actualInput);
        }

        public static bool IsEqual(double expectedInput, double actualInput)
        {
            if (expectedInput == actualInput)
            {
                return true;
            }

            throw JestFailedException.GetInstance(expectedInput, actualInput);
        }

        public static bool IsEqual(char expectedInput, char actualInput)
        {
            if (expectedInput == actualInput)
            {
                return true;
            }

            throw JestFailedException.GetInstance(expectedInput, actualInput);
        }

        public static bool IsEqual<T>(T expectedInput, T actualInput)
        {
            if (expectedInput.Equals(actualInput))
            {
                return true;
            }

            throw JestFailedException.GetInstance(expectedInput, actualInput);
        }

        public static bool ExpectException(Type expectedExceptionType, Action methodToExecute)
        {
            try
            {
                methodToExecute();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == expectedExceptionType)
                {
                    return true;
                }
                
            }
            throw JestFailedException.GetInstance(expectedExceptionType);
        }


    }
}
