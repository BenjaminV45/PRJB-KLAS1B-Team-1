using System.Collections.Generic;

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
}
