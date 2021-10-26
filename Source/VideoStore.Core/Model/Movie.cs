namespace VideoStore.Core.Model
{
	public class Movie
	{
		public Movie(string title, PriceCode priceCode)
		{
			Title = title;
			PriceCode = priceCode;
		}

		public string Title { get; }

		public PriceCode PriceCode { get; }
	}
}
