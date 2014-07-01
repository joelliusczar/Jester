using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Jester
{
    public partial class TesterForm : Form
    {
        Dictionary<string, MethodInfo> methodDictionary;

        public TesterForm()
        {
            InitializeComponent();
            methodDictionary = new Dictionary<string, MethodInfo>();
            Assembly asm = Assembly.GetCallingAssembly();
            var types = asm.GetTypes();
            listView1.MultiSelect = false;
            var allMethods = types.SelectMany(t => t.GetMethods());
            var methodList = allMethods.Where(m => m.GetCustomAttributes(typeof(JestAttribute), false).Length > 0).ToArray();
            foreach (var m in methodList)
            {
                listView1.Items.Add(new ListViewItem(new string[] { m.Name, "Not Yet Run" }));
                methodDictionary.Add(m.Name, m);
            }
        }


        private static void RunTestMethod(MethodInfo m)
        {
            Type t = m.DeclaringType;
            var o = System.Activator.CreateInstance(t);
            m.Invoke(o, null);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != listView1.SelectedItems)
            {
                listView1.SelectedItems[0].ForeColor = Color.Red;
            }
        }

    }
}
