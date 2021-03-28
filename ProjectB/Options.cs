using System;
// using System.IO;
// using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Linq;
namespace ProjectB
{
    public class Options : Settings
    {
        public void options()
        {

            Tuple<int, string, string>[] options =
            {
                // Parametes are as follow [key] 1,2,3 etc [text parameters json file] [method name in switch file]
                Tuple.Create(1, getText(new object[] { "options", 0 }), "Create"),
                Tuple.Create(2, getText(new object[] { "options", 1 }), "Cancel"),
                Tuple.Create(3, getText(new object[] { "options", 4 }), "lr"),

            };

            foreach (Tuple<int, string, string> row in options)
            {
                Console.WriteLine(row.Item1 + " | " + row.Item2);
            }
            int input;
            bool complete = false;
            while (!complete)
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                    if (input <= options.Count() && input > 0)
                    {
                        Type type = typeof(Switch);
                        MethodInfo method = type.GetMethod(options[input - 1].Item3);
                        Switch c = new Switch();
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
    }
}