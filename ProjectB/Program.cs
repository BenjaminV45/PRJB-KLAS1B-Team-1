using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Mail;

namespace ProjectB
{
    class System
    {

    }
    class Json
    {
        private string file;
        public Json(string file)
        {
            this.file = file;
        }
        public dynamic Read()
        {

            string json = File.ReadAllText(@"..\..\..\Storage\" + this.file);
            if (this.file == "settings.json") return JsonSerializer.Deserialize<SettingsJson>(json);
            if (this.file == "languages.json") return JsonSerializer.Deserialize<LanguageJson>(json);
            if (this.file == "reservation.json") return JsonSerializer.Deserialize<ReservationJson>(json);
            if (this.file == "members.json") return JsonSerializer.Deserialize<List<MembersJson>>(json);
            if (this.file == "menu.json") return JsonSerializer.Deserialize<MenuJson>(json);
            return null;
        }

        public void Write(dynamic data)
        {
            string json = JsonSerializer.Serialize(data);
            File.WriteAllText(@"..\..\..\Storage\" + this.file, json);
        }


    }

    public class Settings
    {
        public dynamic File = new Json("settings.json").Read();
        public string Language;
        public int Member_id;

        public Settings()
        {
            this.Language = this.File.language;
            this.Member_id = this.File.member_id;
        }

        public void Update(dynamic data)
        {
            new Json("settings.json").Write(data);
        }

    }
    public class Alfred : Settings
    {
        private string row;
        private int col;
        private dynamic data;
        public Alfred(string row, int col)
        {
            this.row = row;
            this.col = col;
            this.data = new Json("languages.json").Read();
        }
        public void Write()
        {
            Console.Write("\n[Alfred] ");
            if (this.Language == "NL") Console.Write(this.data.NL[this.row][this.col]);
            else Console.Write(this.data.EN[this.row][this.col]);
            Console.Write("\n");
        }
        public void Line()
        {
            Console.Write("[Alfred] ");
            if (this.Language == "NL") Console.Write(this.data.NL[this.row][this.col]);
            else Console.Write(this.data.EN[this.row][this.col]);
        }

        public string Option()
        {
            if (this.Language == "NL") return this.data.NL[this.row][this.col];
            else return this.data.EN[this.row][this.col];
        }
    }


    class Option : System
    {
        public void Menu(Tuple<int, string, Action>[] options)
        {
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
                    if (inputTmp != "sesame")
                    {
                        input = Convert.ToInt32(inputTmp);
                        if (input > options.Length || input < 1)
                        {
                            Console.WriteLine("** " + input + " does not exist. **");
                        }
                        else
                        {
                            Console.Clear();
                            tmp = true;
                            options[input - 1].Item3();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        new Chef();
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
        public dynamic customer;

        public Customer()
        {
            this.customer = new Json("members.json").Read();

            var options = new[]
            {
                            Tuple.Create<int, string, Action>(1, "Login with membership code", () => this.Login()),
                            Tuple.Create<int, string, Action>(2, "Become a member", () => this.Register())
                        };

            this.Menu(options);

            var optionss = new[]
{
                            Tuple.Create<int, string, Action>(1, "Make a reservation", () => new Reservation()),
                        };

            this.Menu(optionss);
        }



        public void Login()
        {
            bool boolean = false;

            while (!boolean)
            {
                Console.Write("Input membership code: ");
                string input = Console.ReadLine();
                foreach (var row in this.customer)
                {
                    if (input == row.code)
                    {
                        boolean = true;
                        dynamic Settings = new Settings().File;
                        Settings.member_id = row.id;
                        new Settings().Update(Settings);
                        break;
                    }
                }
            }
        }

        public void Register()

        {

            string[] continent = { "Europa", "Africa", "North-america", "South-america", "Asia", "Australia" };
            bool boolean = false;

            while (!boolean)
            {
                int count = 1;
                foreach (string row in continent)
                {
                    Console.WriteLine($"[{count++}] {row}");
                }
                Console.Write("\n[Alfred] I would like to know your continent: ");

                string input = Console.ReadLine();
                if (continent.Contains(input))
                {
                    dynamic Settings = new Settings().File;
                    Settings.language = (input == "Europa" || input == "Africa" ? "NL" : "EN");
                    new Settings().Update(Settings);

                    Console.Clear();

                    new Alfred("create-customer", 0).Write();
                    boolean = true;


                }
                else
                {
                    Console.WriteLine("[Alfred] I have no clue what continet that is! Try again :)\n");
                }
            }

            boolean = false;
            
            while (!boolean)
            {
                new Alfred("create-customer", 1).Line();
                string input = Console.ReadLine();
                try
                {
                    MailAddress addr = new MailAddress(input);
                    boolean = true;
                }
                catch (FormatException)
                {
                    new Alfred("create-customer", 2).Write();
                }
            }
            //var person = new MembersJson{
            //    id = 2,
            //    code = "ABC123",
            //    email = "benjaminvdberg@live.nl",
            //    firstname = "Benjamin",
            //    lastname = "van den Berg",
            //    creditcard = "",
            //    continent = "Europa",
            //    rank = "Bronze"
            //};

            //this.customer.Add(person);
            //Console.WriteLine(this.customer);
            //new Json("members.json").Write(this.customer);
        }
    }

    class Start : Option
    {

        public Start()
        {
            dynamic Settings = new Settings().File;
            Settings.language = "EN";
            Settings.member_id = 0;
            new Settings().Update(Settings);

            new Alfred("start", 0).Write();
            var options = new[]
            {
                Tuple.Create<int, string, Action>(1, new Alfred("option", 0).Option(), () => new GetMenu()),
                Tuple.Create<int, string, Action>(2, new Alfred("option", 1).Option(), () => new Customer())
            };
            this.Menu(options);
        }
    }



    class Program
    {
        static void Main(string[] args)
        {

            new Start();

        }
    }
}