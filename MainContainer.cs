using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitBox
{
    public partial class MainContainer : Form
    {
        Parser parser;
        DobleSizePropertyWindow widthChange;
        DobleSizePropertyWindow gridChange;

        public MainContainer()
        {
            InitializeComponent();
            parser = new Parser(new Parser.GetParsedData(bitView1.SetData));
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                parser.Data = Utils.OpenFile(openFileDialog1.FileName);
                parser.Update();
                bitView1.Invalidate();
            }
        }

        private void MainContainer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'w')
            {
                if (widthChange == null || widthChange.IsDisposed)
                {
                    widthChange = new DobleSizePropertyWindow("Width", bitView1.BitsWidth1, bitView1.BitsWidth2, new DobleSizePropertyWindow.ChangeValue(bitView1.ChangeBitWidth), new DobleSizePropertyWindow.XLableClicked(bitView1.SetMaxWidth));
                    widthChange.Show();
                }
                else
                    widthChange.BringToFront();
            }
            if (e.KeyChar == 'g')
            {
                if (gridChange == null || gridChange.IsDisposed)
                {
                    gridChange = new DobleSizePropertyWindow("Grid", bitView1.GridSizeX, bitView1.GridSizeY, new DobleSizePropertyWindow.ChangeValue(bitView1.ChangeGridSize), new DobleSizePropertyWindow.XLableClicked(bitView1.SetDefaultGridSize));
                    gridChange.Show();
                }
                else
                    gridChange.BringToFront();
            }
            if (e.KeyChar == 'p')
            {
                PluginsWindow plugins = new PluginsWindow(parser);
                plugins.Show();
            }
        }
    }
}
