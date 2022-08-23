
using System.Collections.Generic;


namespace DataAccessSqlClient.Repositories
{
	public interface ICustomerRepository
	{
		List<Customer> ReadAllCustomers();

		List<Customer> ReadAllCustomers(int limit, int offset);

		List<CustomerCountry> NumberOfCustomersInEachCountry();

		Customer ReadCustomer(int id);

		Customer ReadCustomer(string name);

		bool CreateCustomer(Customer customer);

		bool UpdateCustomer(Customer customer);

		List<CustomerSpender> GetCustomersSortedBySpending();

		List<CustomerGenre> GetCustomerFavouriteGenre(int id);


	}

}