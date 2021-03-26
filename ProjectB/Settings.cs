using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ProjectB
{
    public class Settings
    {
        public static JObject SETTINGS = Newtonsoft.Json.JsonConvert.DeserializeObject(File.ReadAllText("settings.json")) as JObject;
        public static string SETTING_LANGUAGE = (string)SETTINGS["language"];

        public string getLanguage(object[] value)
        {
            JObject language = Newtonsoft.Json.JsonConvert.DeserializeObject(File.ReadAllText("language.json")) as JObject;
            string data = (string)language[SETTING_LANGUAGE][value[0]][value[1]];
            return data;
        }
    }

}