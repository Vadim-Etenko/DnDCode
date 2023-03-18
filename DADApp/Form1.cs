using DADApp.best;
using DADApp.services;
using DADApp.updater;
using System;
using System.Windows.Forms;

namespace DADApp
{
    public partial class Form1 : Form
    {
        private Инвентарь formInvent;
        private Updater formUpdate;
        private Best best;
        public Form1()
        {
            if(Updater.isNewExist())
            {
                MessageBox.Show("Вышло новое обновление", "Обновление", MessageBoxButtons.OK);
            }
            ConfigService.readConf();
            InitializeComponent();
            label1.Text = "v. " + Updater.getCurrentVersion().ToString("N1");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigService.firstRun();
        }

        private void инвентарьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formInvent = new Инвентарь();
            formInvent.FormClosed += formClosed;
            this.Hide();
            formInvent.Show();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formUpdate = new Updater();
            formUpdate.FormClosed += formClosed;
        }

        private void formClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void бестиарийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            best = new Best();
            best.FormClosed += formClosed;
            this.Hide();
            best.Show();
        }
    }
}
