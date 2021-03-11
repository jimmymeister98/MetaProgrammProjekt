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
            AllocConsole();
            string pwd = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            Directory.SetCurrentDirectory(pwd);
            JsonToClass JsonHandler = new JsonToClass();
            JsonToClass.LoadJson("C:\\Users\\Jimmy Neitzert\\Downloads\\message.json");
            
            //KonsolenHandler inputHandler = new KonsolenHandler();
            //inputHandler.WriteToConsole("dotnet new classlib -o C:\\Users\\Jimmy Neitzert\\source\\repos\\Testlauf"); //kann sein dass backslashes oder sonderzeichen mit backslash escaped werden müssen
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
