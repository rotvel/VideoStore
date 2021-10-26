using System;
using System.Collections.Generic;
using VideoStore.Core.Model;

namespace VideoStore.Core.Service
{
    public class RentalService
    {
        public void CalculateRental(Customer customer)
        {
            foreach (var rental in customer.Rentals) 
            {
                rental.Price = CalculatePrice(rental);
            }

            customer.FrequentRenterPoints = CalculateFrequentRenterPoints(customer.Rentals);
        }

        public int CalculateFrequentRenterPoints(IEnumerable<Rental> rentals)
        {
            var frequentRenterPoints = 0;

            foreach (var rental in rentals)
            {
                frequentRenterPoints++;

                if (rental.Movie.PriceCode == PriceCode.NewRelease && rental.DaysRented > 1)
                {
                    frequentRenterPoints++;
                }
            }

            return frequentRenterPoints;
        }

        public double CalculatePrice(Rental rental)
        {
            return rental.Movie.PriceCode switch
            {
                PriceCode.Regular => CalculateRegularPrice(rental),
                PriceCode.NewRelease => CalculateNewReleasePrice(rental),
                PriceCode.Childrens => CalculateChildrensPrice(rental),
                _ => throw new ArgumentException($"invalid {nameof(rental.Movie.PriceCode)}: {rental.Movie.PriceCode}"),
            };
        }

        //TODO: make magic numbers in CalculateXXX methods configurable

        private double CalculateRegularPrice(Rental rental)
        {
            var priceRegular = 2.0;
            if (rental.DaysRented > 2)
            {
                priceRegular += (rental.DaysRented - 2) * 1.5;
            }

            return priceRegular;
        }

        private double CalculateNewReleasePrice(Rental rental)
        {
            return rental.DaysRented * 3;
        }

        private double CalculateChildrensPrice(Rental rental)
        {
            var priceChildrens = 1.5;
            if (rental.DaysRented > 3)
            {
                priceChildrens += (rental.DaysRented - 3) * 1.5;
            }

            return priceChildrens;
        }
    }
}
