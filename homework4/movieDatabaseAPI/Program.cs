using System.Net.Http.Headers;
using movieDatabaseAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace movieDatabaseAPI
{

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddUserSecrets<Program>();
        
        string tmdbApiKey = builder.Configuration["tmdbApiKey"];
        // I got a 401 error code meaning that my API key was not loading. Adding this for future reference to make sure its loading.
        if (string.IsNullOrEmpty(tmdbApiKey))
        {
            throw new InvalidOperationException("TMDB API key is missing or not configured.");
        }
        string tmdbAPIUrl = "https://api.themoviedb.org/3";

       

        /// <summary>
        /// call DI container to create and store HTTPClient
        /// this is also called a named client. see: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-7.0
        /// { "accept", "application/json" },
       /// { "Authorization", "Bearer"
       /// </summary>
        builder.Services.AddHttpClient<ITMDBService, TMDBService>((httpClient, services) =>
        {
            httpClient.BaseAddress = new Uri(tmdbAPIUrl);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tmdbApiKey);
            return new TMDBService(httpClient, services.GetRequiredService<ILogger<TMDBService>>());
        });
       
        //Added ITMDB interfaces to DI, so that I can inject ITMD to TMDB !!THIS WAS OVERLOADING THE HTTPCLIENT DI AND CAUSING ISSUES
        //builder.Services.AddScoped<ITMDBService, TMDBService>();

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapControllerRoute(
            name: "api",
            pattern: "api/{controller}/{action}/{query?}");

        

        app.Run();
    }
}
}

/*   builder.Services.AddHttpClient<ITMDBService, TMDBService>((httpClient, services) =>
        {
            httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3");
            //httpClient.DefaultRequestHeaders.Add("accept","application/json" );
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tmdbApiKey);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Correct "accept"

           // httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tmdbApiKey);
           // httpClient.DefaultRequestHeaders.Add("Bearer", tmdbApiKey);
            return new TMDBService(httpClient, services.GetRequiredService<ILogger<TMDBService>>());
        });

 */       