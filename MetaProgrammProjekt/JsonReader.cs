using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MetaProgrammProjekt
{
    class JsonToClass
    {
        public static void LoadJson(string Path)
        {
            JObject o1 = JObject.Parse(File.ReadAllText(Path)); //lese json ein
            int objektCount = o1["umpleClasses"].Count();       //zähle Objekte der Json des Arrays "umpleClasses"
            for (int i = 0; i < objektCount; i++)   
               {
                    string idteststring = (string) o1["umpleClasses"][i]["id"];  //gib alle id's aus
                    Console.WriteLine(idteststring);
                    using (FileStream fs = File.Create("C:\\Users\\Jimmy Neitzert\\Downloads\\"+idteststring+".cs"));
               }
            
                

        }
    }

    class JsonToRelation
    {

    }
}

