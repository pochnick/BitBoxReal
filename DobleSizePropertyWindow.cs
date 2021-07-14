using System;
using System.Windows.Forms;

namespace BitBox
{
    public partial class DobleSizePropertyWindow : Form
    {
        #region parameters

        public delegate void ChangeValue(uint leftValue, uint rightValue);
        public delegate void XLableClicked();

        private ChangeValue changeValueHandler;
        public XLableClicked xLableClickedHandler;

        #endregion

        public DobleSizePropertyWindow(string title, uint leftValue, uint rightValue, ChangeValue changeValueHandler, XLableClicked xLableClickedHandler)
        {
            InitializeComponent();
            Text = title;
            this.changeValueHandler = changeValueHandler;
            this.xLableClickedHandler = xLableClickedHandler;
            textBox1.Text = Convert.ToString(leftValue);
            textBox2.Text = Convert.ToString(rightValue);
        }

        #region event handle methods

        private void textBoxs_TextChanged(object sender, EventArgs e)
        {
            changeValueHandler(UIntFromValue(textBox1.Text), UIntFromValue(textBox2.Text));
        }

        private void label1_Click(object sender, EventArgs e)
        {
            xLableClickedHandler();
        }

        private void textBoxs_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBoxValidKeyInput(sender, e);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            textBoxChangeValeByKey(e, textBox1);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            textBoxChangeValeByKey(e, textBox2);
        }

        private void textBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            textBoxChangeValueByMouseWheel(e, textBox1);
        }

        private void textBox2_MouseWheel(object sender, MouseEventArgs e)
        {
            textBoxChangeValueByMouseWheel(e, textBox2);
        }

        #endregion

        #region logic methods
        private void textBoxChangeValeByKey(KeyEventArgs e, TextBox textBox)
        {
            {
                if (e.KeyCode == Keys.Up)
                {
                    textBox.Text = Convert.ToString(UIntFromValue(textBox.Text) + 1);
                }
                if (e.KeyCode == Keys.Down && UIntFromValue(textBox.Text) > 0)
                {
                    textBox.Text = Convert.ToString(UIntFromValue(textBox.Text) - 1);
                }
            }
        }

        private void textBoxChangeValueByMouseWheel(MouseEventArgs e, TextBox textBox)
        {
            if (e.Delta / 10 + UIntFromValue(textBox.Text) > 0)
                textBox.Text = Convert.ToString(UIntFromValue(textBox.Text) + e.Delta / 10);
            else
                textBox.Text = "0";

        }

        private void textBoxValidKeyInput(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }
        private uint UIntFromValue(string value)
        {
            uint uintValue;
            if (value == "")
                uintValue = 0;
            else
                uintValue = Convert.ToUInt32(value);
            return uintValue;
        }
        #endregion

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            xLableClickedHandler();
        }
    }
}
