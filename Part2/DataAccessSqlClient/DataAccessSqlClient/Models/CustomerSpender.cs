public class CustomerSpender
{
	public CustomerSpender(int customerId, decimal totalSpending)
	{
		CustomerId = customerId;
		TotalSpending = totalSpending;

	}

	public int CustomerId { get; set; }
	public decimal TotalSpending { get; set; }
}
