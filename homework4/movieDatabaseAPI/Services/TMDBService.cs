using System.Text.Json;
using System.Net.Http.Headers;
using movieDatabaseAPI.Models;
using System.Threading.Tasks;
using movieDatabaseAPI.Services;
using NuGet.Protocol.Core.Types;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Mvc;

namespace movieDatabaseAPI.Services
{
    public class TMDBService : ITMDBService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger <TMDBService> _logger;

        public TMDBService (HttpClient httpClient, ILogger<TMDBService> logger){
            _httpClient = httpClient;
            Console.WriteLine($"HttpClient BaseAddress: {httpClient.BaseAddress}");
            _logger = logger;
        }

        public async Task<IEnumerable<TMDBMovie>> SearchMoviesAsync (string query){
            /// <summary>
            /// Now we need to call the API to search and return our movie object. <c>The httpClient</c> class allows us to this: https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient
            /// First we have to create an API call. We will store the response here. This will the store the HttpResponseMessage object in that variable. The await keyboard means the response will pause the SearchMoviesAsync Containing method
            /// until a response is recieved. The thread is not blocked thus our program can run other tasks or requests
            /// </summary>

            _logger.LogInformation("Calling TMDB API with query: {query}", query);
            string endpoint = $"https://api.themoviedb.org/3/search/movie?query={query}";

            HttpResponseMessage response = await _httpClient.GetAsync(endpoint); //forgot to add https:// which gave me invalid uri was provided
            //var response = await _httpClient.GetAsync($"/search/movie?query={Uri.EscapeDataString(query)}");
            // DONE Build HTTPResponse
           // response.EnsureSuccessStatusCode();

            string responseBody;
            if(response.IsSuccessStatusCode)
            {
                responseBody = await response.Content.ReadAsStringAsync();
            }
            else
            {
                _logger.LogError($"Failed to find movie: {query} - {response.StatusCode}\n{response.Content}"); 
                return null;
            }
            /// <summary>
            /// Response is successful then we get the body of the http response aka the Content and encapsulated it (In our case its a JSON Object). Then we will convert it to a string for easier and further processing using ReadAsStringAsync() method
            /// forgot to add await keyboard causing type conversion error -- each time we use async we have to use await
            /// var content = await response.Content.ReadAsStringAsync();
            /// ----
            /// Now that we have our JSON object as string we need to convert to a C# object. This is called JSON dserialization: https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/deserialization
            /// this is done using the method :JsonSerializer.Deserialize (). We can also use our object type -- Root - to deserilze into < > // We need type Root because the JSON movie data is stored in Results
            /// </summary>
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            //var root = JsonSerializer.Deserialize<Root>(content);

            //Now we return the an IEnumarble collection of C# TMDBMovie objects that matched the query. 
            Root movies =  JsonSerializer.Deserialize<Root>(responseBody, options);
            
            //turn movies into an object in this case a TMDBMovie object
            return movies.Results.Select (r => new TMDBMovie
                {
                    Id = r.Id,
                    title = r.title,
                    release_date = r.release_date,
                    overview = r.overview,
                    poster_path = r.poster_path,
                    backdrop_path = r.backdrop_path
                });

        }

        /* public async Task<string> GetMoviePoster(string size, string posterPath){
            // TODO implement api call to get poster image. https://api.themoviedb.org/3/movie/{movie_id}/images
            //api for images: https://image.tmdb.org/t/p/w500/1E5baAaEse26fej7uHcjOgEE2t2.jpg
            //need 3 peices to render: base_url, a file_size and a file_path
            //the 2 pieces we need to call the configuartion api to retrieve
            //here is we will contstruct the url to get the image
            //This function is a Task of Type String because we want to return the poster url
            _logger.LogInformation("Calling TMDB Image API with query");
            string endpoint = $"https://image.tmdb.org/t/p/{size}{posterPath}";


            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
            
            string responseBody;
            if(response.IsSuccessStatusCode)
            {
                return endpoint; //return url to be used in js
            }
            else
            {
                _logger.LogError($"Failed to find movie: {posterPath} - {response.StatusCode}\n{response.Content}"); 
                return null;
            }

        } */
            //https://api.themoviedb.org/3/movie/{movie_id}

            
            public async Task<IEnumerable<Cast>> GetMovieCredits(int id)
            {
                
                    // Construct the TMDB API endpoint
                    string endpoint = $"https://api.themoviedb.org/3/movie/{id}/credits";

                    // Call TMDB API
                   HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
            
                    string responseBody;
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Success");
                        responseBody = await response.Content.ReadAsStringAsync();
                    
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    CastRepo castMembers = JsonSerializer.Deserialize<CastRepo>(responseBody, options);
                    return castMembers.cast.Select(r => new Cast
                    {
                        id = r.id,                       
                        name = r.name,
                        character = r.character
                
                    });
                    }
                    else
                    {
                        throw new Exception($"Error: {response.StatusCode}");
                    }
    }


            public async Task<MovieRevenuePopularity> GetMovieRevenueAndPopularityAsync(int id)
                {
                    string endpoint = $"https://api.themoviedb.org/3/movie/{id}";

                    HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        // Deserialize into a MovieRevenuePopularity object
                        return JsonSerializer.Deserialize<MovieRevenuePopularity>(responseBody, options);
                    }
                    else
                    {
                        throw new Exception($"Error: {response.StatusCode}");
                    }
 
               }

    }
}

/// <summary>
/// Class <c>Point</c> models a point in a two-dimensional plane.
/// </summary>
/// 
