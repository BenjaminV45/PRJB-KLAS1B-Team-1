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
                string resCode = Console.ReadLine();
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

        }

        public void DeleteReservation()
        {
            Console.WriteLine(this.reservation[1].code);
            this.reservation.RemoveAt(1);
            new Json("reservation.json").Write(this.reservation);
        }
    }
}
