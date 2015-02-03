using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jester
{
    
    //the sucess or failure of a test are stored in objects of this class.
    //A GUI can use the values stored in these properties to display to the user
    //if a test was successful or not.
    public class MethodTestInfo
    {
        public string MethodName { get; set; }
        public MethodInfo Method { get; set; }
        public string ErrorInfo { get; set; }
        public System.Windows.Forms.ListViewItem.ListViewSubItem ListViewSubItem { get; set; }

        public void SetTestStatus(JestResultsEnum testResults)
        {
            this.ListViewSubItem.ForeColor = testResults.color;
            this.ListViewSubItem.Text = testResults.display;
        }


    }
}
