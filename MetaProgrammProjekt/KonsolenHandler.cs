using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MetaProgrammProjekt
{
    class KonsolenHandler   ///Wrapper um string input in form von Parametern direkt an die Konsole zu senden
    {   
        
        private string currPath;
        public void setPath(string Path)
        {
             currPath = Path;
        }
        public void WriteToConsole(string input)
        {
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.UseShellExecute = false;
            info.WorkingDirectory = @currPath;
            p.StartInfo = info;
            p.StartInfo.WorkingDirectory = @currPath;
            p.Start();
            

            using (StreamWriter sw = p.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine(input);
                    
                }
            }
            p.WaitForExit();

            
        }
    }
}
