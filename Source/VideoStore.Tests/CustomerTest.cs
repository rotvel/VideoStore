using NUnit.Framework;
using VideoStore.Core.Model;
using VideoStore.Core.Service;

namespace VideoStore.Tests
{
    [TestFixture]
    public class CustomerTest
    {
        private Customer customer;
        private RentalService rentalService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            rentalService = new RentalService();
        }

        [SetUp]
        public void Setup()
        {
            customer = new Customer("Fred");
        }

        [Test]
        public void NewReleaseShortLeaseTest()
        {
            customer.AddRental(new Rental(new Movie("The Tigger Movie", PriceCode.NewRelease), 1));

            rentalService.CalculateRental(customer);

            Assert.AreEqual("The Tigger Movie", customer.Rentals[0].Movie.Title);
            Assert.AreEqual(3, customer.Rentals[0].Price); // DaysRented * 3.0

            Assert.AreEqual(1, customer.FrequentRenterPoints); // 1/movie + 1 if DaysRented > 1
        }

        [Test]
        public void NewReleaseLongLeaseTest()
        {
            customer.AddRental(new Rental(new Movie("The Cell", PriceCode.NewRelease), 2));

            rentalService.CalculateRental(customer);

            Assert.AreEqual("The Cell", customer.Rentals[0].Movie.Title);
            Assert.AreEqual(6, customer.Rentals[0].Price);  // DaysRented * 3.0

            Assert.AreEqual(2, customer.FrequentRenterPoints); // 1/movie + 1 if DaysRented > 1
        }

        [Test]
        public void NewReleaseShortAndLongLeaseTest()
        {
            customer.AddRental(new Rental(new Movie("The Cell", PriceCode.NewRelease), 3));
            customer.AddRental(new Rental(new Movie("The Tigger Movie", PriceCode.NewRelease), 1));

            rentalService.CalculateRental(customer);

            Assert.AreEqual(12, customer.GetTotalPrice());

            Assert.AreEqual("The Cell", customer.Rentals[0].Movie.Title);
            Assert.AreEqual(9, customer.Rentals[0].Price);

            Assert.AreEqual("The Tigger Movie", customer.Rentals[1].Movie.Title);
            Assert.AreEqual(3, customer.Rentals[1].Price);

            Assert.AreEqual(2 + 1, customer.FrequentRenterPoints); // 1/movie + 1 if DaysRented > 1
        }

        [Test]
        public void ChildrensShortLeaseTest()
        {
            customer.AddRental(new Rental(new Movie("The Tigger Movie", PriceCode.Childrens), 3));

            rentalService.CalculateRental(customer);

            Assert.AreEqual(1.5, customer.GetTotalPrice());

            Assert.AreEqual("The Tigger Movie", customer.Rentals[0].Movie.Title);
            Assert.AreEqual(1.5, customer.Rentals[0].Price); // A flat 1.5/movie

            Assert.AreEqual(1, customer.FrequentRenterPoints);
        }

        [Test]
        public void ChildrensLongLeaseTest()
        {
            customer.AddRental(new Rental(new Movie("The Tigger Movie", PriceCode.Childrens), 4));

            rentalService.CalculateRental(customer);

            Assert.AreEqual(3, customer.GetTotalPrice());

            Assert.AreEqual("The Tigger Movie", customer.Rentals[0].Movie.Title);
            Assert.AreEqual(3, customer.Rentals[0].Price); // 1.5 + (DaysRented - 3) * 1.5

            Assert.AreEqual(1, customer.FrequentRenterPoints);
        }

        [Test]
        public void RegularShortLeaseTest()
        {
            customer.AddRental(new Rental(new Movie("Plan 9 from Outer Space", PriceCode.Regular), 1));

            rentalService.CalculateRental(customer);

            Assert.AreEqual(2, customer.GetTotalPrice());

            Assert.AreEqual("Plan 9 from Outer Space", customer.Rentals[0].Movie.Title);
            Assert.AreEqual(2, customer.Rentals[0].Price); // A flat 2.0/movie

            Assert.AreEqual(1, customer.FrequentRenterPoints);
        }

        [Test]
        public void RegularLongLeaseTest()
        {
            customer.AddRental(new Rental(new Movie("8 1/2", PriceCode.Regular), 3));

            rentalService.CalculateRental(customer);

            Assert.AreEqual(3.5, customer.GetTotalPrice());

            Assert.AreEqual("8 1/2", customer.Rentals[0].Movie.Title);
            Assert.AreEqual(3.5, customer.Rentals[0].Price); //  2.0 + (DaysRented - 1) * 1.5
            
            Assert.AreEqual(1, customer.FrequentRenterPoints);
        }
    }
}
