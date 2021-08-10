using FinalCaseStudy.ADO_Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="Customer")]
    public class CustomerController : ControllerBase
    {
        MovieData movieObj = new MovieData();
        List<Movies> activeMovies = new List<Movies>();

        /*[HttpGet]
        public ActionResult<List<Movies>> GetallMovies()
        {
            return Ok(movieObj.getAllMovies());

        }*/
        [HttpGet]
        public ActionResult<List<Movies>> getActiveMovies()
        {
            var allMovies = movieObj.getAllMovies();
            foreach(var i in allMovies)
            {
                if(i.Active == true)
                {
                    //Only Adding Active Movies 
                    activeMovies.Add(i);
                }
            }
            return Ok(activeMovies);
        }

        [HttpGet("{userId}")]
        public ActionResult<List<Movies>> getFavoriteMovies(int userId)
        {
            return Ok(movieObj.getFavoriteMovies(userId));
        }


        [HttpPost("{userId}")]
        public ActionResult addFavoriteMovie(int userId, Movies movie)
        {
            try
            {
                movieObj.addFavoriteMovie(userId, movie.Id);
                return Ok(movie);
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

        }
        
        [HttpDelete("{userId}")]
        public ActionResult deletefavoriteMovie(int userId, Movies movie)
        {
            try
            {
                movieObj.removeFavoriteMovie(userId, movie.Id);
                return Ok(movie);
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
