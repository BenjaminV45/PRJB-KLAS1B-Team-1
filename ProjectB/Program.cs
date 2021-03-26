using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ProjectB
{
    class Program
    {

        static void Main(string[] args)
        {
            Introduction Intro = new Introduction();
            Intro.intro();

            Options Options = new Options();
            Options.options();
        }
    }
}
