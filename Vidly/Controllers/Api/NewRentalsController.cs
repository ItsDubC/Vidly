using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //  using Single instead of SingleOrDefault because since this is a public API, we prefer to throw a vague exception if a malicious user attempts to pass an invalid customer ID
            var customer = _context.Customers.Single(x => x.Id == newRental.CustomerId);  

            var movies = _context.Movies.Where(x => newRental.MovieIds.Contains(x.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                movie.NumberAvailable--;

                var rental = new Rental()
                {
                    DateRented = DateTime.Now,
                    Movie = movie,
                    Customer = customer
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
