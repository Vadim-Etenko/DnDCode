using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DADApp.inventory.tabTextBox
{
    public partial class TabTextBoxForm : Form
    {

        public String newName;
        public Boolean isOk = false;

        public TabTextBoxForm(String oldName)
        {
            InitializeComponent();
            newName = oldName;
            textBox1.Text = oldName;
        }

        private void rename_Click(object sender, EventArgs e)
        {
            newName = textBox1.Text;
            isOk = true;
            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
