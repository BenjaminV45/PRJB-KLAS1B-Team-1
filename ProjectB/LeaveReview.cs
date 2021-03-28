using System.IO;
using System.Text.Json;
using System;

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

            string loadedSerializedJsonFileStringText = File.ReadAllText("reviews.json");
            File.WriteAllText("reviews.json", JsonSerializer.Serialize(new { code = code, text = review, rating = sterren}));
            
        }
    }
}