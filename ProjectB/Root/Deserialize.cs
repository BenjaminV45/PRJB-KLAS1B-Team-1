using System.Collections.Generic;

namespace ProjectB
{
    public class SettingsJson
    {
        public string language { get; set; }
        public int tables { get; set; }
        public int member_id { get; set; }
    }

    public class LanguageJson
    {
        public Dictionary<string, string[]> EN { get; set; }

        public Dictionary<string, string[]> NL { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class People
    {
        public string name { get; set; }
        public string menu { get; set; }
        public string allergies { get; set; }
        public string kcal { get; set; }
    }

    public class ReservationJson
    {
        public int id { get; set; }

        public string code { get; set; }
        public int memberID { get; set; }
        public int amount { get; set; }
        public string date { get; set; }
        public List<People> People { get; set; }
        public object hunt { get; set; }
        public string currentdate { get; set; }
    }


    public class MembersJson
    {
        public int id { get; set; }
        public string code { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string creditcard { get; set; }
        public string continent { get; set; }
        public string rank { get; set; }
    }

    public class MenuJson
    {
        public List<List<object>> vegan { get; set; }
        public List<List<object>> vis { get; set; }
        public List<List<object>> impala { get; set; }
    }

    public class LogsJson
    {
        public int id { get; set; }
        public string timestamp { get; set; }
        public string description { get; set; }
    }
}

