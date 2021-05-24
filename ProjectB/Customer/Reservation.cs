using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Text;

namespace ProjectB
{
    class Reservation : Option
    {
        private int persons;
        public dynamic data;
        public dynamic menu;
        public bool hunt;
        public int id;
        public string Date;
        public List<People> people;
        public bool hotel;
        public double discount;
        public dynamic members;
        public double prijsmenu;

        public Reservation()
        {
            new System().Log("Reservation has been initialized");
            this.members = new Json("members.json").Read();
            this.menu = new Json("menu.json").Read();
            this.data = new Json("reservation.json").Read();
            this.id = this.data[this.data.Count - 1].id + 1;
            this.hunt = false;
            this.people = new List<People>();
            this.hotel = false;
            this.discount = this.members[new Settings().Member_ind].discount == true ? 0.90 : 1.00;
            this.prijsmenu = 0;


            HowManyPersons();
            DateAndTime();

            var menuoptions = new[]
            {
                Tuple.Create<int, string, Action>(1, "Yes", () => ShowMenu()),
                Tuple.Create<int, string, Action>(2, "No", () => GetNames())
            };
            new Alfred("reservation", 5).Write();
            this.Menu(menuoptions);

            new Alfred("reservation", 19).Write();
            var hoteloptions = new[]
            {
                Tuple.Create<int, string, Action>(1, "Yes", () => BookHotel()),
                Tuple.Create<int, string, Action>(2, "No", () => Checkout())
            };
            this.Menu(hoteloptions);
        }

        public void HowManyPersons()
        {
            new System().Log("HowManyPersons was called");
            new Alfred("reservation", 0).Write();
            bool complete = false;
            while (!complete)
            {
                try
                {
                    this.persons = Convert.ToInt32(Console.ReadLine());
                    if (this.persons <= 0)
                    {
                        new Alfred("reservation", 6).Write();
                    }
                    else
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
            new System().Log("DateAndTime was called");
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
                        this.Date = inputString;
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
            new System().Log("ShowMenu was called");
            Console.WriteLine(this.persons);
            Console.WriteLine("\nVegetarisch/Vegan: ");
            WriteMenu(this.menu.vegan);
            Console.WriteLine("\nVis/Fish: ");
            WriteMenu(this.menu.vis);
            Console.WriteLine("\nImpala: ");
            WriteMenu(this.menu.impala);

            void WriteMenu(dynamic a)
            {

                foreach (var row in a)
                {
                    foreach (var col in row)
                    {
                        Console.WriteLine(col);
                    }
                }
            }
        }

        public void GetNames()
        {
            new System().Log("GetNames was called");
            new Alfred("reservation", 12).Write();
            Tuple<int, string, int>[] optionsmenu =
            {
                    Tuple.Create(1, "Vega", 2500),
                    Tuple.Create(2, "Impala", 3300),
                    Tuple.Create(3, "Fish", 1700),
            };
            int countImpala = 0;
            bool complete = false;
            string menu = null;

            new Alfred("reservation", 7).Line();
            string fName = Console.ReadLine();
            new Alfred("reservation", 8).Line();
            string SName = Console.ReadLine();

            while (!complete)
            {
                foreach (var row in optionsmenu)
                {
                    Console.WriteLine($"[{row.Item1}] {row.Item2}");
                }
                new Alfred("reservation", 9).Line();
                try
                {
                    int menuTmp = Convert.ToInt32(Console.ReadLine());
                    menu = optionsmenu[menuTmp - 1].Item2;
                    this.prijsmenu += optionsmenu[menuTmp - 1].Item3;
                    if (menuTmp == 1 || menuTmp == 3)
                    {
                        complete = true;
                    }
                    else if (menuTmp == 2)
                    {
                        complete = true;
                        countImpala++;
                    }
                    else
                    {
                        new Alfred("error", 1).Write();
                    }
                }
                catch
                {
                    new Alfred("error", 1).Write();
                }

            }

            new Alfred("reservation", 10).Line();
            string Allergie = Console.ReadLine();
            new Alfred("reservation", 11).Line();
            string Kcal = Console.ReadLine();

            var person = new People
            {
                name = $"{fName} {SName}",
                menu = menu,
                allergies = Allergie,
                kcal = Kcal
            };

            this.people.Add(person);

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
                    foreach (var row in optionsmenu)
                    {
                        Console.WriteLine($"[{row.Item1}] {row.Item2}");
                    }
                    new Alfred("reservation", 9).Line();
                    try
                    {
                        int menuTmp = Convert.ToInt32(Console.ReadLine());
                        guestMenu = optionsmenu[menuTmp - 1].Item2;
                        this.prijsmenu += optionsmenu[menuTmp - 1].Item3;
                        if (menuTmp == 1 || menuTmp == 3)
                        {
                            comp = true;
                        }
                        else if (menuTmp == 2)
                        {
                            comp = true;
                            countImpala++;
                        }
                        else
                        {
                            new Alfred("error", 1).Write();
                        }
                    }
                    catch
                    {
                        new Alfred("error", 1).Write();
                    }
                }
                new Alfred("reservation", 16).Line();
                string guestAllergie = Console.ReadLine();
                new Alfred("reservation", 17).Line();
                string guestKcal = Console.ReadLine();

                if (countImpala > 3)
                {
                    bool tmp = false;
                    while (!tmp)
                    {
                        new Alfred("reservation", 18).Line();
                        string optie = Console.ReadLine();
                        if (optie == "Y" || optie == "y" || optie == "n" || optie == "N")
                        {
                            if (optie == "Y" || optie == "y")
                            {
                                tmp = true;
                                this.hunt = true;
                            }
                        }
                        else
                        {
                            new Alfred("error", 2).Write();
                        }
                    }
                }
                var guest = new People
                {
                    name = $"{guestfName} {guestSName}",
                    menu = guestMenu,
                    allergies = guestAllergie,
                    kcal = guestKcal
                };
                people.Add(guest);
                Console.Clear();
            }

        }

        public void BookHotel()
        {
            new System().Log("BookHotel was called");
            this.hotel = true;
            new Alfred("reservation", 20).Write();
            Checkout();
        }

        public void Checkout()
        {
            new System().Log("Checkout was called");
            string code = null;
            DateTime dateTime = DateTime.UtcNow.Date;
            bool boolean = false;
            while (!boolean)
            {
                boolean = true;
                code = new System().RandomChar(3, 3);
                foreach (var row in this.data)
                {
                    if (code == row.code)
                    {
                        boolean = false;
                        break;
                    }

                }
            }
            var reservation = new ReservationJson
            {
                id = this.id,
                code = code,
                memberID = new Settings().Member_id,
                amount = this.persons,
                date = this.Date,
                People = this.people,
                hunt = this.hunt,
                currentdate = dateTime.ToString("dd/MM/yyyy"),
                discount = this.members[new Settings().Member_ind].discount,
                hotel = this.hotel
            };

            if (this.hotel)
            {
                Tuple<string, int>[] km =
                {
                    Tuple.Create("Europa", 9100),
                    Tuple.Create("North-america", 14822),
                    Tuple.Create("Africa", 5004),
                    Tuple.Create("South-america", 7763),
                    Tuple.Create("Asia", 12324),
                    Tuple.Create("Australia", 10520)
                };

                string index = this.members[new Settings().Member_ind].continent;

                foreach (var row in km)
                {
                    if (index == row.Item1)
                    {
                        prijsmenu += ((row.Item2 / 100) * 13.55) * this.persons;
                        prijsmenu += 7350 * this.persons;
                    }
                }
            }
            if (this.hunt)
            {
                prijsmenu += 1900 * this.persons;
            }
            double totalcost = prijsmenu * this.discount;
            new Alfred("reservation", 21).Line();
            Console.Write($"{totalcost} euro.");
            new Alfred("reservation", 23).Write(); // butler vraagt om aanpassingen aanpassingen
            var changeoptions = new[]
            {
                Tuple.Create<int, string, Action>(1, "Yes", () => changeReservation()),
                Tuple.Create<int, string, Action>(2, "No", () => finalize())
            };
            this.Menu(changeoptions);

            new Alfred("reservation", 22).Write();

        }

        public void changeReservation()
        {
            dynamic reservation = this.data[this.data.Count - 1];
            bool comp = false;
            int counter = 0;
            while (!comp)
            {
                ConsoleKeyInfo inp = Console.ReadKey();
                if (inp.Key == ConsoleKey.RightArrow)
                {
                    counter++;
                }
                if (inp.Key == ConsoleKey.LeftArrow)
                {
                    counter--;
                }
                if (counter == 1)
                {
                    new Alfred("reservation", 25).Line();
                    Console.Write(reservation.date);                  
                }
            }
            
            
        }
        public void finalize()
        {

        }
    }
}
