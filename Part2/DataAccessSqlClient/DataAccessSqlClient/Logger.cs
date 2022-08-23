
using System;
/// <summary>
/// Contains functions for formatting the models into strings.
/// </summary>
public static class Logger
{
	public static void LogCustomer(Customer customer)
	{
		string message = $"Id: {customer.CustomerId} | Name: {customer.FirstName} {customer.LastName} | Country: {customer.Country} | Postal code: {customer.PostalCode}| Phone: {customer.Phone}  | Email: {customer.Email}";
		Console.WriteLine(message);
	}

	
	public static void LogCustomerSpender(CustomerSpender spender)
	{
		string message = $"Customer Id: {spender.CustomerId} | Total Spending: {spender.TotalSpending}";
		Console.WriteLine(message);
	}
	public static void LogCustomerGenre(CustomerGenre genre)
	{
		string message = $"GenreId: {genre.GenreId} | Name: {genre.Name} | Count: {genre.Count}";
		Console.WriteLine(message);
	}
	public static void LogCountryCounter(CustomerCountry country)
	{
		string message = $"Country name: {country.CountryName} | Count: {country.CountryCount}";
		Console.WriteLine(message);
	}

}