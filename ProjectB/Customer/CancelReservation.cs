using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectB
{
    class CancelReservation : Option
    {
        public dynamic reservation;
        public DateTime dateTime;
        public int reservation_index;
        public string resCode;

        public CancelReservation()
        {
            this.reservation = new Json("reservation.json").Read();
            this.dateTime = DateTime.Today;
            this.reservation_index = 0;
            ReservationCode();
        }

        public void ReservationCode(string code = null)
        {
            bool comp = false;
            while (!comp)
            {
                if (code != null)
                {

                    resCode = code;
                } else
                {
                    new Alfred("cancel", 0).Write();
                    resCode = Console.ReadLine();
                }
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
            bool sheesh = false;
            new Alfred("cancel", 4).Write();
            string code = null;

            while (!comp)
            {
                foreach (var row in this.reservation)
                {
                    if (row.code == resCode)
                    {
                        code = row.code;
                        if (row.People.Count < 2)
                        {
                            comp = true;
                            new Alfred("cancel", 6).Write();
                            break;
                        }
                        else
                        {
                            comp = true;
                            while (!sheesh)
                            {
                                int counter = 1;
                                foreach (var col in row.People)
                                {
                                    Console.WriteLine($"[{counter}] {col.name}");
                                    counter++;
                                }
                                new Alfred("cancel", 5).Line();
                                try
                                {                                   
                                    int inputTmp = Convert.ToInt32(Console.ReadLine());
                                    if (inputTmp > row.People.Count || inputTmp < 1)
                                    {
                                        new Alfred("error", 2).Write();
                                    }
                                    else
                                    {
                                        this.reservation[this.reservation_index].People.RemoveAt(inputTmp - 1);
                                        if (row.People.Count < 2)
                                        {
                                            new Alfred("cancel", 6).Write();
                                            sheesh = true;
                                            comp = true;
                                            break;

                                        }
                                        else
                                        {
                                            new Alfred("cancel", 8).Write();
                                            if (Console.ReadKey().KeyChar == 'N' || Console.ReadKey().KeyChar == 'n')
                                            {
                                                new Alfred("cancel", 7).Write();
                                                sheesh = true;
                                                comp = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    new Alfred("error", 0).Write();
                                }
                            }
                        }
                    }
                }
            }
            if (comp && sheesh)
            {
                new Json("reservation.json").Write(this.reservation);
            }  else
            {
                this.ReservationCode(code);
            } 

        }

        public void DeleteReservation()
        {
            this.reservation.RemoveAt(this.reservation_index);
            new Json("reservation.json").Write(this.reservation);
        }
    }
}
