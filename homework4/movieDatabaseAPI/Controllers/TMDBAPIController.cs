using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using movieDatabaseAPI.Models;
using movieDatabaseAPI.Services;

namespace movieDatabaseAPI.Controllers
{

[Route("api/movies")] //this routes all methods in the class actions below to this url location. also called a template
[ApiController] //This tells ASP.Net that this controller is strictly for building APIs

//Earlier this failed to build because I did not inherit Controller class " : Controller" does the views, rendering actions etc could not be done
//Controller class has ControllerBase built in. Since we still need to render a view we 
public class TMDBAPIController : ControllerBase 
{
    /// <summary>
    /// DI - Dependancy Inversion Principle. It doesn't care you implement TMDBService
    /// In Program.cs or Startup.cs, the DI container is configured to know which implementation to use for ITMDBService:
    /// So we create _tmdbService variable of Type ITMDService - we w
    /// </summary>
    
    private readonly ITMDBService  _tmdbService; 
    //Now we construct our TMDBApiController and our _tmdbService is iniatlized to tmdbservice and it is injected via DI. _tmdb is now reference to an object tmdbservice thus
    // we can now use it to call the methods of tmdbservice object
    public TMDBAPIController (ITMDBService tmdbService)
    {
        _tmdbService = tmdbService;
    }

    /// <summary>
    /// Now we can use the Get our SearchMoviesAsync method. Sinces we are "getting" movies we are going to get request to our api.
    /// We define and route HttpGet request with a verb attribute query. These verbs will be appended to the api path:
    /// api/shows/{query} - which means this action will only match GET requests in this form.
    /// GET api/shows/titanic
    /// </summary>
    /// <param name="query"></param> movie string
    /// <returns></returns>
    
    [HttpGet("{query}")]
    // Since our method is a Task of Type enumerables of TMDBMovie we need to make sure our function return this data form as well
    public async Task<IEnumerable<TMDBMovie>> SearchMovies (String query)
    {
        //now we can call our method to get the movies (which is in our _tmdbService) and store the enumerable list of them
        var movies = await _tmdbService.SearchMoviesAsync(query);
        return movies;

    }

    /// <summary>
    /// Gets the full URL of a movie poster based on size and poster path.
    /// Example route: GET api/shows/poster?size=500&posterPath=/path-to-poster.jpg
    /// api/shows/poster?size=w500&posterPath/1E5baAaEse26fej7uHcjOgEE2t2.jpg
    /// </summary>
    /* [HttpGet("poster")]
    public async Task<string> GetPosterUrl(string size, string posterPath){
        var posterURL = await _tmdbService.GetMoviePoster(size,posterPath);
        return posterURL;
    } */
    
    [HttpGet("{id}/credits")]

    public async Task<IEnumerable<Cast>> GetMovieCreditsAsync(int id){
        var movieDetails = await _tmdbService.GetMovieCredits(id);
        return movieDetails;
    }

    [HttpGet("{id}/revenue-popularity")]
    public async Task<IActionResult> GetMovieRevenueAndPopularity(int id)
    {
        var data = await _tmdbService.GetMovieRevenueAndPopularityAsync(id);
        return Ok(data);
        
    }


}


}