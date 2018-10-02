using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        //private IEnumerable<Movie> _movies;
        private ApplicationDbContext _context;

        public MoviesController()
        {
            //_movies = new List<Movie>
            //{
            //    new Movie { Id = 1, Name = "Shrek" },
            //    new Movie { Id = 2, Name = "Wall-E" }
            //};
            _context = new ApplicationDbContext();
        }

        // GET: Movies
        //public ActionResult Random()
        //{
        //    Movie movie = new Movie() { Name = "Shrek" };
        //    //return View(movie);
        //    //return Content("Hello World");
        //    //return HttpNotFound();
        //    //return new EmptyResult();
        //    //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });

        //    //ViewData["Movie"] = movie;
        //    //ViewBag.Movie = movie;

        //    //var viewResult = new ViewResult();
        //    //viewResult.ViewData.Model

        //    var customers = new List<Customer>()
        //    {
        //        new Customer { Name = "Customer 1"},
        //        new Customer { Name = "Customer 2"}
        //    };

        //    var viewModel = new RandomMovieViewModel
        //    {
        //        Movie = movie,
        //        Customers = customers
        //    };

        //    return View(viewModel);
        //}

        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;

        //    if (string.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";

        //    return Content(string.Format("pageIndex = {0}, sortBy = {1}", pageIndex, sortBy));
        //}

        public ViewResult Index()
        {
            //var movies = _context.Movies.Include(m => m.Genre).ToList();

            //return View(movies);
            return View();
        }

        //public ActionResult ByReleaseDate(int year, int month)
        //{
        //    return Content(year + "/" + month);
        //}

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseYear(int year, int month)
        {
            return Content(year + "/" + month);
        }

        //public ViewResult Index()
        //{
        //    return View(_movies);
        //}

        public ActionResult Details(int id)
        {
            ////Movie movie = _movies.FirstOrDefault(x => x.Id == id);
            Movie movie = _context.Movies.Include(m => m.Genre).FirstOrDefault(x => x.Id == id);

            if (movie == null)
                return HttpNotFound();
            else
                return View(movie);
        }

        public ActionResult New()
        {
            var viewModel = new MovieFormViewModel()
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            Movie movie = _context.Movies.SingleOrDefault(x => x.Id == id);

            if (movie == null)
                return HttpNotFound();
            else
            {
                var viewModel = new MovieFormViewModel(movie)
                {
					Genres = _context.Genres.ToList()
				};

                return View("MovieForm", viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
			if (!ModelState.IsValid)
			{
				var viewModel = new MovieFormViewModel(movie)
				{
					Genres = _context.Genres.ToList()
				};

				return View("MovieForm", viewModel);
			}

			if (movie.Id == 0)
			{
				movie.DateAdded = DateTime.Now;
				_context.Movies.Add(movie);
			}
			else
			{
				var movieInDb = _context.Movies.Single(x => x.Id == movie.Id);

				movieInDb.Name = movie.Name;
				movieInDb.ReleaseDate = movie.ReleaseDate;
				movieInDb.GenreId = movie.GenreId;
				movieInDb.NumberInStock = movie.NumberInStock;
			}

            _context.SaveChanges();


            return RedirectToAction("Index", "Movies");
        }
    }
}