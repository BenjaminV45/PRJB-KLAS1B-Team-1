using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ProjectB
{
    public class Options : Settings
    {

    public void options()
    {
        int input;
        Tuple<int, string, string>[] options =
        {
                Tuple.Create(1, getLanguage(new object[] { "options", 0 }), "Create"),
                Tuple.Create(2, getLanguage(new object[] { "options", 1 }), "Cancel"),
            };


        foreach (Tuple<int, string, string> row in options)
        {
            Console.WriteLine(row.Item1 + " | " + row.Item2);
        }

        try
        {
            input = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(input);
        }
        catch
        {

        }

    }
}
}