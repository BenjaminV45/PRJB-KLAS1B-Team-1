using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectB
{
    class Reservation
    {

        public int persons;
        public dynamic data;

        public Reservation()
        {
            this.data = new Json("reservation.json").Read();
            HowManyPersons();
        }

        public void HowManyPersons() {
            new Alfred("reservation", 0).Write();
            bool complete = false;
            while (!complete) {
                try
                {
                    this.persons = Convert.ToInt32(Console.ReadLine());
                    complete = true;
                }
                catch
                {
                    new Alfred("error", 0).Write();
                }
            }
        }
    }
}
