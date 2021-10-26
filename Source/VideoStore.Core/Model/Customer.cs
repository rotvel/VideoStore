using System.Collections.Generic;
using System.Linq;

namespace VideoStore.Core.Model
{
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
            Rentals = new List<Rental>();
        }
        
        public int FrequentRenterPoints { get; set; }

        public string Name { get; }

        public void AddRental(Rental rental)
        {
            Rentals.Add(rental);
        }

        public List<Rental> Rentals { get; }

        /// <summary>
        /// Returns 0 if rental has not been calculated yet.
        /// </summary>
        /// <returns></returns>
        public double GetTotalPrice()
        {
            return Rentals.Sum(r => r.Price);
        }
    }
}
