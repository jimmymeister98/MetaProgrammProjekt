using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MetaProgrammProjekt
{
    class JsonToClass
    {   
        private static List<string> dateienList = new List<string>();
        private static JObject o1;
        private static string classlibname;
        public static void ConvertJson(string PathJson, string PathLib)
        {   
            
            o1 = JObject.Parse(File.ReadAllText(PathJson)); //lese json ein
            int objektCount = o1["umpleClasses"].Count();       //zähle Objekte der Json des Arrays "umpleClasses"
            classlibname = Path.GetFileNameWithoutExtension(PathJson);
            for (int i = 0; i < objektCount; i++)
            {
                    string idteststring = (string) o1["umpleClasses"][i]["id"];  //gib alle id's aus
                    dateienList.Add(idteststring);

            }
            WriteToClasses(PathLib);

        }

        private static void WriteToClasses(string PathLib)
        {
            string[] lines =
            {
                "using System;", "using System.Collections;", "using System.Linq;", "using System.Reflection.Metadata;", "using System.Text;"
                ,"using System.Threading.Tasks;", "", "using System.IO;","using System.Text.Json;","","","","namespace "+classlibname+"{"
            };

            int objektCount = o1["umpleAssociations"].Count();       //zähle Objekte der Json des Arrays "umpleClasses"

            ///Schreibe Basiscode in alle dateien
            for (int i = 0; i < objektCount; i++)
            {
                string ID1 = (string) o1["umpleAssociations"][i]["classOneId"];
                string ID2 = (string)o1["umpleAssociations"][i]["classTwoId"];
                string[] tempID1 = {"", "    public class "+ID1+"{","    "};
                string[] tempID2 = { "", "   public class " + ID2 + "{", " " };
                if (!File.Exists(PathLib + "\\" + ID1 + ".cs"))
                {
                    File.WriteAllLines(PathLib + "\\" + ID1 + ".cs", lines);
                    File.AppendAllLines(PathLib + "\\" + ID1 + ".cs", tempID1);
                }

                if (!File.Exists(PathLib + "\\" + ID2 + ".cs"))
                {
                    File.WriteAllLines(PathLib + "\\" + ID2 + ".cs", lines);
                    File.AppendAllLines(PathLib + "\\" + ID2 + ".cs", tempID2);
                }
            }

            for (int i = 0; i < objektCount; i++)
            {
                string ID1 = (string)o1["umpleAssociations"][i]["classOneId"];
                string ID2 = (string)o1["umpleAssociations"][i]["classTwoId"];
                string ID1relation = (string)o1["umpleAssociations"][i]["isLeftNavigable"]; 
                string ID2relation = (string)o1["umpleAssociations"][i]["isRightNavigable"];
                string[] tempID1 = { "       public " + ID2 + " " + ID2 + " {set;}", "","}" };
                string[] tempID2 = { "       public " + ID1 + " " + ID1 + " {set;}", "","}"};
                File.AppendAllLines(PathLib + "\\" + ID1 + ".cs", tempID1);
                File.AppendAllLines(PathLib + "\\" + ID2 + ".cs", tempID2);
            }

            //for (int i = 0; i < objektCount; i++)
            //{
            //    string ID1 = (string)o1["umpleAssociations"][i]["classOneId"];
            //    string ID2 = (string)o1["umpleAssociations"][i]["classTwoId"];
            //    string ID1relation = (string)o1["umpleAssociations"][i]["isLeftNavigable"];
            //    string ID2relation = (string)o1["umpleAssociations"][i]["isRightNavigable"];
            //    string[] tempID1 = { "", "   public class " + ID1 + "{", "       public " + ID2 + " " + ID2 + " {set;}", "" };
            //    string[] tempID2 = { "", "   public class " + ID2 + "{", "       public " + ID1 + " " + ID1 + " {set;}", "" };
            //    File.AppendAllLines(PathLib + "\\" + ID1 + ".cs", tempID1);
            //    File.AppendAllLines(PathLib + "\\" + ID2 + ".cs", tempID2);
            //}
        }


    }

}

