using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MetaProgrammProjekt
{
    class KonsolenHandler   ///Wrapper um string input in form von Parametern direkt an die Konsole zu senden
    {
        public void WriteToConsole(string input)
        {
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.UseShellExecute = false;

            p.StartInfo = info;
            p.Start();

            using (StreamWriter sw = p.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine(input);
                    
                }
            }
            
        }
    }
}
