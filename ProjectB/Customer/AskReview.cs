using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectB
{
    class AskReview
    {

        public AskReview()
        {

            dynamic reviews = new Json("reviews.json").Read();
            dynamic members = new Json("members.json").Read();

            int count = reviews.Count;

            Random rnd = new Random();
            int randint = rnd.Next(count);

            string name = null;

            foreach(var row in members)
            {
                if (reviews[randint].memberid == row.id)
                {
                    name = row.firstname + " " + row.lastname;
                    break;
                }
            }

            Console.Write("\n[");
            for (int i = 0; i < reviews[randint].rating; i++)
            {
                Console.Write("*");
            }
            Console.WriteLine("] " + name);
            Console.WriteLine("_________________________________________________________________________________\n");

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
        }
    }
}
