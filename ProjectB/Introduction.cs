using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ProjectB
{
    public class Introduction : Settings
    {
        public void intro()
        {
            if ((string)SETTINGS["language"] == "")
            {

                Console.WriteLine("[EN] Hi, I am Alfred your personal assistant. Before we can start I need to know your continent.");
                Console.WriteLine("[NL] Hei, Ik ben Alfred, uw persoonlijke assistent. Voordat we kunnen beginnen, moet ik uw continent weten.\n");

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

                    Console.WriteLine("\n[EN] Type one of the above numbers:");
                    Console.WriteLine("[NL] Type een van de bovenstaande nummers:");
                    try
                    {
                        input = Convert.ToInt32(Console.ReadLine());
                        if (input < 7 && input > 0)
                        {
                            complete = true;
                            JToken jToken = SETTINGS.SelectToken("language");
                            jToken.Replace(continent[input - 1].Item3);
                            string updateSettings = SETTINGS.ToString();
                            File.WriteAllText("settings.json", updateSettings);
                        }
                        else
                        {
                            Console.WriteLine("\nKies alstublieft een nummer tussen 1 en 6.");
                            Console.WriteLine("Please choose a number between 1 and 6.");
                        }

                    }
                    catch
                    {
                        Console.WriteLine("\nDit is geen nummer, kies alstublieft een nummer.");
                        Console.WriteLine("This is not a number. Please choose a number.");
                    }
                }
            }
        }
    }
}
