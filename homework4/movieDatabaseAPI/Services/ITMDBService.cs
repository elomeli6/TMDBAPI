using Microsoft.AspNetCore.Mvc;
using movieDatabaseAPI.Models;

namespace movieDatabaseAPI.Services
{
    public interface ITMDBService
    {
        Task<IEnumerable<TMDBMovie>> SearchMoviesAsync (string query);
        //Task<string> GetMoviePoster (string size, string posterPath);
        
        Task<IEnumerable<Cast>> GetMovieCredits(int id);
        Task<MovieRevenuePopularity> GetMovieRevenueAndPopularityAsync(int id);
    }
}