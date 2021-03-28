using System;
using System.Reflection;
using System.Linq;
namespace ProjectB
{
	class Start : Settings
	{
        public void start()
        {
            Console.WriteLine(getText(new object[] { "introduction", 0 }));
            Tuple<int, string, string>[] options =
            {
                // Parametes are as follow [key] 1,2,3 etc [text parameters json file] [method name in switch file]
                Tuple.Create(1, getText(new object[] { "options", 2 }), "Create"),
                Tuple.Create(2, getText(new object[] { "options", 3 }), "Cancel"),
            };

            foreach (Tuple<int, string, string> row in options)
            {
                Console.WriteLine(row.Item1 + " | " + row.Item2);
            }
            string inputTmp;
            int input;
            bool complete = false;
            while (!complete)
                try
                {
                    inputTmp = Console.ReadLine();
                    if(inputTmp != "code")
                    {
                       input = Convert.ToInt32(inputTmp);
                        if (input <= options.Count() && input > 0)
                        {
                            complete = true;
                            Type type = typeof(Switch);
                            MethodInfo method = type.GetMethod(options[input - 1].Item3);
                            Switch c = new Switch();
                            string result = (string)method.Invoke(c, null);
                        }
                        else
                        {
                            Console.WriteLine(getText(new object[] { "error", 0 }));
                        }
                    }
                    else
                    {
                        complete = true;
                        Console.WriteLine("INLOGGEN");
                        Switch c = new Switch();
                        c.Chef();

                    }
                }
                catch
                {
                    Console.WriteLine(getText(new object[] { "error", 1 }));
                }
        }
    }
}