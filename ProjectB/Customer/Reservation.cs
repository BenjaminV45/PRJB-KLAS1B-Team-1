using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectB
{
    class Reservation
    {
        private int persons;
        public dynamic data;
        public dynamic menu;

        public Reservation()
        {
            this.menu = new Json("menu.json").Read();
            this.data = new Json("reservation.json").Read();

            // HowManyPersons();
            // DateAndTime();

            var menuoptions = new[]
{
                Tuple.Create<int, string, Action>(1, "Yes", () => ShowMenu()),
                Tuple.Create<int, string, Action>(2, "No", () => new GetMenu())
            };

            ChooseMenu(menuoptions);
        }

        public void HowManyPersons() {
            new Alfred("reservation", 0).Write();
            bool complete = false;
            while (!complete) {
                try
                {
                    this.persons = Convert.ToInt32(Console.ReadLine());
                    complete = true;
                }
                catch
                {
                    new Alfred("error", 0).Write();
                }
            }
        }
        public void DateAndTime()
        {
            new Alfred("reservation", 1).Write();
            DateTime dDate;
            bool complete = false;

            while (!complete)
            {
                string inputString = Console.ReadLine();

                if (DateTime.TryParse(inputString, out dDate))
                {
                    String.Format("{0:d/MM/yyyy}", dDate);

                    if (dDate < DateTime.Today)
                    {
                        new Alfred("reservation", 2).Write();
                    }
                    else if (dDate.DayOfWeek == DayOfWeek.Monday)
                    {
                        new Alfred("reservation", 4).Write();
                    }
                    else
                    {
                        complete = true;
                    }
                }
                else
                {
                    new Alfred("reservation", 3).Write();
                }
            }
        }

        public void ChooseMenu(Tuple<int, string, Action>[] options)
        {
            new Alfred("reservation", 5).Write();
            foreach (Tuple<int, string, Action> row in options)
            {
                Console.WriteLine($"[{row.Item1}] {row.Item2}");
            }

            string inputTmp;
            int input;
            bool tmp = false;

            while (!tmp)
            {
                try
                {
                    Console.Write("\nPick a number: ");
                    inputTmp = Console.ReadLine();  
                    input = Convert.ToInt32(inputTmp);
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

        public void ShowMenu()
        {
            foreach (var row in this.menu)
            {
                Console.WriteLine(row);
            }
        }
    }
}
