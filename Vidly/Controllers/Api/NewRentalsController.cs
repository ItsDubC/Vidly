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

			#region Public API logic (defensive approach)

			////Note:  For a public API, it is better to validate and return explicit error messages even if it pollutes the code.  For an internal API or MVC application, it's ok to throw errors
			//if (newRental.MovieIds.Count == 0)
			//	return BadRequest("No Movie IDs provided.");

			//var customer = _context.Customers.SingleOrDefault(x => x.Id == newRental.CustomerId);

			//if (customer == null)
			//	return BadRequest("Invalid Customer ID.");

			//var movies = _context.Movies.Where(x => newRental.MovieIds.Contains(x.Id)).ToList();

			//if (movies.Count != newRental.MovieIds.Count)
			//	return BadRequest("One or more Movie IDs are invalid.");

			#endregion

			var customer = _context.Customers.Single(x => x.Id == newRental.CustomerId);
			var movies = _context.Movies.Where(x => newRental.MovieIds.Contains(x.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie '" + movie.Name + "' is not available.");

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
