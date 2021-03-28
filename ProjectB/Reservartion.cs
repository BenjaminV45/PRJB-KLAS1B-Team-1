using System;

namespace ProjectB
{
    public class Reservartion : Settings
    {
        public void Constructer()
        {
            Console.WriteLine(getText(new object[] { "reservation", 0 }));
            
            int input;
            bool complete = false;
            while (!complete)
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                    complete = true;
                }
                catch
                {
                    Console.WriteLine(getText(new object[] { "error", 1 }));
                }

            Console.WriteLine(getText(new object[] { "reservation", 1 }));
            DateTime dDate;
            complete = false;

            while (!complete)
            {
                string inputString = Console.ReadLine();
                

                if (DateTime.TryParse(inputString, out dDate))
                {
                    String.Format("{0:d/MM/yyyy HH:mm}", dDate);                  
                }
                else
                {
                    Console.WriteLine(getText(new object[] { "reservation", 2 })); // <-- Control flow goes here
                }
            }
        }
    }
    }
