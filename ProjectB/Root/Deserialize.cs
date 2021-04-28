﻿using System.Collections.Generic;

namespace ProjectB
{
    public class SettingsJson
    {
        public string language { get; set; }
        public int tables { get; set; }
    }

    public class LanguageJson
    {
        public Dictionary<string, string[]> EN { get; set; }

        public Dictionary<string, string[]> NL { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Poeple
    {
        public string name { get; set; }
        public string menu { get; set; }
        public string preference { get; set; }
        public string allergies { get; set; }
        public string kcal { get; set; }
    }

    public class Reservations
    {
        public int id { get; set; }
        public string memberID { get; set; }
        public int amount { get; set; }
        public string date { get; set; }
        public List<Poeple> poeple { get; set; }
        public object rating { get; set; }
    }

    public class ReservationJson
    {
        public List<Reservations> reservations { get; set; }
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

    public class Veganistisch
    {
        public List<string> aperitif { get; set; }
        public List<object> voorgerecht { get; set; }
        public List<object> hoofdgerecht { get; set; }
        public List<object> desert { get; set; }
    }

    public class Imapala
    {
        public List<object> aperitif { get; set; }
        public List<object> voorgerecht { get; set; }
        public List<object> hoofdgerecht { get; set; }
        public List<object> desert { get; set; }
    }

    public class Vis
    {
        public List<object> aperitif { get; set; }
        public List<object> voorgerecht { get; set; }
        public List<object> hoofdgerecht { get; set; }
        public List<object> desert { get; set; }
    }




}

