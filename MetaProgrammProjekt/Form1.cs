using System;
using System.IO;
using System.Windows.Forms;


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
            string filename = Path.GetFileNameWithoutExtension(textBox1.Text);
            JsonToClass JsonHandler = new JsonToClass();
            if (JsonToClass.CheckJson(textBox1.Text))//prüfe ob json gültig ist
            {
                if (Directory.Exists(textBox2.Text)) //prüfe ob ziel ordner existiert
                {

                    KonsolenHandler inputHandler = new KonsolenHandler();
                    inputHandler.setPath(textBox2.Text);
                    inputHandler.WriteToConsole("dotnet new classlib --force -o " + filename); //generiere klassenbibliothek, überschreibe gleichnamige.
                    JsonToClass.ConvertJson(textBox1.Text, textBox2.Text + "\\" + filename);

                    MessageBox.Show("Klassenbibliothek erfolgreich erstellt!");
                }
                else
                {
                    MessageBox.Show("Ungültiger oder leerer Ziel-Pfad!");
                }

            }
            
        }



        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog(); //choosefiledialog
            choofdlog.Filter = "json files (*.json)|*.json";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = choofdlog.FileName;
                string[] arrAllFiles = choofdlog.FileNames; //used when Multiselect = true         
                textBox1.Text = sFileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog(); //folderbrowserdialog
            fbd.Description = "Custom Description";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string sSelectedPath = fbd.SelectedPath;
                textBox2.Text = sSelectedPath;
            }
        }

        
    }
}
