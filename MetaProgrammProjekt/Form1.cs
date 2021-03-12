using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MetaProgrammProjekt.JsonToClass;
using static MetaProgrammProjekt.KonsolenHandler;
using System.Runtime.InteropServices;
using System.Threading;


namespace MetaProgrammProjekt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            //var jsonText string = File.ReadAllText(@"./")
            //AllocConsole();
            string filename = Path.GetFileNameWithoutExtension(textBox1.Text);
            string pwd = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            Directory.SetCurrentDirectory(pwd);
            KonsolenHandler inputHandler = new KonsolenHandler();
            inputHandler.setPath(textBox2.Text);
            
            inputHandler.WriteToConsole("dotnet new classlib --force -o "+filename); //kann sein dass backslashes oder sonderzeichen mit backslash escaped werden müssen
            Thread.Sleep(100);
            JsonToClass JsonHandler = new JsonToClass();
            JsonToClass.ConvertJson(textBox1.Text,textBox2.Text+"\\"+filename);
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

       

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = choofdlog.FileName;
                string[] arrAllFiles = choofdlog.FileNames; //used when Multiselect = true         
                textBox1.Text = sFileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Custom Description";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string sSelectedPath = fbd.SelectedPath;
                textBox2.Text = sSelectedPath;
            }
        }
    }
}
