using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectB
{
    class Reservation
    {
        public dynamic data;
        public Reservation()
        {
            this.data = new Json("reservation.json").Read();
            foreach (var row in this.data.ReservationJson)
            {
                Console.WriteLine(row.id);
            }
        }
    }
}
