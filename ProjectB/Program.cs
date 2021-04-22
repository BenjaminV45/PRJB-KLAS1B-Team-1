using System;
using System.Collections.Generic;

namespace ProjectB
{
    class System
    {

    }

    class Json
    {

    }

    class Option : System
    {
        public void Menu(Tuple<int, string, Action>[] options)
        {
            foreach (Tuple<int, string, Action> row in options)
            {
                Console.WriteLine($"[{row.Item1}] {row.Item2}");
            }

            int input;
            bool tmp = false;
            while (!tmp)
            {

                try
                {
                    Console.Write("\nPick a number: ");
                    input = Convert.ToInt32(Console.ReadLine());
                    if (input > options.Length || input < 1)
                    {
                        Console.WriteLine("** " + input + " does not exist. **");
                    }
                    else
                    {
                        tmp = true;
                        options[input - 1].Item3();
                    }
                }

                catch
                {
                    Console.WriteLine("** Input is not valid. Please pick a number. **");
                }
            }
        }
    }
    class Chef : Option
    {
        public Chef()
        {
            var options = new[]
            {
                Tuple.Create<int, string, Action>(1, "Krijg menu", () => new GetMenu()),

            };
            this.Menu(options);
        }
    }

    class Customer : Option
    {

    }

    class Start : Option
    {

        public Start()
        {

        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Chef test = new Chef();
        }
    }
}