using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Net.Mail;
using System.Net;

namespace ProjectB
{
    class System
    {
        public static string SYSTEM_NAME = "La Mouette";

        public void SendMail(string email, string title, string content)
        {
            content += "<p>Kind Regards,<br>Albert</p>";
            MailMessage mail = new MailMessage(email, email, $"{SYSTEM_NAME} - {title}", content);
            mail.IsBodyHtml = true;

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("team1.projectb@gmail.com", "V%cajS^JF[*tY2Sb"),
                EnableSsl = true
            };

            client.Send(mail);
        }
        public string RandomChar(int letters = 0, int numbers = 0)
        {
            char[] a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] n = "123456789".ToCharArray();
            Random r = new Random();

            string str = "";

            for (int i = letters; i > 0; i--)
            {

                r.Next(a.Length);
                str += a[i];
            }

            for (int i = numbers; i > 0; i--)
            {
                r.Next(n.Length);
                str += n[i];
            }

            return new string(str.ToCharArray().OrderBy(s => (r.Next(2) % 2) == 0).ToArray());
        }
        public void Log(string des)
        {
            string content = "";

            if (des == "System is running")
            {
                content += $"\n";
            }

            content += $"[{string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)}] {des} {Environment.NewLine}";
            File.AppendAllText(@"..\..\..\Storage\chatlog.txt", content);
        }
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
            if (this.file == "reservation.json") return JsonSerializer.Deserialize<List<ReservationJson>>(json);
            if (this.file == "members.json") return JsonSerializer.Deserialize<List<MembersJson>>(json);
            if (this.file == "reviews.json") return JsonSerializer.Deserialize<List<ReviewsJson>>(json);
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
            new System().Log($"Alfred is being called. Data: {row} {col}");
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
            new System().Log("Calling menu options");
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
                    //inputTmp = Console.ReadKey().KeyChar.ToString();
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
            new System().Log("Calling chef class");
            Console.Write("Input date: ");
            string date = Console.ReadLine();
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
        public dynamic settings;
        public Customer()
        {
            this.customer = new Json("members.json").Read();
            this.settings = new Settings().File;
            var options = new[]
            {
                Tuple.Create<int, string, Action>(1, "Login with membership code", () => this.Login()),
                Tuple.Create<int, string, Action>(2, "Become a member", () => this.Register())
            };

            this.Menu(options);

            var optionss = new[]
            {
                Tuple.Create<int, string, Action>(1, new Alfred("option", 2).Option(), () => this.Membership()),
                Tuple.Create<int, string, Action>(2, new Alfred("option", 1).Option(), () => new Reservation()),
                Tuple.Create<int, string, Action>(3, new Alfred("option", 3).Option(), () => this.Lookup()),
                Tuple.Create<int, string, Action>(4, new Alfred("option", 4).Option(), () => new leaveReview())
            };

            this.Menu(optionss);
        }

        public void Login()
        {
            bool boolean = false;
            new System().Log("Calling login method");
            while (!boolean)
            {
                Console.Write("[Albert] I would like to know your membership code: ");
                string input = Console.ReadLine();
                foreach (var row in this.customer)
                {
                    if (input == row.code)
                    {
                        boolean = true;
                        this.settings.member_id = row.id;
                        new Settings().Update(this.settings);

                        this.settings.language = (row.continent == "Europa" || row.continent == "Africa" ? "NL" : "EN");
                        new Settings().Update(this.settings);
                        Console.Clear();
                        break;
                    }
                }
            }
        }

        public void Register()

        {
            new System().Log("Calling register method");
            string[] continents = { "Europa", "Africa", "North-america", "South-america", "Asia", "Australia" };
            bool boolean = false;
            string continent = "";
            foreach (string row in continents)
            {
                Console.WriteLine($"[+] {row}");
            }
            while (!boolean)
            {

                Console.Write("\n[Alfred] I would like to know your continent: ");

                continent = Console.ReadLine();
                if (continents.Contains(continent))
                {

                    this.settings.language = (continent == "Europa" || continent == "Africa" ? "NL" : "EN");
                    new Settings().Update(this.settings);

                    Console.Clear();

                    new Alfred("create-customer", 0).Write();
                    boolean = true;


                }
                else
                {
                    Console.WriteLine("[Alfred] I have no clue what continet that is! Try again :)");
                }
            }

            boolean = false;
            string email = "";
            while (!boolean)
            {
                new Alfred("create-customer", 1).Line();
                email = Console.ReadLine();
                try
                {
                    MailAddress addr = new MailAddress(email);
                    foreach (var row in this.customer)
                    {
                        if (email == row.email)
                        {
                            new Alfred("create-customer", 3).Write();
                            break;
                        }
                        boolean = true;
                    }
                }
                catch (FormatException)
                {
                    new Alfred("create-customer", 2).Write();
                }
            }

            Console.Clear();
            new Alfred("create-customer", 4).Line();
            string firstname = Console.ReadLine();

            new Alfred("create-customer", 5).Line();
            string lastname = Console.ReadLine();

            Console.Clear();
            boolean = false;
            string creditcard = "";

            while (!boolean)
            {
                new Alfred("create-customer", 6).Line();
                creditcard = Console.ReadLine();
                if (creditcard.Length != 19)
                {
                    new Alfred("create-customer", 7).Write();
                }
                else
                {
                    boolean = true;
                }
            }

            string code = "";
            boolean = false;
            while (!boolean)
            {
                boolean = true;
                code = new System().RandomChar(3, 3);
                foreach (var row in this.customer)
                {
                    if (code == row.code)
                    {
                        boolean = false;
                    }
                }
            }



            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();

            new System().SendMail(email, new Alfred("create-customer", 10).Option(), $"{new Alfred("create-customer", 11).Option()}{myuuidAsString}");
            string secretKey = null;
            boolean = false;
            new Alfred("create-customer", 8).Line();
            while (!boolean)
            {
                secretKey = Console.ReadLine();
                if (myuuidAsString == secretKey)
                {
                    boolean = true;
                }
                else
                {
                    new Alfred("create-customer", 9).Line();
                }
            }

            string content = "";
            content += $"<p>{firstname} {lastname}, you're now member of our beautiful restaurant!</p><br>";
            content += "<h2>Your account credentials</h2>";
            content += $"<p>Membership code: {code}</p>";
            content += $"<p>Email: {email}</p>";
            content += $"<p>Creditcard number: {creditcard}</p>";
            content += $"<p>Continent: {continent}</p>";
            content += $"<p>Rank: Bronze</p>";

            new System().SendMail(email, "Account credentials", content);
            int id = this.customer[this.customer.Count - 1].id + 1;
            var person = new MembersJson
            {
                id = id,
                code = code,
                email = email,
                firstname = firstname,
                lastname = lastname,
                creditcard = creditcard,
                continent = continent,
                rank = "Bronze"
            };

            new Alfred("create-customer", 12).Write();

            this.customer.Add(person);
            new Json("members.json").Write(this.customer);

            this.settings.member_id = id;
            new Settings().Update(this.settings);
        }

        public void Lookup()
        {

            bool boolean = false;
            while (!boolean)
            {
                new Alfred("lookup", 0).Line();
                string input = Console.ReadLine();
                foreach (var row in this.customer)
                {
                    string name = row.firstname + " " + row.lastname;
                    if (name.ToLower().Contains(input.ToLower()))
                    {
                        Console.WriteLine(name);
                        Console.WriteLine("Press [ESC] to stop or any other [Key] to keep going!");
                        if (Console.ReadKey().Key == ConsoleKey.Escape)
                        {
                            boolean = true;
                            break;
                        }
                        Console.Clear();
                        break;
                    }
                }
            }
        }
        public void Membership()
        {
            new Alfred("membership", 0).Write();
            dynamic Reservation = new Json("reservation.json").Read();
            string content = "Nee";
            foreach (var row in this.customer)
            {
                if (this.settings.member_id == row.id)
                {
                    Console.WriteLine($"\nCode: {row.code}");
                    Console.WriteLine($"Name: {row.firstname} {row.lastname}");
                    Console.WriteLine($"Email: {row.email}");
                    Console.WriteLine($"Rank: {row.rank}");
                    Console.WriteLine($"Continent: {row.continent}");
                    Console.WriteLine($"Creditcard: {row.creditcard}");
                    foreach(var col in Reservation)
                    {
                        if (row.id == col.memberID)
                        {
                            if(col.hunt == true)
                            {
                                content = "Ja";
                                break;
                            }

                        }
                    }
                    Console.WriteLine($"Op impala gejaagd: {content}");
                }
            }
        }
    }
    class Start : Option
    {

        public Start()
        {
            new System().Log("Starting the program");
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
            new System().Log("System is running");
            new Start();
        }
    }
}

