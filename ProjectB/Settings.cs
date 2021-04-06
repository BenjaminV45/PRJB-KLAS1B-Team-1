using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ProjectB
{
    public class Settings
    {
        public static JObject SETTINGS = Newtonsoft.Json.JsonConvert.DeserializeObject(File.ReadAllText(@"..\..\..\Data\settings.json")) as JObject;
        public static int SETTINGS_TABLES = (int)SETTINGS["tables"];
        //static int LogKey(string args) 
        //{
        //    List<string> log = new List<string>();
        //    log.Add(args);
        //    return log.Count;

        //}
        public string getText(object[] value)
        {
            JObject language = Newtonsoft.Json.JsonConvert.DeserializeObject(File.ReadAllText(@"..\..\..\Data\languages.json")) as JObject;
            string data = (string)language[(string)SETTINGS["language"]][value[0]][value[1]];
            return data;
        }

    }
}