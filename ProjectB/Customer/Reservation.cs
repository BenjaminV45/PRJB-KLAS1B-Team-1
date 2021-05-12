using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ProjectB
{
    class Reservation : Option
    {
        private int persons;
        public dynamic data;
        public dynamic menu;

        public Reservation()
        {
            this.menu = new Json("menu.json").Read();
            this.data = new Json("reservation.json").Read();
            
           HowManyPersons();
           //DateAndTime();

            var menuoptions = new[]
{
                Tuple.Create<int, string, Action>(1, "Yes", () => ShowMenu()),
                Tuple.Create<int, string, Action>(2, "No", () => GetNames())
            };
            //new Alfred("reservation", 5).Write();
            // this.Menu(menuoptions);
            GetNames();

        }

        public void HowManyPersons() {
            new Alfred("reservation", 0).Write();
            bool complete = false;
            while (!complete) {
                try
                {
                    this.persons = Convert.ToInt32(Console.ReadLine());
                    if (this.persons <= 0)
                    {
                        new Alfred("reservation", 6).Write();
                    } else
                    {
                        complete = true;
                    }
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

        public void ShowMenu()
        {
            Console.WriteLine(this.persons);
            Console.WriteLine("\nVegetarisch/Vegan: ");
            WriteMenu(this.menu.vegan);
            Console.WriteLine("\nVis/Fish: ");
            WriteMenu(this.menu.vis);
            Console.WriteLine("\nImpala: ");
            WriteMenu(this.menu.impala);

            void WriteMenu(dynamic a) {
                
                foreach (var row in a)
                {
                    int count = 0;
                    foreach (var col in row)
                    {
                        Console.WriteLine(col);
                    }
                }
            }
        }

        public void GetNames()
        {
            new Alfred("reservation", 12).Write();
            object[][] bookinfo = new object[this.persons][];
            int countImpala = 0;
            bool complete = false;
            string menu = null;

            new Alfred("reservation", 7).Line();
            string fName = Console.ReadLine();
            new Alfred("reservation", 8).Line();
            string SName = Console.ReadLine();
            while (!complete)
            {
                new Alfred("reservation", 9).Line();
                menu = Console.ReadLine();
                if (menu.Contains("Vis") || menu.Contains("vis") || menu.Contains("Vega") || menu.Contains("vega") || menu.Contains("Fish") || menu.Contains("fish"))
                {
                    complete = true;
                }
                else if (menu.Contains("Impala") || menu.Contains("impala"))
                {
                    complete = true;
                    countImpala++;
                }
                else
                {
                    new Alfred("error", 1).Write();
                }
            }
            new Alfred("reservation", 10).Line();
            string Allergie = Console.ReadLine();
            new Alfred("reservation", 11).Line();
            string Kcal = Console.ReadLine();



            bookinfo[0] = new object[]
            {
                   fName, SName, menu, Allergie, Kcal
            };

            Console.Clear();


            for (int i = 1; i < this.persons; i++)
            {
                string guestMenu = null;
                bool comp = false;
                new Alfred("reservation", 13).Line();
                string guestfName = Console.ReadLine();
                new Alfred("reservation", 14).Line();
                string guestSName = Console.ReadLine();
                while (!comp)
                {
                    new Alfred("reservation", 15).Line();
                    guestMenu = Console.ReadLine();
                    if (guestMenu.Contains("Vis") || guestMenu.Contains("vis") || guestMenu.Contains("Vega") || guestMenu.Contains("vega"))
                    {
                        comp = true;
                    }
                    else if (guestMenu.Contains("Impala") || guestMenu.Contains("impala"))
                    {
                        comp = true;
                        countImpala++;
                    }
                    else
                    {
                        new Alfred("error", 1).Write();
                    }
                }
                new Alfred("reservation", 16).Line();
                string guestAllergie = Console.ReadLine();
                new Alfred("reservation", 17).Line();
                string guestKcal = Console.ReadLine();

                bookinfo[i] = new object[]
                {
                    guestfName, guestSName, guestMenu, guestAllergie, guestKcal
                };
                Console.Clear();
            }
            Console.WriteLine(bookinfo[0][0]);
            Console.WriteLine(countImpala);

        }
    }
}
