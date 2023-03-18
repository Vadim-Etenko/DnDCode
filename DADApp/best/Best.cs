using DADApp.best.entity;
using DADApp.forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DADApp.best
{
    public partial class Best : Form
    {
        public Best()
        {
            InitializeComponent();
        }

        private void Best_Load(object sendeіr, EventArgs e)
        {
            KDLabel.Parent = KDImage;
            MobsTreeView.BeginUpdate();

            Dictionary<int, ArrayList> allMobsWithLvls = XMLService.ParseXMLToBestDTO();
            MobsTreeView.Nodes.Add("Все существа");
            for (int i = 1; i <= 20; i++)
            {
                MobsTreeView.Nodes[0].Nodes.Add(i + " LVL");
                ArrayList MOBSbYlVL = allMobsWithLvls[i];
                foreach(MobDTO mob in MOBSbYlVL) {
                    MobsTreeView.Nodes[0].Nodes[i-1].Nodes.Add(mob.name);
                }
            }
            MobsTreeView.EndUpdate();            
        }

        private void eToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void labelName_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
