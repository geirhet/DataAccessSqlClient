public class CustomerGenre
{
	public CustomerGenre(int genreId, string name, int count)
	{
		GenreId = genreId;
		Name = name;
		Count = count;
	}

	public int GenreId { get; set; }
	public string Name { get; set; }
	public int Count { get; }
}