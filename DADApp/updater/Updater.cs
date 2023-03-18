using DADApp.forms;
using DADApp.services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace DADApp.updater
{
    public partial class Updater : Form
    {
        private static WebClient client = new WebClient();
        private Dictionary<int, String> emprtVar;

        public Updater()
        {
            InitializeComponent();
            double newVersion = getNewVersion();
            double currentVersion = getCurrentVersion();
            Boolean isOld = newVersion > currentVersion;
            Boolean isNew = newVersion == currentVersion;
            Boolean isNewst = newVersion < currentVersion;
            if (isOld)
            {
                var client = new WebClient();
                client.DownloadFile(new Uri(DADConstants.URL_TO_UPDATER), DADConstants.EXE_FILE_UPDATER);
                if (MessageBox.Show("Updated", "Updating status", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    Process.Start(DADConstants.EXE_FILE_UPDATER);
                    Process.GetCurrentProcess().Kill();
                }
            }
            else if (isNew)
            {
                if (MessageBox.Show("You're already updated", "Updating status", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else if (isNewst)
            {
                if (MessageBox.Show("Do not touch any files\n >:(", "Updating status", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    client.DownloadFile(new Uri(DADConstants.URL_TO_UPDATER), DADConstants.EXE_FILE_UPDATER);
                    Process.Start(DADConstants.EXE_FILE_UPDATER);
                    Process.GetCurrentProcess().Kill();
                }
            }
        }

        public void Updater_Load (object sender, EventArgs eventArgs)
        {

        }


        private static double getNewVersion()
        {
            Stream stream = client.OpenRead(DADConstants.URL_TO_VERSTION);
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd();

            return Double.Parse(content);
        }

        public static double getCurrentVersion()
        {
            String version = "0,0";
            StreamReader reader = null;
            try
            {
                if (!File.Exists(DADConstants.TXT_FILE_VERSION))
                {
                    client.DownloadFileAsync(new Uri(DADConstants.URL_TO_VERSTION), DADConstants.TXT_FILE_VERSION);
                }
                reader = new StreamReader(DADConstants.TXT_FILE_VERSION);
                version = reader.ReadToEnd().Trim();
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return Double.Parse(version);

        }

        public static Boolean isNewExist()
        {
            return getNewVersion() > getCurrentVersion();
        }
    }
}
