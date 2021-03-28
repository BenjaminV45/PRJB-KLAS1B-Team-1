using System.Text.Json;
using System;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ProjectB
{

    public class LeaveReview : Settings
    {
        public void Construct()
        {
            Console.WriteLine(getText(new object[] { "leaveReview", 3 }));
            string rcode = Console.ReadLine();
            int code = Convert.ToInt32(rcode);

            Console.WriteLine(getText(new object[] { "leaveReview", 0 }));
            string review = Console.ReadLine();

            Console.WriteLine(getText(new object[] { "leaveReview", 1 }));
            string ster = Console.ReadLine();
            int sterren = Convert.ToInt32(ster);

            Console.WriteLine(getText(new object[] { "leaveReview", 2 }));

            //string test = File.ReadAllText("reviews.json");
            //JObject rss = JObject.Parse(test);
            //JObject channel = (JObject)rss["channel"];
            //Array item = (JArray)channel["review"];
            //item.Add("test");
            //string updateSettings = test.ToString();
            //File.WriteAllText("settings.json", updateSettings);

            // string loadedSerializedJsonFileStringText = File.ReadAllText("reviews.json");
            // File.WriteAllText("reviews.json", JsonSerializer.Serialize(new { code = code, text = review, rating = sterren}));

        }
    }
}