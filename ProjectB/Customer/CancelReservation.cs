using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectB
{
    class CancelReservation : Option
    {
        public dynamic reservation;
        public DateTime dateTime;
        public DateTime reservationDate;
        public int reservation_index;
        public string resCode;

        public CancelReservation()
        {
            this.reservation = new Json("reservation.json").Read();
            this.dateTime = DateTime.Today;
            this.reservation_index = 0;
            ReservationCode();
        }

        public void ReservationCode()
        {
            bool comp = false;
            while (!comp)
            {
                new Alfred("cancel", 0).Write();
                resCode = Console.ReadLine();
                foreach (var row in this.reservation)
                {
                    if (row.code == resCode)
                    {
                        TimeSpan diff = this.dateTime - DateTime.Parse(row.currentdate);
                        if (diff.TotalDays < 6)
                        {
                            Console.WriteLine(diff.TotalDays);
                            comp = true;
                            
                        }
                        else
                        {
                            new Alfred("cancel", 2).Write();
                        }
                        break;
                    }
                    this.reservation_index++;
                }
                if (!comp)
                {
                    new Alfred("cancel", 1).Write();
                } else
                {
                    var options = new[]
                           {
                            Tuple.Create<int, string, Action>(1, new Alfred("cancel",3).Option(), () => DeleteReservation()),
                            Tuple.Create<int, string, Action>(2, new Alfred("cancel",4).Option(), () => DeleteMember())
                            };
                    this.Menu(options);
                }
            }
        }

        public void DeleteMember()
        {
            bool comp = false;
            new Alfred("cancel", 5).Write();

            while (!comp)
            {
                foreach (var row in this.reservation)
                {
                    if (row.code == resCode)
                    {
                        int index = 0;
                        comp = true;
                        bool shees = false;
                        Tuple<int, string>[] guests = new Tuple<int, string>[50];
                        foreach (var col in row.People)
                        {
                            guests[index] = Tuple.Create(index + 1, col.name);
                            index++;
                        }
                        while (!shees)
                        {
                            for (int i = 0; i < guests.Length; i++)
                            {
                                Console.WriteLine($"[{guests[i].Item1}] {guests[i].Item2}");
                            }                                                    
                            string inputTmp = Console.ReadLine();
                            int input = Convert.ToInt32(inputTmp);
                        }
                    }
                }
            }
            

        }

        public void DeleteReservation()
        {
            this.reservation.RemoveAt(1);
            new Json("reservation.json").Write(this.reservation);
        }
    }
}
