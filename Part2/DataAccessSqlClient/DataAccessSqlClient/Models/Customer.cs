
public class Customer
{
	public int CustomerId { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string? Country { get; set; }
	public string? PostalCode { get; set; }
	public string? Phone { get; set; }
	public string Email { get; set; }

	public Customer(int id, string firstName, string lastName, string? country, string? postalCode, string? phone, string email)
	{
		CustomerId = id;
		FirstName = firstName;
		LastName = lastName;
		Country = country;
		PostalCode = postalCode;
		Phone = phone;
		Email = email;
	}
}

