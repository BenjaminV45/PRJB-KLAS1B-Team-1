using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ProjectB
{
    public class Settings
    {
        public static JObject SETTINGS = Newtonsoft.Json.JsonConvert.DeserializeObject(File.ReadAllText("settings.json")) as JObject;

        public string getText(object[] value)
        {
            JObject language = Newtonsoft.Json.JsonConvert.DeserializeObject(File.ReadAllText("languages.json")) as JObject;
            string data = (string)language[(string)SETTINGS["language"]][value[0]][value[1]];
            return data;
        }
    }
}