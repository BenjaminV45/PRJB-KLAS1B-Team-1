using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Globalization;

namespace ProjectB
{
    public class Reservartion : Settings
    {
        public int persons;
        public void Constructer()
        {
            Console.WriteLine(getText(new object[] { "reservation", 0 }));

            
            bool complete = false;
            while (!complete)
                try
                {
                   
                    this.persons = Convert.ToInt32(Console.ReadLine());
                    complete = true;
                }
                catch
                {
                    Console.WriteLine(getText(new object[] { "error", 1 }));
                }

            Console.WriteLine(getText(new object[] { "reservation", 1 }));
            DateTime dDate;
            complete = false;

            string json = File.ReadAllText(@"..\..\..\Data\reservation.json");

            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(json);

            DataTable dataTable = dataSet.Tables["reservation"];


            while (!complete)
            {
                string inputString = Console.ReadLine();

                if (DateTime.TryParse(inputString, out dDate))
                {
                    String.Format("{0:d/MM/yyyy}", dDate);

                    if (dDate < DateTime.Today)
                    {
                        Console.WriteLine(getText(new object[] { "reservation", 3 }));
                    }
                    else
                    {
                        int tCount = this.persons;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (DateTime.Parse((string)row["date"]) == dDate)
                            {
                                tCount += Convert.ToInt32(row["amount"]);
                            }
                        }

                        if (tCount <= (SETTINGS_TABLES * 2))
                        {
                            complete = true;
                        }
                        else
                        {
                            Console.WriteLine(getText(new object[] { "reservation", 4 }));
                        }
                        
                        Console.WriteLine(tCount);
                    }
                }
                else
                {
                    Console.WriteLine(getText(new object[] { "reservation", 2 })); // <-- Control flow goes here
                }
            }            
        }
    }
}
