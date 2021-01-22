using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MetaProgrammProjekt.KonsolenHandler;

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
         KonsolenHandler inputHandler = new KonsolenHandler();
         inputHandler.WriteToConsole("code");
        }
    }
}
