using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ProjectB
{
    class Program
    {

        static void Main(string[] args)
        {

            Start Start = new Start();
            Start.Construct();

            Options Options = new Options();
            Options.options();
        }
    }
}
