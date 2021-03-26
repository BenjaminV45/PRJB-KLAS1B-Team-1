using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ProjectB
{
    public class Introduction : Settings
    {
        public void intro()
        {
            if (SETTING_LANGUAGE == "")
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

                    Console.WriteLine("\nKies het nummer van uw continent:");
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
                            Console.Clear();
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
