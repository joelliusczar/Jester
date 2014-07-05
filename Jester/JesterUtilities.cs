using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jester
{
    public static class JesterUtilities
    {
        public static void RunTestMethod(MethodInfo m)
        {
            Type t = m.DeclaringType;
            var o = System.Activator.CreateInstance(t);
            var setups = t.GetMethods().Where(m2 => m2.GetCustomAttributes(typeof(JestSetupAttribute), false).Length > 0 
                && m2.IsMethodVoidOrNonValue());
            foreach (var s in setups)
            {
                try
                {
                    s.Invoke(o, null);
                }
                catch (TargetInvocationException ex)
                {
                    throw ex.InnerException;
                }
            }
            m.Invoke(o, null);
        }

        public static T Create<T>(params object[] args) where T : class
        {
            try
            {
                Type[] paramTypes = GetArrayOfTypes(args);
                ConstructorInfo constructor = typeof(T).GetConstructor(paramTypes);
                T t = (T)constructor.Invoke(args);
                return t;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public static Type[] GetArrayOfTypes(object[] args)
        {
            Type[] t = new Type[args.Length];
            for (int i = 0; i < t.Length; i++)
            {
                t[i] = args[i].GetType();
            }
            return t;
        }

        public static string GetErrorInfo(Exception ex)
        {
            return string.Format("{0}\r\n\r\n{1}", ex.Message, ex.StackTrace);
        }

        public static string SelectTest(MethodInfo method, MethodTestInfo testInfo)
        {
            bool testPassed = true;
            try
            {

                JesterUtilities.RunTestMethod(method);
            }
            catch (TargetInvocationException ex)
            {
                testPassed = false;
                string errorInfo = JesterUtilities.GetErrorInfo(ex.InnerException);
                testInfo.ErrorInfo = errorInfo;
                testInfo.SetTestStatus(JestResultsEnum.FAILED);
                return errorInfo;

            }

            if (testPassed)
            {
                string errorInfo = "Test Passed!";
                testInfo.ErrorInfo = errorInfo;
                testInfo.SetTestStatus(JestResultsEnum.PASSED);
                return errorInfo;
            }
            else
            {
                return null;
            }
        }

        public static bool IsMethodVoidOrNonValue(this MethodInfo method)
        {
            return method.ReturnType == typeof(void) && method.GetParameters().Length == 0;
        }

    }
}
