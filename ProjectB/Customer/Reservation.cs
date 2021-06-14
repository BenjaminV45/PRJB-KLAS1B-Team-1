using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Text;

namespace ProjectB
{
    class Reservation : Option
    {
        public int countImpala;
        public string code;
        public dynamic[] currentres;
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
        public ReservationJson reservation;
        public double totalcost;
        public People guests;
        public dynamic tables;
        public TablesJson newtabledate;
        public int resindex;
        public int tableindex;
        public int tablecount;
        public Tuple<string, int> cord;

        public Reservation()
        {
            Console.ForegroundColor = ConsoleColor.White;
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
            this.currentres = new dynamic[3];
            this.countImpala = 0;
            this.code = null;
            this.totalcost = 0;
            this.tables = new Json("tables.json").Read();
            this.tablecount = 0;

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
                    else if (this.persons > 22)
                    {
                        new Alfred("tables", 1).Write();
                    }
                    else
                    {
                        complete = true;
                        if (this.persons == 1)
                        {
                            this.tablecount = 2;
                        } else
                        {
                            this.tablecount = this.persons;
                        }
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
            GetNames();
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
            this.countImpala = 0;
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
                    this.totalcost += optionsmenu[menuTmp - 1].Item3;
                    if (menuTmp == 1 || menuTmp == 3)
                    {
                        complete = true;
                    }
                    else if (menuTmp == 2)
                    {
                        complete = true;
                        this.countImpala++;
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

            this.guests = new People
            {
                name = $"{fName} {SName}",
                menu = menu,
                allergies = Allergie,
                kcal = Kcal
            };

            this.people.Add(this.guests);

            Console.Clear();

            int tmp = 1;
            while (tmp <= this.persons - 1)
            {
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
                            this.totalcost += optionsmenu[menuTmp - 1].Item3;
                            if (menuTmp == 1 || menuTmp == 3)
                            {
                                comp = true;
                            }
                            else if (menuTmp == 2)
                            {
                                comp = true;
                                this.countImpala++;
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

                    this.guests = new People
                    {
                        name = $"{guestfName} {guestSName}",
                        menu = guestMenu,
                        allergies = guestAllergie,
                        kcal = guestKcal
                    };
                    tmp++;
                    people.Add(this.guests);
                    Console.Clear();
                }
            }
            if (this.countImpala > 3)
            {
                Hunt();
            }
            Tables();
        }

        public void Hunt()
        {
            bool tmp = false;
            while (!tmp)
            {
                new Alfred("reservation", 18).Line();
                ConsoleKeyInfo optie = Console.ReadKey();
                if (optie.Key == ConsoleKey.Enter)
                {
                    tmp = true;
                    this.hunt = true;
                    new Alfred("reservation", 37).Write();
                }
                else
                {
                    new Alfred("error", 2).Write();
                }
            }
        }

        public void Tables()
        {
            this.resindex = 0;
            for (int i = 0; i < this.tables.Count; i++)
            {
                resindex = i;
                string resdate = this.tables[i].date;
                int row1 = this.tables[i].row1.Count;
                int row2 = this.tables[i].row2.Count;
                int row3 = this.tables[i].row3.Count;
                if (resdate == this.Date)
                {
                    if (row1 + this.tablecount > 22)
                    {
                        if (row2 + this.tablecount > 22)
                        {
                            if (row3 + this.tablecount > 22)
                            {
                                new Alfred("tables", 2).Write();
                                DateAndTime();
                                Tables();
                            }
                            else
                            {

                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (this.resindex == this.tables.Count - 1 && this.tables[resindex].date != this.Date)
            {
                this.newtabledate = new TablesJson
                {
                    date = this.Date,
                    row1 = new List<string>(),
                    row2 = new List<string>(),
                    row3 = new List<string>()
                };
                this.tables.Add(this.newtabledate);
                this.resindex++;
            }
            if (this.tables[this.resindex].row1.Count + this.tablecount < 24)
            {
                if (this.tables[this.resindex].row1.Count + this.tablecount + 2 < 24)
                {
                    if (this.tablecount <= 2)
                    {
                        this.tables[this.resindex].row1.Add("[");
                        this.tables[this.resindex].row1.Add("]");
                        this.tables[this.resindex].row1.Add(" ");
                        this.tables[this.resindex].row1.Add(" ");
                        this.tableindex = this.tables[this.resindex].row1.Count - 4;
                        this.cord = Tuple.Create("row1", this.tableindex);
                    }
                    else if (this.tablecount > 2)
                    {
                        this.tables[this.resindex].row1.Add("[");
                        for (int i = 0; i < this.tablecount - 2; i++)
                        {
                            this.tables[this.resindex].row1.Add(".");
                        }
                        this.tables[this.resindex].row1.Add("]");
                        this.tables[this.resindex].row1.Add(" ");
                        this.tables[this.resindex].row1.Add(" ");
                        this.tableindex = this.tables[this.resindex].row1.Count - (this.tablecount + 2);
                        this.cord = Tuple.Create("row1", this.tableindex);
                    }

                }
            }
            else if (this.tables[this.resindex].row2.Count + this.tablecount < 24)
            {
                if (this.tables[this.resindex].row2.Count + this.tablecount + 2 < 24)
                {
                    if (this.tablecount <= 2)
                    {
                        this.tables[this.resindex].row2.Add("[");
                        this.tables[this.resindex].row2.Add("]");
                        this.tables[this.resindex].row2.Add(" ");
                        this.tables[this.resindex].row2.Add(" ");
                        this.tableindex = this.tables[this.resindex].row2.Count - 4;
                        this.cord = Tuple.Create("row2", this.tableindex);
                    }
                    else if (this.tablecount > 2)
                    {
                        this.tables[this.resindex].row2.Add("[");
                        for (int i = 0; i < this.tablecount - 2; i++)
                        {
                            this.tables[this.resindex].row2.Add(".");
                        }
                        this.tables[this.resindex].row2.Add("]");
                        this.tables[this.resindex].row2.Add(" ");
                        this.tables[this.resindex].row2.Add(" ");
                        this.tableindex = this.tables[this.resindex].row2.Count - (this.tablecount + 2);
                        this.cord = Tuple.Create("row2", this.tableindex);
                    }

                }
            }
            else if (this.tables[this.resindex].row3.Count + this.tablecount < 24)
            {
                if (this.tables[this.resindex].row3.Count + this.tablecount + 2 < 24)
                {
                    if (this.tablecount <= 2)
                    {
                        this.tables[this.resindex].row3.Add("[");
                        this.tables[this.resindex].row3.Add("]");
                        this.tables[this.resindex].row3.Add(" ");
                        this.tables[this.resindex].row3.Add(" ");
                        this.tableindex = this.tables[this.resindex].row3.Count - 4;
                        this.cord = Tuple.Create("row3", this.tableindex);
                    }
                    else if (this.tablecount > 2)
                    {
                        this.tables[this.resindex].row3.Add("[");
                        for (int i = 0; i < this.tablecount - 2; i++)
                        {
                            this.tables[this.resindex].row3.Add(".");
                        }
                        this.tables[this.resindex].row3.Add("]");
                        this.tables[this.resindex].row3.Add(" ");
                        this.tables[this.resindex].row3.Add(" ");
                        this.tableindex = this.tables[this.resindex].row3.Count - (this.tablecount + 2);
                        this.cord = Tuple.Create("row3", this.tableindex);
                    }

                }
            }
            DisplayTables();
        }

        public void DisplayTables()
        {
            //Console.WriteLine(this.cord);
            // rij 1
            if (this.cord.Item1 == "row1")
            {
                Console.WriteLine("############################");
                Console.WriteLine("#                          #");
                Console.Write("#  ");
                for(int i = 0; i < this.cord.Item2; i++)
                {
                    Console.Write(this.tables[this.resindex].row1[i]);
                }
                for(int j = this.cord.Item2; j < this.cord.Item2 + this.tablecount + 2; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(this.tables[this.resindex].row1[j], Console.ForegroundColor);
                    Console.ResetColor();
                }
                if (this.tables[this.resindex].row1.Count <= 24)
                {
                    Console.Write(new String(' ', (24 - this.tables[this.resindex].row1.Count)));
                    Console.Write("#\n");
                }
                else
                {
                    Console.Write("#\n");
                }
                Console.WriteLine("#                          #");
                Console.Write("#  ");
                Console.Write(String.Join("", this.tables[this.resindex].row2));
                if (this.tables[this.resindex].row2.Count <= 24)
                {
                    Console.Write(new String(' ', 24 - this.tables[this.resindex].row2.Count));
                    Console.Write("#\n");
                }
                else
                {
                    Console.Write("#\n");
                }
                Console.WriteLine("#                          #");
                Console.Write("#  ");
                Console.Write(String.Join("", this.tables[this.resindex].row3));
                if (this.tables[this.resindex].row3.Count <= 24)
                {
                    Console.Write(new String(' ', 24 - this.tables[this.resindex].row3.Count));
                    Console.Write("#\n");
                }
                else
                {
                    Console.Write("#\n");
                }
                Console.WriteLine("#                          #");
                Console.WriteLine("############################");
            }

            // rij 2
            else if (this.cord.Item1 == "row2")
            {
                Console.WriteLine("############################");
                Console.WriteLine("#                          #");
                Console.Write("#  ");
                Console.Write(String.Join("", this.tables[this.resindex].row1));
                if (this.tables[this.resindex].row1.Count <= 24)
                {
                    Console.Write(new String(' ', 24 - this.tables[this.resindex].row1.Count));
                    Console.Write("#\n");
                }
                else
                {
                    Console.Write("#\n");
                }
                Console.WriteLine("#                          #");
                Console.Write("#  ");
                for (int i = 0; i < this.cord.Item2; i++)
                {
                    Console.Write(this.tables[this.resindex].row1[i]);
                }
                for (int j = this.cord.Item2; j < this.cord.Item2 + this.tablecount + 2; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(this.tables[this.resindex].row1[j], Console.ForegroundColor);
                    Console.ResetColor();
                }
                if (this.tables[this.resindex].row2.Count <= 24)
                {
                    Console.Write(new String(' ', 24 - this.tables[this.resindex].row2.Count));
                    Console.Write("#\n");
                }
                else
                {
                    Console.Write("#\n");
                }
                Console.WriteLine("#                          #");
                Console.Write("#  ");
                Console.Write(String.Join("", this.tables[this.resindex].row3));
                if (this.tables[this.resindex].row3.Count <= 24)
                {
                    Console.Write(new String(' ', 24 - this.tables[this.resindex].row3.Count));
                    Console.Write("#\n");
                }
                else
                {
                    Console.Write("#\n");
                }
                Console.WriteLine("#                          #");
                Console.WriteLine("############################");
            }

            // rij 3
            else if (this.cord.Item1 == "row3")
            {
                Console.WriteLine("############################");
                Console.WriteLine("#                          #");
                Console.Write("#  ");
                Console.Write(String.Join("", this.tables[this.resindex].row1));
                if (this.tables[this.resindex].row1.Count <= 24)
                {
                    Console.Write(new String(' ', 24 - this.tables[this.resindex].row1.Count));
                    Console.Write("#\n");
                }
                else
                {
                    Console.Write("#\n");
                }
                Console.WriteLine("#                          #");
                Console.Write("#  ");
                Console.Write(String.Join("", this.tables[this.resindex].row2));
                if (this.tables[this.resindex].row2.Count <= 24)
                {
                    Console.Write(new String(' ', 24 - this.tables[this.resindex].row2.Count));
                    Console.Write("#\n");
                }
                else
                {
                    Console.Write("#\n");
                }
                Console.WriteLine("#                          #");
                Console.Write("#  ");
                for (int i = 0; i < this.cord.Item2; i++)
                {
                    Console.Write(this.tables[this.resindex].row3[i]);
                }
                for (int j = this.cord.Item2; j < this.cord.Item2 + this.tablecount + 2; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(this.tables[this.resindex].row3[j], Console.ForegroundColor);
                    Console.ResetColor();
                }
                if (this.tables[this.resindex].row3.Count <= 24)
                {
                    Console.Write(new String(' ', (24 - this.tables[this.resindex].row3.Count)));
                    Console.Write("#\n");
                }
                else
                {
                    Console.Write("#\n");
                }
                Console.WriteLine("#                          #");
                Console.WriteLine("############################");
            }
        }

        public void BookHotel()
        {
            new System().Log("BookHotel was called");
            if (this.hotel == false)
            {
                this.hotel = true;
            }
            else
            {
                this.hotel = false;
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
                        this.totalcost -= ((row.Item2 / 100) * 13.55) * this.persons;
                        this.totalcost -= 7350 * this.persons;
                    }
                }
            }
            new Alfred("reservation", 20).Write();
            Checkout();
        }

        public void Checkout()
        {
            new System().Log("Checkout was called");
            DateTime dateTime = DateTime.UtcNow.Date;
            bool boolean = false;
            while (!boolean)
            {
                boolean = true;
                this.code = new System().RandomChar(3, 3);
                foreach (var row in this.data)
                {
                    if (this.code == row.code)
                    {
                        boolean = false;
                        break;
                    }

                }
            }
            this.reservation = new ReservationJson
            {
                id = this.id,
                code = this.code,
                memberID = new Settings().Member_id,
                amount = this.persons,
                date = this.Date,
                People = this.people,
                hunt = this.hunt,
                currentdate = dateTime.ToString("dd/MM/yyyy"),
                discount = this.members[new Settings().Member_ind].discount,
                hotel = this.hotel
            };
            this.currentres = new dynamic[]
            {
                this.Date,
                this.people,
                this.hotel,

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
                        this.totalcost += ((row.Item2 / 100) * 13.55) * this.persons;
                        this.totalcost += 7350 * this.persons;
                        break;
                    }
                }
            }
            if (this.hunt)
            {
                this.totalcost += 1900 * this.persons;
            }

            new Alfred("reservation", 21).Line();
            Console.Write($"{this.totalcost * this.discount} euro.");
            new Alfred("reservation", 23).Write(); // butler vraagt om aanpassingen aanpassingen
            var changeoptions = new[]
            {
                Tuple.Create<int, string, Action>(1, "Yes", () => changeReservation()),
                Tuple.Create<int, string, Action>(2, "No", () => finalize())
            };
            this.Menu(changeoptions);

        }

        public void changeReservation()
        {

            new Alfred("reservation", 27).Write();
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
                if (counter < 0 || counter > 3)
                {
                    Console.Clear();
                    new Alfred("reservation", 26).Write();
                    var changeoptions = new[]
                    {
                    Tuple.Create<int, string, Action>(1, "Yes", () => finalize()),
                    Tuple.Create<int, string, Action>(2, "No", () => changeReservation())
                    };
                    this.Menu(changeoptions);
                }
                else if (counter == 1)
                {
                    Console.Clear();
                    new Alfred("reservation", 25).Line();
                    Console.Write("[Date]: ");
                    Console.Write(this.Date);
                    new Alfred("reservation", 28).Write();
                    ConsoleKeyInfo tmp = Console.ReadKey();
                    if (tmp.Key == ConsoleKey.Enter)
                    {
                        DateAndTime();
                    }
                }
                else if (counter == 2)
                {
                    Console.Clear();
                    Console.Write("\n");
                    new Alfred("reservation", 25).Line();
                    Console.Write("[Guests]: \n");
                    foreach (var row in this.people)
                    {
                        Console.WriteLine(row.name);
                        Console.WriteLine(row.menu);
                        Console.WriteLine(row.allergies);
                        Console.WriteLine(row.kcal);
                    }
                    new Alfred("reservation", 30).Write();
                    ConsoleKeyInfo tmp = Console.ReadKey();
                    if (tmp.Key == ConsoleKey.Enter)
                    {
                        changeMember();
                    }
                }
                else if (counter == 3)
                {
                    Console.Clear();
                    Console.Write("\n");
                    new Alfred("reservation", 25).Line();
                    Console.Write("[Hotel]: ");
                    Console.Write(this.hotel);
                    ConsoleKeyInfo tmp = Console.ReadKey();
                    if (tmp.Key == ConsoleKey.Enter)
                    {
                        BookHotel();
                    }
                }
            }
        }
        public void changeMember()
        {
            bool complete = false;
            bool comp = false;
            bool sheesh = false;
            Tuple<int, string, int>[] optionsmenu =
            {
                    Tuple.Create(1, "Vega", 2500),
                    Tuple.Create(2, "Impala", 3300),
                    Tuple.Create(3, "Fish", 1700),
            };

            new Alfred("reservation", 29).Write();

            while (!comp)
            {

                comp = true;
                while (!sheesh)
                {
                    int counter = 1;
                    foreach (var row in this.people)
                    {
                        Console.WriteLine($"[{counter}] {row.name}\n {row.menu}\n {row.allergies}\n {row.kcal}\n");
                        counter++;
                    }
                    new Alfred("reservation", 31).Line();
                    try
                    {
                        int inputTmp = Convert.ToInt32(Console.ReadLine());
                        if (inputTmp > this.people.Count || inputTmp < 1)
                        {
                            new Alfred("error", 2).Write();
                        }
                        else
                        {
                            dynamic tmpmenu = this.reservation.People[inputTmp - 1].menu;
                            for (int i = 0; i < optionsmenu.Length; i++)
                            {
                                if (tmpmenu == optionsmenu[i].Item2)
                                {
                                    this.totalcost -= optionsmenu[i].Item3;
                                }
                            }
                            new Alfred("reservation", 7).Line();
                            string fName = Console.ReadLine();
                            new Alfred("reservation", 8).Line();
                            string SName = Console.ReadLine();

                            while (!complete)
                            {
                                foreach (var i in optionsmenu)
                                {
                                    Console.WriteLine($"[{i.Item1}] {i.Item2}");
                                }
                                new Alfred("reservation", 9).Line();
                                try
                                {
                                    int menuTmp = Convert.ToInt32(Console.ReadLine());
                                    menu = optionsmenu[menuTmp - 1].Item2;
                                    this.totalcost += optionsmenu[menuTmp - 1].Item3;
                                    if (menuTmp == 1 || menuTmp == 3)
                                    {
                                        complete = true;
                                    }
                                    else if (menuTmp == 2)
                                    {
                                        complete = true;
                                        this.countImpala++;
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

                            this.guests = new People
                            {
                                name = $"{fName} {SName}",
                                menu = menu,
                                allergies = Allergie,
                                kcal = Kcal
                            };
                            this.people[inputTmp - 1] = this.guests;
                            new Alfred("reservation", 32).Write();
                            ConsoleKeyInfo temp = Console.ReadKey();
                            if (temp.Key == ConsoleKey.N)
                            {
                                new Alfred("reservation", 33).Write();
                                sheesh = true;
                            }
                        }
                    }
                    catch
                    {
                        new Alfred("error", 0).Write();
                    }
                }
            }
        }
        public void finalize()
        {
            Console.Write($"{this.totalcost * this.discount} euro.");
            new Alfred("reservation", 22).Write();
            var final = new[]
            {
                Tuple.Create<int, string, Action>(1, "Yes", () => email()),
                Tuple.Create<int, string, Action>(2, "No", () => changeReservation())
            };
            this.Menu(final);
        }

        public void email()
        {
            this.data.Add(this.reservation);
            new Json("reservation.json").Write(this.data);
            new Alfred("reservation", 34).Write();
            dynamic customer = this.members[new Settings().Member_ind];

            string content = "";
            content += $"<p>Thank you for your reservation!</p><br>";
            content += $"<h2>Your reservation code is: {this.code}</h2>";
            content += $"<p>You can cancel your reservation before 7 days from the date of this email was sent</p>";
            content += $"<p>Creditcard number: {customer.creditcard}</p>";

            new System().SendMail(customer.email, "Reservation conformation", content);

            new Alfred("reservation", 35).Write();
            ConsoleKeyInfo tmp = Console.ReadKey();
            if (tmp.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                new Customer();
            }
            else
            {
                new Alfred("reservation", 36).Write();
            }
        }
    }
}

