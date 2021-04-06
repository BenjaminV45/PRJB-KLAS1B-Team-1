using System;
namespace ProjectB
{
	class Switch : Settings
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
			//	Console.WriteLine(getText(new object[] { "chef", 0 }));

			//	{ 
			//	Tuple<int, string>[] Chef_options =
			//		Tuple.Create(1, getText(new object[] { "chef", 1 }),
			//		Tuple.Create(2, getText(new object[] { "chef", 2 }),
			//		Tuple.Create(3, getText(new object[] { "chef", 3 }),
			//		Tuple.Create(4, getText(new object[] { "chef", 4 }),
			//		};

			//	foreach (Tuple<int, string> row in Chef_options)

			//		{
			//			Console.WriteLine(row.Item1 + " | " + row.Item2);
			//	}
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