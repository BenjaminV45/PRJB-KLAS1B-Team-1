using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectB
{
    class leaveReview
    {
        public string lang;
        public int resCode;
        public string review;
        public int sterren;
        public leaveReview()
        {
            this.lang = new Json("language.json").Read();

            Console.WriteLine("\nVul uw unieke reserveringscode in: ");
            resCode = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nUw review mag bestaan uit maximaal 512 karakters: ");
            this.review = Console.ReadLine();

            bool tmp = false;
            while (!tmp)
            {
                if (this.review.Length > 512)
                {
                    Console.WriteLine("\nNiet meer dan 512 karakters toegestaan!");
                }
            }

            Console.WriteLine("\nGeef uw diner een beoordeling van 1 tot 5: ");
            this.sterren = Convert.ToInt32(Console.ReadLine());
        }
    }
}
