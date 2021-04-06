using System;
using System.Reflection;
using System.Linq;

namespace ProjectB
{
    class Switch : Settings
    {
        public void Create()
        {
            Reservartion create = new Reservartion();
            create.Constructer();
            // Console.WriteLine("Call create reservation class in this method");
            // Console.ReadLine();

        }
        public void Cancel()
        {
            Console.WriteLine("Call cancel reservation class in this method");
            Console.ReadLine();
        }






        public void Chef()
        {
            Console.WriteLine(getText(new object[] { "chef", 0 }));

            Tuple<int, string, string>[] Chef_options =
                {
                // Parametes are as follow [key] 1,2,3 etc [text parameters json file] [method name in switch file]
                Tuple.Create(1, getText(new object[] { "chef", 2 }), "Ontbijt"),
                Tuple.Create(2, getText(new object[] { "chef", 3 }), "Lunch"),
                Tuple.Create(3, getText(new object[] { "chef", 4 }), "Diner"),
                Tuple.Create(4, getText(new object[] { "chef", 5 }), "Alles")
                };

            foreach (Tuple<int, string, string> row in Chef_options)
            {
                Console.WriteLine(row.Item1 + " | " + row.Item2);
            }

            int input;
            bool complete = false;
            while (!complete)
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                    if (input <= Chef_options.Count() && input > 0)
                    {
                        Type type = typeof(Get_menu);
                        MethodInfo method = type.GetMethod(Chef_options[input - 1].Item3);
                        Get_menu c = new Get_menu();
                        string result = (string)method.Invoke(c, null);
                        complete = true;
                    }
                    else
                    {
                        Console.WriteLine(getText(new object[] { "error", 0 }));
                    }
                }
                catch
                {
                    Console.WriteLine(getText(new object[] { "error", 1 }));
                }

        }


        public void lr()
        {
            LeaveReview review = new LeaveReview();
            review.Construct();
        }

        public void Check_member()
        {
            Console.WriteLine("[A] Inloggen via een membership code");
            Console.WriteLine("[B] Member worden");
        }
    }
}