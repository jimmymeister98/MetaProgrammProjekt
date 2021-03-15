using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Newtonsoft.Json.Linq;

namespace MetaProgrammProjekt
{
    class JsonToClass
    {
        private static JObject o1;  //Hier wird die Json Gespeichert
        private static string classlibname; //Name der json ohne .json

        public static bool CheckJson(string PathJson)
        {
            
            try
            {
                JObject test = JObject.Parse(File.ReadAllText(PathJson)); //lese json ein
                string dateiname = (string)test["umpleClasses"][0]["id"]; 
                Console.WriteLine(dateiname);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show("Ungültiges json format! JSON wurde nicht mit umple generiert!");
                return false;
            }
        }


        public static void ConvertJson(string PathJson, string PathLib)
        {   
            
            o1 = JObject.Parse(File.ReadAllText(PathJson)); //lese json ein
            classlibname = Path.GetFileNameWithoutExtension(PathJson); //setze classlib name (json ohne .json)
            WriteToClasses(PathLib);

        }

        private static void WriteToClasses(string PathLib)
        {
            string[] lines =
            {
                "using System;", "using System.Collections;", "using System.Linq;", "using System.Reflection.Metadata;", "using System.Text;"
                ,"using System.Threading.Tasks;", "", "using System.IO;","using System.Text.Json;","using System.Collections.Generic;","","","","namespace "+classlibname+"{"
            }; //Standardinitialisierung mit den verschiedenen Bibliotheken und das setzen des namespaces der classlib

            int objektCount = o1["umpleAssociations"].Count();          //zähle Objekte der Json des Arrays "umpleAssociations"
            int classCount = o1["umpleClasses"].Count();                //zähle Objekte der Json des Arrays "umpleClasses"


            ///Schreibe Basiscode in alle dateien
            for (int i = 0; i < objektCount; i++)
            {
                string ID1 = (string) o1["umpleAssociations"][i]["classOneId"]; //Auslesen der Objekte A und B
                string ID2 = (string)o1["umpleAssociations"][i]["classTwoId"];  //Auslesen der Objekte A und B
                string ID1relation = (string)o1["umpleAssociations"][i]["multiplicityOne"]; //auslesen der Relation zw Objekt A und B
                string ID2relation = (string)o1["umpleAssociations"][i]["multiplicityTwo"]; //auslesen der Relation zw Objekt A und B
                string[] tempID1 = {"", "    public class "+ID1+"{","    "};    //Klassenname = Obkjektname
                string[] tempID2 = { "", "   public class " + ID2 + "{", " " }; //Klassenname = Obkjektname
                string[] IdCounter = {"","       public int ID { get; }" };
                if (!File.Exists(PathLib + "\\" + ID1 + ".cs")) //Wenn datei nicht existiert dann erstelle neue
                {
                    File.WriteAllLines(PathLib + "\\" + ID1 + ".cs", lines);    //Schreibe Standardinitialisierung (s.o)
                    File.AppendAllLines(PathLib + "\\" + ID1 + ".cs", tempID1); //füge Klassenname Hinzu
                    File.AppendAllLines(PathLib + "\\" + ID1 + ".cs", IdCounter); //ID counter
                }

                if (!File.Exists(PathLib + "\\" + ID2 + ".cs")) 
                {
                    File.WriteAllLines(PathLib + "\\" + ID2 + ".cs", lines);
                    File.AppendAllLines(PathLib + "\\" + ID2 + ".cs", tempID2);
                    File.AppendAllLines(PathLib + "\\" + ID2 + ".cs", IdCounter); //ID counter
                }

                if (ID1relation != "1")//Wenn eine relation mit nur einem objekt dann Keine lise erstellen
                {
                    if (!File.Exists(PathLib + "\\" + ID1 + "Liste.cs")) //wenn datei KlassennameListe.cs nicht existiert dann erstellen
                    {
                        string[] tempRelationID1 = { "", "    public class " + ID1 + "Liste{", " ", 
                            
                            "        private readonly List<" + ID1 + "> liste" + ID1 + " = new List<" + ID1 + ">();" }; //Liste für die 1..N relation hinzufügen
                        File.WriteAllLines(PathLib + "\\" + ID1 + "Liste.cs", lines);   //Basisinitialisierung
                        File.AppendAllLines(PathLib + "\\" + ID1 + "Liste.cs", tempRelationID1);    //Hinzufügen der Liste
                    }
                }

                if (ID2relation != "1")
                {
                    if (!File.Exists(PathLib + "\\" + ID2 + "Liste.cs"))
                    {
                        
                        string[] tempRelationID2 = { "", "    public class " + ID2 + "Liste{", " ", //sonderfall Klasse 2 ist von 1 abhängig deshalb Getter der Übergeordneten klasse hinzufügen
                             "        public "+ID1+" "+ID1+" {get;}"
                            ,"        private readonly List<"+ID2+"> liste"+ID2+" = new List<"+ID2+">();" };
                        File.WriteAllLines(PathLib + "\\" + ID2 + "Liste.cs", lines);
                        File.AppendAllLines(PathLib + "\\" + ID2 + "Liste.cs", tempRelationID2); //Hinzufügen der Liste
                    }
                }
            }



            ///Hinzufügen der Relationen
            for (int i = 0; i < objektCount; i++)
            {
                string ID1 = (string)o1["umpleAssociations"][i]["classOneId"];
                string ID2 = (string)o1["umpleAssociations"][i]["classTwoId"];
                string[] tempID1 = { "       public " + ID2 + " " + ID2 + " {get; set;}"};   
                string[] tempID2 = { "       public " + ID1 + " " + ID1 + " {get; set;}"};   
                File.AppendAllLines(PathLib + "\\" + ID1 + ".cs", tempID1); //füge relationen hinzu
                //File.AppendAllLines(PathLib + "\\" + ID1 + "Liste.cs", tempID1);
                File.AppendAllLines(PathLib + "\\" + ID2 + ".cs", tempID2); //füge relationen hinzu
                //File.AppendAllLines(PathLib + "\\" + ID2 + "Liste.cs", tempID2);
            }

            
            ///Freistehende Klassen Initialisieren

            for (int i = 0; i < classCount; i++)
            {
                string dateiname = (string)o1["umpleClasses"][i]["id"];
                string[] tempID1 = { "", "    public class " + dateiname + "{", "    " };    //Klassenname = Obkjektname
                if (!File.Exists(PathLib + "\\" + dateiname + ".cs"))
                {
                     
                     File.WriteAllLines(PathLib + "\\" + dateiname + ".cs", lines);
                     File.AppendAllLines(PathLib + "\\" + dateiname + ".cs", tempID1);
                }
                 
            }
        

            ///Hinzufügen der attribute
            for (int i = 0; i < classCount; i++)
            {
                string ID1 = (string)o1["umpleClasses"][i]["id"];
                int attribcount = (int) o1["umpleClasses"][i]["attributes"].Count();
                for (int j = 0; j < attribcount; j++)
                {
                    string[] attribID = {"       "+(string)o1["umpleClasses"][i]["attributes"][j]["type"] + " " + (string)o1["umpleClasses"][i]["attributes"][j]["name"] + ";"}; //hole Attribut + Datentyp aus Json
                    File.AppendAllLines(PathLib + "\\" + ID1 + ".cs", attribID); //füge relationen hinzu
                }
            }

            ///Hinzufügen der Konstruktoren

            List<string> accesscheck = new List<string>();
            for (int i = 0; i < objektCount; i++)
                {
                    string ID1 = (string)o1["umpleAssociations"][i]["classOneId"]; 
                    string ID2 = (string)o1["umpleAssociations"][i]["classTwoId"];
                    
                if ((string)o1["umpleAssociations"][i]["multiplicityOne"] == "1") //wenn eine Komposition dann...
                    {
                    string[] tempID1 = { "         public " + ID2 + "(" + ID1 + " " + ID1 + "obj, int " + ID2 + "ID)", "        {", "        " + ID1 + " = " + ID1 + "obj;", "        ID = " + ID2 + "ID;", "        }" };
                    File.AppendAllLines(PathLib + "\\" + ID2 + ".cs", tempID1); //füge relationen hinzu
                    accesscheck.Add(ID2); //schreibe welche dateien "berührt" worden sind um festzustellen wo konstruktoren fehlen
                    }

                }
            //Basiskonstruktoren zu Unbehandelten Klassen hinzufügen
            for (int i = 0; i < classCount; i++)
            {
                string classname = (string)o1["umpleClasses"][i]["id"];
                string[] basisKonstruktor = {"       public " + classname + "(){}"};
                if (!accesscheck.Contains(classname)) //Wenn der Konstruktor noch nicht geschrieben wurde dann..
                {
                    File.AppendAllLines(PathLib+"\\" + classname+".cs",basisKonstruktor);
                }
            }

            for (int i = 0; i < classCount; i++)
            {
                string dateiname = (string)o1["umpleClasses"][i]["id"];
                string[] abschluss = { "","    }","}"};
                if(File.Exists(PathLib + "\\" + dateiname + ".cs"))
                File.AppendAllLines(PathLib + "\\" + dateiname + ".cs", abschluss); //Schließe geöffnete brackets
                if(File.Exists(PathLib + "\\" + dateiname + "Liste.cs"))
                File.AppendAllLines(PathLib + "\\" + dateiname + "Liste.cs", abschluss);
                
            }
        }


    }

}

