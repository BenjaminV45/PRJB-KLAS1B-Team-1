﻿using System;
namespace ProjectB
{
	class Switch
	{
		public void Create()
		{
			// Reservartion create = new Reservartion;
			// create.contruct
			Console.WriteLine("Call create reservation class in this method");
			Console.ReadLine();
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
	}
}