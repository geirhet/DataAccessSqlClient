using DataAccessSqlClient.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

public class CustomerClientHelper : ICustomerRepository
{
	public const string DataSource = @"OCSPC178L\SQLEXPRESS";
	private const string Chinook = "Chinook";

	public static string GetConnectionString()
	{
		SqlConnectionStringBuilder builder = new()
		{
			DataSource = DataSource,
			InitialCatalog = Chinook,
			IntegratedSecurity = true,
			TrustServerCertificate = true
		};

		return builder.ConnectionString;
	}
	/// <summary>
	/// Get a list of all customers as Customer objects.
	/// If exceptions are produced, they are logged to the console instead of thrown.
	/// </summary>
	/// <returns>Returns a list of Customer objects.</returns>
	public List<Customer> ReadAllCustomers()
	{
		List<Customer> Customers = new();
		try
		{
			using SqlConnection connection = new(GetConnectionString());
			connection.Open();
			string sql = "SELECT * FROM Customer";

			using SqlCommand command = new(sql, connection);
			using SqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				Customers.Add(ReaderHelper.ReadCustomer(reader));
			}

		}
		catch (SqlException ex)
		{
			Console.WriteLine("Something went wrong in the db: " + ex.Message);

		}
		catch (Exception ex)
		{
			Console.WriteLine("Something went wrong: " + ex.Message);

		}


		return Customers;

	}
	/// <summary>
	/// Get a page of cusomers from the database, starting from offset and ending at offset+limit.
	/// Returns a list of Customer objects.
	/// If exceptions are produced, they are logged to the console instead of thrown.
	/// </summary>
	/// <param name="limit"></param>
	/// <param name="offset"></param>
	/// <returns>Returns a list of Customer objects.</returns>
	public List<Customer> ReadAllCustomers(int limit, int offset)
	{
		List<Customer> Customers = new();
		try
		{
			using SqlConnection connection = new(GetConnectionString());
			connection.Open();
			string sql = "SELECT * FROM Customer ORDER BY CustomerId OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY";

			using SqlCommand command = new(sql, connection);
			command.Parameters.Add("@limit", SqlDbType.Int);
			command.Parameters["@limit"].Value = limit;
			command.Parameters.Add("@offset", SqlDbType.Int);
			command.Parameters["@offset"].Value = offset;
			using SqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				Customers.Add(ReaderHelper.ReadCustomer(reader));
			}

		}
		catch (SqlException ex)
		{
			Console.WriteLine("Something went wrong in the db: " + ex.Message);

		}
		catch (Exception ex)
		{
			Console.WriteLine("Something went wrong: " + ex.Message);

		}


		return Customers;

	}
	/// <summary>
	/// Counts the number of customers in each country in the database.
	/// Returns a list of CustomerCountry, where each CustomerCountry contains a name and a corresponding customer count.
	/// If exceptions are produced, they are logged to the console instead of thrown.
	/// </summary>
	/// <returns>Returns a list of CustomerCountry objects.</returns>
	public List<CustomerCountry> NumberOfCustomersInEachCountry()
	{
		List<CustomerCountry> countries = new();
		try
		{
			using SqlConnection connection = new(GetConnectionString());
			connection.Open();
			string sql = "SELECT COUNT(CustomerId), Country FROM Customer GROUP BY Country ORDER BY COUNT(CustomerID) DESC;";

			using SqlCommand command = new(sql, connection);

			using SqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				countries.Add(ReaderHelper.ReadCustomerCountry(reader));
				
			}

		}
		catch (SqlException ex)
		{
			Console.WriteLine("Something went wrong in the db: " + ex.Message);

		}
		catch (Exception ex)
		{
			Console.WriteLine("Something went wrong: " + ex.Message);

		}

		return countries;
	}
	/// <summary>
	/// Given a customer Id, retrieve a specific customer from the database with the same Id.
	/// If exceptions are produced, they are logged to the console instead of thrown.
	/// </summary>
	/// <param name="Id"></param>
	/// <returns>Returns the requested customer object, otherwise null.</returns>
	public Customer? ReadCustomer(int Id)
	{
		try
		{
			using SqlConnection connection = new(GetConnectionString());
			connection.Open();
			string sql = "SELECT * FROM Customer WHERE CustomerId = @Id";

			using SqlCommand command = new(sql, connection);
			command.Parameters.Add("@Id", SqlDbType.Int);
			command.Parameters["@Id"].Value = Id;
			using SqlDataReader reader = command.ExecuteReader();
			reader.Read();

			return ReaderHelper.ReadCustomer(reader);

		}
		catch (SqlException ex)
		{
			Console.WriteLine("Something went wrong in the db: " + ex.Message);

		}
		catch (Exception ex)
		{
			Console.WriteLine("Something went wrong: " + ex.Message);

		}
		return null;
	}
	/// <summary>
	/// Given a customer name, retrieve a specific customer from the database of the same name.
	/// If exceptions are produced, they are logged to the console instead of thrown.
	/// </summary>
	/// <param name="name"></param>
	/// <returns>Returns the requested customer object, otherwise null.</returns>
	public Customer? ReadCustomer(string name)
	{
		try
		{
			using SqlConnection connection = new(GetConnectionString());
			connection.Open();
			string sql = "SELECT * FROM Customer WHERE FirstName LIKE @name";

			StringBuilder FirstName = new(name);
			for (int i = 0; i < FirstName.Length; i++)
			{
				if (!(FirstName[i] >= 'a' && FirstName[i] <= 'z' && FirstName[i] >= 'A' && FirstName[i] <= 'Z')) FirstName[i] = '_';
			}

			using SqlCommand command = new(sql, connection);
			command.Parameters.Add("@name", SqlDbType.VarChar);
			command.Parameters["@name"].Value = name;
			using SqlDataReader reader = command.ExecuteReader();
			reader.Read();

			return ReaderHelper.ReadCustomer(reader);

		}
		catch (SqlException ex)
		{
			Console.WriteLine("Something went wrong in the db: " + ex.Message);

		}
		catch (Exception ex)
		{
			Console.WriteLine("Something went wrong: " + ex.Message);

		}
		return null;
	}
	/// <summary>
	/// Adds a customer to the database and returns a boolean representing the success or failure of the Sql.
	/// If exceptions are produced, they are logged to the console instead of thrown.
	/// </summary>
	/// <param name="cust"></param>
	/// <returns>Returns True if successful, otherwise false.</returns>
	public bool CreateCustomer(Customer cust)
	{
		try
		{
			using SqlConnection connection = new(GetConnectionString());

			connection.Open();

			string sql = "INSERT INTO Customer (FirstName,LastName,Country,PostalCode,Email) VALUES (@FirstName,@LastName,@Country,@PostalCode,@Email)";

			using SqlCommand command = new(sql, connection);

			command.Parameters.AddWithValue("@FirstName", cust.FirstName);
			command.Parameters.AddWithValue("@LastName", cust.LastName);
			command.Parameters.AddWithValue("@Country", cust.Country);
			command.Parameters.AddWithValue("@PostalCode", cust.PostalCode);
			command.Parameters.AddWithValue("@Email", cust.Email);

			command.ExecuteNonQuery();

			return true;
		}
		catch (SqlException ex)
		{
			Console.WriteLine("Something went wrong in the db: " + ex.Message);

		}
		catch (Exception ex)
		{
			Console.WriteLine("Something went wrong: " + ex.Message);

		}
		return false;
	}
	/// <summary>
	/// Alter an existing customer in the database. The customer is matched by Id, not by name.
	/// If exceptions are produced, they are logged to the console instead of thrown.
	/// </summary>
	/// <param name="cust"></param>
	/// <returns>Returns True if successful, otherwise false.</returns>
	public bool UpdateCustomer(Customer cust)
	{
		try
		{
			using SqlConnection connection = new(GetConnectionString());

			connection.Open();

			string sql = "UPDATE Customer SET Country = @country WHERE CustomerId = @id";

			using SqlCommand command = new(sql, connection);

			command.Parameters.Add("@country", SqlDbType.VarChar);
			command.Parameters["@country"].Value = cust.Country;
			command.Parameters.Add("@Id", SqlDbType.Int);
			command.Parameters["@Id"].Value = cust.CustomerId;

			command.ExecuteNonQuery();
			return true;
		}
		catch (SqlException ex)
		{
			Console.WriteLine("Something went wrong in the db: " + ex.Message);

		}
		catch (Exception ex)
		{
			Console.WriteLine("Something went wrong: " + ex.Message);

		}

		return false;
	}

	/// <summary>
	/// Retrieves CustomerId's and the customer's total spending, as a list ordered by the total spending.
	/// If exceptions are produced, they are logged to the console instead of thrown.
	/// </summary>
	/// <returns>Returns a list CustomerSpender objects</returns>
	public List<CustomerSpender> GetCustomersSortedBySpending()
	{
		List<CustomerSpender> output = new();
		try
		{
			using SqlConnection connection = new(GetConnectionString());
			connection.Open();

			string sql = "SELECT CustomerId, SUM(Total) FROM Invoice GROUP BY CustomerId ORDER BY SUM(Total) DESC";

			using SqlCommand command = new(sql, connection);

			using SqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				output.Add(ReaderHelper.ReadCustomerSpender(reader));
			}

		}
		catch (SqlException ex)
		{
			Console.WriteLine("Something went wrong in the db: " + ex.Message);
		}
		catch (Exception ex)
		{
			Console.WriteLine("Something went wrong: " + ex.Message);

		}
		return output;
	}
	/// <summary>
	/// Given a customer id, gets that customer's favourite genre, where the favourite genre is the one that is associated with the most invoices.
	/// In the case of a tie, multiple genres will be returned.
	/// Returns the genre name, genre id, and the count of associated invoices.
	/// If exceptions are produced, they are logged to the console instead of thrown.
	/// </summary>
	/// <param name="id"></param>
	/// <returns>Returns the favourite or top 2 favourite genres as an object</returns>
	public List<CustomerGenre> GetCustomerFavouriteGenre(int id)
	{
		List<CustomerGenre> Genres = new();
		try
		{
			using SqlConnection connection = new(GetConnectionString());

			connection.Open();

			string sql = "SELECT Genre.GenreId, Genre.Name, COUNT(Genre.GenreId) FROM Genre " +
				"INNER JOIN Track ON Genre.GenreId = Track.GenreId " +
				"INNER JOIN InvoiceLine ON Track.TrackId = InvoiceLine.TrackId " +
				"INNER JOIN Invoice ON InvoiceLine.InvoiceId = Invoice.InvoiceId " +
				"INNER JOIN Customer ON Invoice.CustomerId = Customer.CustomerId " +
				"WHERE Customer.CustomerId = @id " +
				"GROUP BY Genre.GenreId, Genre.Name ORDER BY COUNT(Genre.GenreId) DESC";

			using SqlCommand command = new(sql, connection);

			command.Parameters.AddWithValue("@Id", id);

			using SqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				Genres.Add(ReaderHelper.ReadCustomerGenre(reader));
			}
			// Find the biggest genre count
			var GenreCountMax = Genres[0].Count;
			// filter for only the genres where the count is equal to the max count.
			var filtered = from Genre in Genres
						   where (Genre.Count == GenreCountMax)
						   select Genre;

			return filtered.ToList();
		}
		catch (SqlException ex)
		{
			Console.WriteLine("Something went wrong in the db: " + ex.Message);

		}
		catch (Exception ex)
		{
			Console.WriteLine("Something went wrong: " + ex.Message);

		}



		return Genres;
	}

}
