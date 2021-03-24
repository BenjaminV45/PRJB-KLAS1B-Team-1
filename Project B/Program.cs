using System;

namespace Project_B
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Geachte meneer, mevrouw, wees welkom bij La Mouette.");
            Console.WriteLine("Kies alstublieft het continent waar u woont.");
            Console.WriteLine("Dear sir, Madame, welcome to La Mouette.");
            Console.WriteLine("Please choose the continet where u live.test");

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

            string input = Console.ReadLine();
            
        }
    }
}