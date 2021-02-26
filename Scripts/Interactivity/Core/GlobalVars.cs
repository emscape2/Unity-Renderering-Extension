using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactivity.ActionComponents
{
    public class GlobalVars
    {
        private Dictionary<string, int> globalVars;
        private static GlobalVars thisObject;
        private static string Path = Application.persistentDataPath;
        private static readonly string[] omissions = { "ScreenW", "ScreenH", "ScreenDiag", "Pause", "delayGlobal" };
        
        public static GlobalVars getGlobalVars()
        {
            if (thisObject == null)
            {
                thisObject = new GlobalVars();
                return thisObject;
            }
            else
            {
                return thisObject;
            }
        }

        public void setVar(string var, int value, bool write = true)
        {
            if (globalVars.ContainsKey(var))
            {
                if (globalVars[var] != value)
                    globalVars[var] = value;
                else
                    return;
            }
            else
                globalVars.Add(var, value);
            if (write)
                Task.Factory.StartNew(WriteGlobalVarsAsync);
        }

        public int getVar(string var)
        {
            if (!globalVars.ContainsKey(var))
            {
                globalVars.Add(var, 0);
            }
            return globalVars[var];
        }

        private GlobalVars()
        {
            globalVars = OpenAndDeserialize();

            CalculateDefaultGLobals();
        }

        private Task WriteGlobalVarsAsync()
        {
            var task =  new Task(new Action(WriteGlobalVars));

            task.Start();
            return task;
        }

        private void WriteGlobalVars()
        {
            string fileName = Path + "/" + "Config.Json";
            FileInfo fileInfo = new FileInfo(fileName);
            StreamWriter sw;
            if (fileInfo.Exists) 
            {

                fileInfo.Delete();
            }
            
            sw = fileInfo.CreateText();
            string fileContents = Serialize();
            sw.Write(fileContents);
            sw.Flush();
            sw.Close();
            sw.Dispose();


        }



        private string Serialize()
        {

            var list = globalVars.ToList();
            string text = "";
            foreach (var item in list)
            {
                //unity cannot serialize anything properly
                if (omissions.Contains(item.Key)) continue;
                var Primaat = $"{{{item.Key}: {item.Value}}}";
                text += ",\n" + Primaat;
            }

            return text.Substring(2,text.Length-2);
        }

        private Dictionary<string, int> OpenAndDeserialize()
        {
            var answer = new Dictionary<string, int>();

            string fileName = Application.persistentDataPath + "/" + "Config.Json";
            FileInfo fileInfo = new FileInfo(fileName);

            if (fileInfo.Exists)
            {
                try
                {

                    StreamReader sr;
                    sr = fileInfo.OpenText();
                    var contents = sr.ReadToEnd();
                    var split = contents.Split("\n".ToCharArray());
                    foreach (var content in split)
                    {
                        var trimmed = content.Trim("{} ,".ToCharArray()).Split(':');
                        if (!answer.ContainsKey(trimmed[0]))
                            answer.Add(trimmed[0], Convert.ToInt32(trimmed[1].Trim()));
                        else
                            Debug.LogError("Duplicate key in Config.Json: " + trimmed[0]);
                    }
                }
                catch
                {
                    //bummer
                    Debug.LogError("Error loading Config.Json. Location: " + fileName);
                }
            }
            return answer;
        }

        private void CalculateDefaultGLobals()
        {
            int width = Mathf.RoundToInt(Screen.width * 25 / Screen.dpi);
            setVar("ScreenW", width,false);
            int height = Mathf.RoundToInt(Screen.height * 25 / Screen.dpi);
            setVar("ScreenH", height,false);
            setVar("ScreenDiag", Mathf.RoundToInt(Mathf.Sqrt(height * height + width * width)),false);
        }
    }

}
