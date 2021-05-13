using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectB
{
    class CancelReservation
    {
        public dynamic reservation;
        public DateTime dateTime;
        public DateTime reservationDate;
        public CancelReservation()
        {
            this.reservation = new Json("reservation.json").Read();
            DateTime dateTime = DateTime.UtcNow.Date;
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
                        getDate();
                        comp = true;
                    }
                    else
                    {
                        new Alfred("cancel", 1).Write();
                    }
                }
            }
        }

        public void getDate()
        {
            bool comp = false;
            while (!comp)
            {
                foreach (var row in this.reservation)
                {
                    if (dateTime > DateTime.Parse(row.currentdate))
                    {
                        Console.WriteLine("Shheeeessh");
                    }
                    else
                    {
                        Console.WriteLine("hoi");
                    }
                }
            }
        }

    }
}
