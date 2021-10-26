namespace VideoStore.Core.Model
{
	public class Rental
	{
		public Rental(Movie movie, int daysRented)
		{
			Movie = movie;
			DaysRented = daysRented;
		}

		public int DaysRented { get; }

		public Movie Movie  { get; }

		/// <summary>
		/// Calculated by RentalService
		/// </summary>
		public double Price { get; set; }
	}
}
