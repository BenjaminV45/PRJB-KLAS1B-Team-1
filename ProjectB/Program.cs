using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ProjectB
{
    public class Intro
    {
        public void intro()
        {
            string json = File.ReadAllText("settings.json");
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject(json) as JObject;
            JToken jToken = jObject.SelectToken("language");
            string rssTitle = (string)jToken;

            if (rssTitle == "")
            {

                Console.WriteLine("Geachte meneer, mevrouw, wees welkom bij La Mouette.");
                Console.WriteLine("Kies alstublieft het continent waar u woont.\n");
                Console.WriteLine("Dear sir, Madame, welcome to La Mouette.");
                Console.WriteLine("Please choose the continet where u live.\n");

                Tuple<int, string, string>[] continent =
                {
                Tuple.Create(1, "Europa", "NL"),
                Tuple.Create(2, "Afrika", "NL"),
                Tuple.Create(3, "Noord-Amerika", "EN"),
                Tuple.Create(4, "Zuid-Amerika", "EN"),
                Tuple.Create(5, "Azië", "EN"),
                Tuple.Create(6, "Australië", "EN"),
                };


                foreach (Tuple<int, string, string> row in continent)
                {
                    Console.WriteLine(row.Item1 + " | " + row.Item2);
                }

                int input;
                bool complete = false;

                while (!complete)
                {

                    Console.WriteLine("Voer een nummer in:");
                    try
                    {
                        input = Convert.ToInt32(Console.ReadLine());
                        if (input < 7 && input > 0)
                        {
                            complete = true;
                            jToken.Replace(continent[input - 1].Item3);
                            string updateSettings = jObject.ToString();
                            File.WriteAllText("settings.json", updateSettings);
                        }
                        else
                        {
                            Console.WriteLine("Kies een nummer tussen 1 en 6.");
                        }

                    }
                    catch
                    {
                        Console.WriteLine("error.");

                    }
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Intro Intro = new Intro();
            Intro.intro();

            Console.WriteLine("Lets hope this works. Test");
        }
    }
}
