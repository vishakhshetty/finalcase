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
    [Authorize(Roles="Admin")]
    public class AdminController : ControllerBase
    {
        MovieData movieobj = new MovieData();

        [HttpGet]
        public ActionResult<List<Movies>> Get()
        {
            return movieobj.getAllMovies();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Movies movie)
        {
            try
            {
                movieobj.updateMovieData(id, movie);
                return Ok(movie);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

        }

    }
}
