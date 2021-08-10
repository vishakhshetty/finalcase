using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCaseStudy
{
    public class Movies
    {
        public int Id { get; set; }
        public string Title { get; set; }

        //public string BoxOffice { get; set; }

        public bool Active { get; set; }

        public DateTime DateofLaunch { get; set; }

        public string Genre { get; set; }

        public bool HasTeaser { get; set; }
    }
}
