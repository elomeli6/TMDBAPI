// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/* This function is going to search for the movie based on the query
id ="searchQuery" from the index html view
*/
document.getElementById("searchButton").addEventListener("click", displayMoviesTemplate);

/* This function is going to display the movie results in bootstrap cards
*/
//modify this function as it is not currently fetching movie URLS
 
    async function displayMoviesTemplate(){

        const query = document.getElementById("searchQuery").value.trim();
        if (!query) {
            alert("Please enter a search term.");
            return;
        }


        const cardLocation = document.getElementById("movieResults");
        cardLocation.innerHTML = ""
        const movies = await GetMovies(query);
        const template = document.getElementById("movieCardsTemplate");
        //const cardLocation = document.getElementById("movieResults");
        
        const arraySize = movies.length;
    
        
         for(const movie of movies){
        //movies.forEach( movie => {
            const clonedTemplate =  template.content.cloneNode(true);
            
            var movieID = movie.id;
             const movieCast =  await GetMovieCredits(movieID)
             const movieFinance = await  GetMovieFinance(movieID)
           /* const [movieCast, movieFinance] = await Promise.all([
            GetMovieCredits(movieID),
            GetMovieFinance(movieID)
        ]); */
            var posterPath = movie.poster_path;
            var posterSize = "original";
            var backdropPath = movie.backdrop_path;
            var backdropSize = "original"
            //var altImage = '~/images/cinemaAlt.png'

            const moviePoster = `https://image.tmdb.org/t/p/${posterSize}${posterPath}`
          

             const movieBackdrop = `https://image.tmdb.org/t/p/${backdropSize}${backdropPath}`;
            

            clonedTemplate.getElementById("poster").src = moviePoster;
            clonedTemplate.getElementById("cardTitle").textContent = movie.title;
            clonedTemplate.getElementById("overview").textContent = movie.overview || "No description";
            clonedTemplate.getElementById("releaseDate").textContent = movie.releaseDate;

            // Generate a unique modal ID for this movie
            const uniqueModalId = `movieModal-${movie.id}`;

            clonedTemplate.querySelector(".card").setAttribute("data-bs-target", `#${uniqueModalId}`);
            clonedTemplate.querySelector(".modal").id = uniqueModalId;
    


            clonedTemplate.getElementById("modalTitle").textContent = movie.title;
            clonedTemplate.getElementById("modalBackdrop").src = movieBackdrop;
            clonedTemplate.getElementById("modalOverview").textContent = movie.overview || "No description";
            //clonedTemplate.getElementById("modalGenre").textContent = movie.genre;
            clonedTemplate.getElementById("modalReleaseDate").textContent = movie.releaseDate;

            clonedTemplate.getElementById("modalRevenue").textContent = movieFinance.revenue;
            clonedTemplate.getElementById("modalPopularity").textContent = movieFinance.popularity;

            clonedTemplate.getElementById("modalFirstCast").textContent = movieCast[0]?.name
            ? `${movieCast[0].name} : ${movieCast[0].character}`
            : "Cast information not available.";
            clonedTemplate.getElementById("modalSecondCast").textContent = movieCast[1]?.name
            ? `${movieCast[1].name} : ${movieCast[1].character}`
            : "Cast information not available.";
            clonedTemplate.getElementById("modalThirdCast").textContent = movieCast[2]?.name
            ? `${movieCast[2].name} : ${movieCast[2].character}`
            : "Cast information not available.";
            
            cardLocation.appendChild(clonedTemplate);
           // listItem.textContent = `${member.name}: ${member.character}`;

    }
            
    
            //cardLocation.appendChild(clonedTemplate);
    
        //});
    
    }
    
     async function GetMovies(query) {
       
        try {
          const response =  await fetch(`api/movies/${query}`);
          const data = await response.json();
          console.log(data);
          return data;
          //displayMoviesTemplate(data);
        } catch (error) {
          console.error(error);
          return [];
        }
    
        
      }

      async function GetMovieCredits(query){
        try{
        
            const response =  await fetch(`api/movies/${query}/credits`)
            const data = response.json()
            return data;
        } catch(error){
            return [];
        }
      }

       async function GetMovieFinance(query){
        try{
        
            const response =  await fetch(`api/movies/${query}/revenue-popularity`)
            const data = response.json()
            return data;
        } catch(error){
            return [];
        }
      }
/* ### PERSONAL NOTES
    *Tempate literals*: this allows us to write muli-strings without needing escape characters AND embed javascript expressions ${}
     -- think string interpolation.
    **Syntax**: `` backticks NOT single or double quotes (' "")

    This did not recieve the poster url from fetchMoviePoster:

      movies.forEach(movie => { //template literals to embedd HTML code for creating bootstrap cards
            const movieCard = ` 
                <div class="card">
                    <img class="card-img-top" src=${fetchMoviePoster(movie)} alt="${movie.title}"/>
                    <div class="card-body">
                    <h5 class="card-title">${movie.title}</h5>
                    <p class="card-text"><small class="text-muted">${movie.releaseDate}</small></p>
                    <p class="card-text">${movie.overview}</p>   
                </div>
            </div>
        `;
        movieContainer.innerHTML += movieCard
    });

*/