using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectB
{
    class AskReview
    {

        public AskReview()
        {
            bool tmpbool = false;
            while (!tmpbool)
            {
                dynamic reviews = new Json("reviews.json").Read();
                dynamic members = new Json("members.json").Read();

                int count = reviews.Count;

                Random rnd = new Random();
                int randint = rnd.Next(count);

                string name = null;

                foreach (var row in members)
                {
                    if (reviews[randint].memberid == row.id)
                    {
                        name = row.firstname + " " + row.lastname;
                        break;
                    }
                }

                for (int i = 0; i < reviews[randint].rating; i++)
                {
                    Console.Write("*");
                }
                Console.WriteLine("");

                string[] words = reviews[randint].reviewtxt.Split(' ');
                int counterr = 1;
                foreach (var word in words)
                {
                    Console.Write($"{word} ");
                    if (counterr % 15 == 0)
                    {
                        Console.Write("\n");
                        counterr = 1;
                    }
                    counterr++;
                }
                Console.WriteLine("\n" + name);
                new Alfred("AskReview", 0).Write();
                ConsoleKeyInfo tmpkey = Console.ReadKey();
                if (tmpkey.Key == ConsoleKey.Escape)
                {
                    tmpbool = true;
                    break;
                }
                Console.Clear();
            }
            Console.Clear();
            new Start();
        }
    }
}
