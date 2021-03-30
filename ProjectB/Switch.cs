using System;
namespace ProjectB
{
	class Switch
	{
		public void Create()
		{
			Reservartion create = new Reservartion();
			create.Constructer(); 
			// Console.WriteLine("Call create reservation class in this method");
			// Console.ReadLine();

		}
		public void Cancel()
		{
			Console.WriteLine("Call cancel reservation class in this method");
			Console.ReadLine();
		}
		public void Chef()
		{
			Console.WriteLine("Call chef class in this method");
			Console.ReadLine();
		}
		
		public void lr()
        {
			LeaveReview review = new LeaveReview();
			review.Construct();
        }

		public void Check_member()
        {
			Console.WriteLine("[A] Inloggen via een membership code");
			Console.WriteLine("[B] Member worden");
		}
	}
}