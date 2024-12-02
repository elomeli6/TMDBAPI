using movieDatabaseAPI.Models;

namespace movieDatabaseAPI.Models
{
    public class TMDBMovie
    {
        public int Id { get; set;}
        public string title { get; set;}
        //public DateTime releaseDate { get; set;}
        public string overview { get; set;} //Description 
        public string poster_path {get; set;}
        public string backdrop_path {get; set;}
        public string release_date { get; set; }
    
    }
    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
    }


    //JSON responses have a root starting point which have nested results. We need to access the movies inside the nested array called Results -- we can see the JSON response in the TMDB Website
    //The name of the properties must match the name of the JSON field names
    public class Root {
        public int Page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }

        //Since our Interface is of Enum we will wrap the results inside a enum as well.
        public IEnumerable<TMDBMovie> Results { get; set; }

    }

 

//api for images: https://image.tmdb.org/t/p/w500/1E5baAaEse26fej7uHcjOgEE2t2.jpg
            //need 3 peices to render: base_url, a file_size and a file_path
            //the 2 pieces we need to call the configuartion api to retrieve 
            //w500 is the size --- found in configuration response
 

     public class Cast
    {
        public int id { get; set; }
        public string name { get; set; }
        public string character { get; set; }

    }

    public class CastRepo {
        public int id {get; set;}
        public IList<Cast> cast {get;set;}
    }

    //https://api.themoviedb.org/3/movie/{movie_id}


   public class MovieRevenuePopularity {
        
            public long Revenue { get; set; }
            public double Popularity { get; set; }
        }


}

//TODO the poste