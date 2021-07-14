using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BitBox
{
    public partial class PluginsWindow : Form
    {
        Parser parser;
        string pluginsDirectory;
        public PluginsWindow(Parser parser)
        {
            this.parser = parser;
            pluginsDirectory = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.Length-10) + "\\Plugins";
            parser.UpdateExistingPlugins(pluginsDirectory);
            InitializeComponent();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {

        }

        private void Plugins_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string plugin;
            if (textBox1.Text.IndexOf(" ") == -1)
                plugin = textBox1.Text;
            else
                plugin = textBox1.Text.Substring(0, textBox1.Text.IndexOf(" "));
            List<string> containingPlugin = parser.SearchInExistingPlugin(plugin);
            if (containingPlugin.Count > 0)
            {
                listBox2.Visible = true;
                listBox2.Items.Clear();
                foreach (string pluginName in containingPlugin)
                    listBox2.Items.Add(pluginName);
            }
            else
                listBox2.Visible = false;
        }

        private void TextBox1_LostFocus(object sender, EventArgs e)
        {
            listBox2.Visible = false;
        }

        private void buttonScanForPlugins_Click(object sender, EventArgs e)
        {
            parser.UpdateExistingPlugins(pluginsDirectory);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string plugin;
                string parameters;
                if (textBox1.Text.IndexOf(" ") == -1)
                {
                    plugin = textBox1.Text;
                    parameters = "";
                }
                else
                {
                    plugin = textBox1.Text.Substring(0, textBox1.Text.IndexOf(" "));
                    parameters = textBox1.Text.Substring(textBox1.Text.IndexOf(" "),textBox1.Text.Length - textBox1.Text.IndexOf(" "));
                }
                List<string> containingPlugin = parser.SearchInExistingPlugin(plugin);
                if (containingPlugin.Count > 0)
                {
                    parser.AddPlugin((Plugin)Activator.CreateInstance(null, "BitBox.Plugins." + containingPlugin[0]).Unwrap());
                    listBox1.Items.Add(containingPlugin[0] + " " + parameters);
                }
            }
        }
    }
}
