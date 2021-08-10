using FinalCaseStudy.ADO_Repo;
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
    public class AnonymousUserController : ControllerBase
    {
        MovieData movieData = new MovieData();

        public ActionResult<List<Movies>> Get()
        {
            var allMovies = movieData.getAllMovies();
            return allMovies;
        }
    }
}
