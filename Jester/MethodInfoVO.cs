using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jester
{
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
