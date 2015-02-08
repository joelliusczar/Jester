using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jester
{
    public static class JesterUtilities
    {
        /// <summary>
        /// Sets things up to run a method and then calls the method
        /// </summary>
        /// <param name="m">The method that we want to call</param>
        public static void RunTestMethod(MethodInfo m)
        {
            Object o = RunSetups(m);
            m.Invoke(o, null);
        }

        /// <summary>
        /// Don't remember. I think this may be deprecated code
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T RunCompareMethod<T>(MethodInfo m,params Object[] args)
        {
            Object o = RunSetups(m);
            return (T)m.Invoke(o, args);
        }

        /// <summary>
        /// Instantiates an object and returns that object to be used to call an object method
        /// </summary>
        /// <param name="m">We want to call the method m but first we need an object of the class that m is a member of </param>
        /// <returns>the object of the class that m is a member of</returns>
        public static object RunSetups(MethodInfo m)
        {
            Type t = m.DeclaringType;
            Object o = System.Activator.CreateInstance(t);
            var setups = t.GetMethods().Where(m2 => m2.GetCustomAttributes(typeof(JestSetupAttribute), false).Length > 0
                && m2.IsMethodVoidOrNonValue());
            foreach (MethodInfo s in setups)
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
            return o;
        }

        /// <summary>
        /// Not sure. Don't remember. This may be used by the user.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
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

        /// <summary>
        /// We want the type for each of these objects that we are sending this method
        /// </summary>
        /// <param name="args">We want the type for each of these objects</param>
        /// <returns>We return an array that has a corresponding type for each of the objects passed to the method</returns>
        public static Type[] GetArrayOfTypes(object[] args)
        {
            Type[] t = new Type[args.Length];
            for (int i = 0; i < t.Length; i++)
            {
                t[i] = args[i].GetType();
            }
            return t;
        }

        /// <summary>
        /// Formats the error information for a failed test
        /// </summary>
        /// <param name="ex">the failure exception</param>
        /// <returns>The formatted information of the error</returns>
        public static string GetErrorInfo(Exception ex)
        {
            return string.Format("{0}\r\n\r\n{1}", ex.Message, ex.StackTrace);
        }

        /// <summary>
        /// This should be called by the gui to run a test
        /// I don't like how I did this method. I don't like using one of the parameters as an out parameter
        /// </summary>
        /// <param name="method">This is the method that we want to call. It needs to be a method that has our Jest Attribute</param>
        /// <param name="testInfo">This will be updated with the success or failure information of the unit test</param>
        /// <returns>This will either be "Test Passed" or the formatted exception info</returns>
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

        /// <summary>
        /// This is an extension method on MethodInfo
        /// This determines if a method is void that it accepts no arguments
        /// </summary>
        /// <param name="method">Since this is an extension method, I don't think you actually have to pass this anything.</param>
        /// <returns></returns>
        public static bool IsMethodVoidOrNonValue(this MethodInfo method)
        {
            return method.ReturnType == typeof(void) && method.GetParameters().Length == 0;
        }

        /// <summary>
        ///Basically gets the list of our unit tests
        /// </summary>
        /// <param name="asm">the assembly that has all of our unit tests</param>
        /// <returns>the list of unit tests</returns>
        public static MethodInfo[] GetMethodList(Assembly asm)
        {

            Type[] types = asm.GetTypes();
            IEnumerable<MethodInfo> allMethods = types.SelectMany(t => t.GetMethods());
            MethodInfo[] methodList = allMethods.Where(m => m.GetCustomAttributes(typeof(JestAttribute), false).Length > 0
                && m.IsMethodVoidOrNonValue() ).ToArray();
            return methodList;
        }

        /// <summary>
        /// This is deprecated but you're welcome to figure out what it does and use it. Personally I don't remember what it does.
        /// </summary>
        /// <param name="asm"></param>
        /// <returns></returns>
        public static MethodInfo[] GetMethodListWithArguments(Assembly asm)
        {
            Type[] types = asm.GetTypes();
            IEnumerable<MethodInfo> allMethods = types.SelectMany(t => t.GetMethods());
            MethodInfo[] methodList = allMethods.Where(m => m.GetCustomAttributes(typeof(JestCompareAttribute), false).Length > 0
                && m.GetParameters().Length == 1 
                && !m.GetParameters().Any(p => p.ParameterType == typeof(Object)) ).ToArray();
            return methodList;
        }



    }
}
