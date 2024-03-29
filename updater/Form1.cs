﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace updater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string downloadPath = Path.GetTempFileName();
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = label1.Text.Replace("%app", Program.appName);
            new Thread(BeginUpdate).Start();
        }

        void BeginUpdate()
        {
            for(int i = 3; i > 0; i--)
            {
                SetStatus("updating in " + i + "...");
                Thread.Sleep(1000);
            }
            WebClient wc = new WebClient();
            SetStatus("downloading...");
            wc.DownloadFile(Program.url, downloadPath);
            SetStatus("deleting " + Path.GetFileName(Program.path) + "...");
            File.Delete(Program.path);
            SetStatus("replacing " + Path.GetFileName(Program.path) + "...");
            File.Move(downloadPath, Program.path);
            SetStatus("done, starting " + Program.appName + "...");
            Process.Start(Program.path);

            for (int i = 3; i > 0; i--)
            {
                SetStatus("closing in " + i + "...");
                Thread.Sleep(1000);
            }

            Application.Exit();
        }

        void SetStatus(string text)
        {
            Invoke(new MethodInvoker(() =>
            {
                label2.Text = text;
            }));
        }
    }
}
