using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCaseStudy.ADO_Repo
{
    public class MovieData
    {
        public string ConnectionString
        {
            get
            {
                return "Data Source=DESKTOP-T860L43\\MSSQLSERVER2;Initial Catalog=WebAPI_1;Integrated Security=True";
            }
        }
        List<Movies> allMovies = new List<Movies>();
        List<Movies> favoriteMovies = new List<Movies>();
        public List<Movies> getAllMovies()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            string qry = "Select * from Movies";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Movies movie = new Movies()
                {
                    Id = (int)reader[0],
                    Title = (string) reader[1],
                    Active = (bool) reader[2],
                    DateofLaunch = Convert.ToDateTime(reader[3]),
                    Genre = (string) reader[4],
                    HasTeaser = (bool) reader[5]

                };
                allMovies.Add(movie);
            }
            con.Close();
            return allMovies;
        }

        public void updateMovieData(int id,Movies movie)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            int active = movie.Active ? 1 : 0;
            int hasTeaser = movie.HasTeaser ? 1 : 0;


            string qry = "Update Movies Set Title = '" + movie.Title + "', Active = " + active+ ", DateOfLaunch = '" + movie.DateofLaunch + "', Genre='" + movie.Genre + "', HasTeaser=" + hasTeaser + " Where Id =" + id + ";";
            SqlCommand cmd = new SqlCommand(qry, con);

            cmd.ExecuteNonQuery();
            con.Close();
        }

        public List<Movies> getFavoriteMovies(int userId)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            string qry = "Select m.* from Movies m join Favorites f on m.Id = f.movieId where f.UserId="+userId +";";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Movies movie = new Movies()
                {
                    Id = (int)reader[0],
                    Title = (string)reader[1],
                    Active = (bool)reader[2],
                    DateofLaunch = Convert.ToDateTime(reader[3]),
                    Genre = (string)reader[4],
                    HasTeaser = (bool)reader[5]

                };
                favoriteMovies.Add(movie);
            }
            con.Close();
            return favoriteMovies;
        }


        public void addFavoriteMovie(int userId, int movieId)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            string qry = "Insert into Favorites values("+userId+","+movieId+");";
            SqlCommand cmd = new SqlCommand(qry, con);

            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void removeFavoriteMovie(int userId, int movieId)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            string qry = "Delete from Favorites where movieId =" + movieId +" and UserId = " + userId + " ;";
            SqlCommand cmd = new SqlCommand(qry, con);

            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
