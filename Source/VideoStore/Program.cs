using System;
using System.Text;
using VideoStore.Core.Model;
using VideoStore.Core.Service;

namespace VideoStore
{
    public class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer("Fred");

            customer.AddRental(new Rental(new Movie("Plan 9 from Outer Space", PriceCode.Regular), 1));
            customer.AddRental(new Rental(new Movie("The Cell", PriceCode.NewRelease), 2));
            customer.AddRental(new Rental(new Movie("Far til 4 på Bornholm", PriceCode.Childrens), 4));

            var service = new RentalService();
            service.CalculateRental(customer);

            PrintStatement(customer);
        }

        private static void PrintStatement(Customer customer)
        {
            const string tab = "    ";
            const int pad = 80;

            var sb = new StringBuilder();

            sb.Append("Rental Record for ").AppendLine(customer.Name);

            foreach (var rental in customer.Rentals)
            {
                sb.Append(tab).Append(rental.Movie.Title.PadRight(pad)).AppendLine(rental.Price.ToString("N2").PadLeft(10));
            }

            sb.Append("You owe ").AppendLine(customer.GetTotalPrice().ToString("N2"));
            sb.Append("You earned ").Append(customer.FrequentRenterPoints).AppendLine(" frequent renter points.");

            Console.WriteLine(sb.ToString());
        }
    }
}
