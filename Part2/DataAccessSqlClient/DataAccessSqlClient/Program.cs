using System;

namespace DataAccessSqlClient
{
	internal class Program
	{

		internal static void Main()
		{
			CustomerClientHelper sql = new();

			// 2.1 
			Console.WriteLine("***** 2.1 Read all Customers *******");
			var c = sql.ReadAllCustomers();
			foreach (var item in c)
			{
				Logger.LogCustomer(item);
			}
			Console.WriteLine("***** 2.1 *******");

			// 2.2
			Console.WriteLine("***** 2.2 Read a spesific Customer by Id *******");
			var t = sql.ReadCustomer(40);
			Logger.LogCustomer(t);
			Console.WriteLine("***** 2.2 *******");

			// 2.3
			Console.WriteLine("***** 2.3 Read a spesific Customer by name *******");
			var d = sql.ReadCustomer("Ladislav");
			Logger.LogCustomer(d);
			Console.WriteLine("***** 2.3 ******");


			// 2.4
			Console.WriteLine("***** 2.4 Return page of customers *******");
			var l = sql.ReadAllCustomers(15, 10);
			foreach (var item in l)
			{
				Logger.LogCustomer(item);
			}
			Console.WriteLine("***** 2.4*******");


			// 2.5
			Console.WriteLine("***** 2.5 Add a new customer *******");
			Customer ola = new(0, "Ola", "Nordmann", "Norway", "4313", null, "ola.nordmann@gmail.com");
			sql.CreateCustomer(ola);
			ola = sql.ReadCustomer("Ola");
			Logger.LogCustomer(ola);
			Console.WriteLine("***** 2.5 *******");

			// 2.6
			Console.WriteLine("***** 2.6 Update a customer *******");
			t = sql.ReadCustomer("Ola");
			Logger.LogCustomer(t);
			t.Country = "Sweeden";
			sql.UpdateCustomer(t);
			t = sql.ReadCustomer("Ola");
			Logger.LogCustomer(t);
			Console.WriteLine("***** 2.6 *******");

			// 2.7
			Console.WriteLine("***** 2.7 Customers in each country *******");
			var q = sql.NumberOfCustomersInEachCountry();
			foreach (var item in q)
			{
				Logger.LogCountryCounter(item);
			}
			Console.WriteLine("***** 2.7 *******");

			// 2.8
			Console.WriteLine("***** 2.8 Highest spenders *******");
			var o = sql.GetCustomersSortedBySpending();
			foreach (var item in o)
			{
				Logger.LogCustomerSpender(item);
			}
			Console.WriteLine("***** 2.8 *******");


			// 2.9
			
			t = sql.ReadCustomer(40);	// When running all code in Program.cs in sequense i get error "index was out of range".
										//Added this ReadCustomer as a quick fix.
			Console.WriteLine("*****2.9 Favorite Genre *******");
			var g = sql.GetCustomerFavouriteGenre(t.CustomerId); 
			foreach (var item in g)
			{
				Logger.LogCustomerGenre(item);
			}
			Console.WriteLine("***** 2.9 *******");
		}

	}
}


