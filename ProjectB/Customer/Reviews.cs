using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectB
{
    class leaveReview
    {
        public string resCode;
        public string review;
        public int sterren;
        public int resid;
        public bool korting;
        public bool woorden;
        public leaveReview()
        {
            this.korting = false;
            string[] kortingarr = new string[] { "fantastisch", "geweldig", "heerlijk", "prachtig" };
            this.woorden = false;
            string[] woordenarr = new string[] { "mieters", "super", "vet", "top" };

            dynamic reviews = new Json("reviews.json").Read();
            dynamic reservation = new Json("reservation.json").Read();

            bool tmp = false;
            while (!tmp)
            {
                new Alfred("leaveReview", 0).Write();
                this.resCode = Console.ReadLine();
                foreach (var row in reservation)
                {
                    if (this.resCode == row.code)
                    {
                        this.resid = row.id;
                        tmp = true;
                        Console.WriteLine(resid);
                        break;
                    }
                }
                if (!tmp)
                {
                    Console.WriteLine("Wrong input");
                }
            }

            new Alfred("leaveReview", 1).Write();
            this.review = Console.ReadLine();

            tmp = false;
            while (!tmp)
            {
                if (this.review.Length > 512)
                {
                    new Alfred("leaveReview", 2).Write();
                    this.review = Console.ReadLine();
                }
                else 
                    tmp = true;
            }

            foreach (var row in kortingarr)
            {
                if (this.review.ToLower().Contains(row))
                {
                    this.korting = true;
                }
            }
            Console.WriteLine(this.korting);
            foreach (var row in woordenarr)
            {
                if (this.review.ToLower().Contains(row))
                {
                    this.woorden = true;
                }
            }
            Console.WriteLine(this.woorden);

            new Alfred("leaveReview", 3).Write();
            this.sterren = Convert.ToInt32(Console.ReadLine());

            var newreview = new ReviewsJson
            {
                id = reviews[reviews.Count - 1].id + 1,
                resid = this.resid,
                memberid = new Settings().Member_id,
                reviewtxt = this.review,
                rating = this.sterren
            };

            reviews.Add(newreview);
            new Json("reviews.json").Write(reviews);

        }
    }
}
