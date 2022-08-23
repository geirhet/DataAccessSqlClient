using System.Data.SqlClient;

public static class ReaderHelper
{
	/// <summary>
	/// Helper class for reading SQL data and generating a Customer class.
	/// </summary>
	/// <param name="reader"></param>
	/// <returns>Returns a Customer object.</returns>
	public static Customer ReadCustomer(SqlDataReader reader)
	{
		return new Customer(
				id: reader.GetInt32(0),
				firstName: reader.GetString(1),
				lastName: reader.GetString(2),
				country: reader.IsDBNull(7) ? null : reader.GetString(7),
				phone: reader.IsDBNull(9) ? null : reader.GetString(9),
				postalCode: reader.IsDBNull(8) ? null : reader.GetString(8),
				email: reader.GetString(11)
				);
	}

	/// <summary>
	/// Helper class for reading SQL data and generating a CustomerCountry class.
	/// </summary>
	/// <param name="reader"></param>
	/// <returns>Returns a CustomerCountry object.</returns>
	public static CustomerCountry ReadCustomerCountry(SqlDataReader reader)
	{
		return new CustomerCountry(
			countryCount: reader.GetInt32(0),
			countryName: reader.IsDBNull(1) ? null : reader.GetString(1)
		);
	}

	/// <summary>
	/// Helper class for reading SQL data and generating a customer class.
	/// </summary>
	/// <param name="reader"></param>
	/// <returns>Returns a CustomerSpender object.</returns>
	public static CustomerSpender ReadCustomerSpender(SqlDataReader reader)
	{
		return new CustomerSpender(
			customerId: reader.GetInt32(0),
			totalSpending: reader.GetDecimal(1)
			);
	}

	/// <summary>
	/// Helper class for reading SQL data and generating a CustomerGenre class.
	/// </summary>
	/// <param name="reader"></param>
	/// <returns>Returns a CustomerGenre object.</returns>
	public static CustomerGenre ReadCustomerGenre(SqlDataReader reader)
	{
		return new CustomerGenre(
			genreId: reader.GetInt32(0),
			name: reader.GetString(1),
			count: reader.GetInt32(2)
			);
	}
}