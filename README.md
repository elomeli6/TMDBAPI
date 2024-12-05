Goals for this homework:
Be able to write a MVC Core web application that employs AJAX to build a single-page responsive application
Learn how to use an existing external API that requires authentication to acquire data, server-side
Appreciate and understand the interface of a modern web REST API by using several of it's endpoints
Demonstrate the use of JSON and AJAX, including parsing and generating JSON on the server in C#
Understand the purpose, operation and some benefits and drawbacks of AJAX style pages
Overall Requirements:
This application should have only one page: one MVC controller (Home) and one view (Index)
All dynamic functionality is created with Javascript AJAX calls to Web API methods that return JSON data
Use JSON as the data exchange format
All Javascript must be placed into a separate file (in the Scripts folder) and included via a @section section
Use routing in an ApiController for retrieving data via AJAX with appropriate REST-like URLs given below
Must use the Service pattern with DI to abstract out the interaction with TMDB's API
Use either jQuery or vanilla JavaScript
Your API key / bearer token MUST NOT be anywhere in your project source code or Git repository! It absolutely may not be sent to the client browser.
You may NOT use any pre-built packages to implement AJAX for you, either server-side or client-side.  It must be done with the basic tools we've gone over in class.
You may NOT use a library to access the TMDB API. (e.g. TMDbLib Links to an external site.) You must implement all access to the TMDB API yourself with primitive HTTP requests from C# and model classes you build yourself.
No AJAX requests may be made from the client browser to TMDB directly.  All data must come from your own server, which gets it from the TMDB API.
Do not send the entire JSON data object that you receive from GitHub to the client. Process it server-side so that you only send what is needed for your application. Sending the whole thing is not only bad practice; it is a data leak and a security no-no.
Introduction
This homework is all about creating a responsive, single-page application. An extreme case of this kind of application is the Google Mail client. We don't have those kinds of aspirations, but we can definitely get the main process down.

So the page that first loads (from the server) doesn't have any data. It presents a minimal interface only. But it does load your JavaScript, which requests data from your server via asynchronous HTTP requests and then builds the page content as desired as the requests come back from your server. Users then interact with the page and more asynchronous calls are placed to send data to and receive data back from your server. The benefit is that, perhaps surprisingly, the time to load HTML, CSS and associated resources, and build and render a DOM, is actually significant. Not doing those things, while using JavaScript to retrieve data and modify the DOM, often creates a more responsive page for the user. (In the extreme limit, this explains web frameworks like React Links to an external site..) We want to improve on our JavaScript skills for building parts of your project next year.

It has become quite a bit easier to do this in JavaScript since the early days when Google pretty much invented it.  For some reference, here's the underlying way to do it from the Mozilla Developer Network Links to an external site., which is pretty awful.  It's now WAY easier with the Fetch API Links to an external site..  Alternately, feel free to use jQuery Links to an external site..

A second purpose of this homework is to learn how to use an existing professional/commercial API as a resource.  You'll need this for the team projects where you're required to use an external API for one or more core features.

This application is a continuation of our Movies and Shows theme.  This time we will use The Movie Database Links to an external site. (TMDB) as our source of data.  We will NOT use the database from HW3 and in fact we won't have a database at all for this homework.  We've already done an actor search so this time we'll do a movie search.  The homepage of TMDB has a great search feature.  Let's try to reproduce some of it.  

Before starting to build something it's a great idea to do some whiteboarding, or simply sketching out what you want.  Here's what I sketched out.  The home page is simple, with a search bar and button.  The user types in the name, or partial name of a movie, and clicks the search button.  We then display the top 10-20 results in order by popularity.  For each match, we want to show just a quick look a the movie.  The idea is we want to show quite a few of them so the user can easily choose the one they were after.

image.png

Then, they click on a movie and can see a lot more detail:

image.png

If the user wants to go back to their search results, all they have to do is click the X to dismiss the modal or simply click outside it.  The previous search results are still there so they can click on another one to see it's details.  We want this experience to be very fast and easy.  Compare this to the TMDB website, which takes you to a separate page for the details.

A summary of this feature could be written as:

As a movie enthusiast I want to find information about a movie, such as runtime, rating, revenue generated and cast members, so I can learn about it, compare to other movies, and discover new actors.  I'd like it to be faster and easier than other methods such as looking it up on IMDb or Wikipedia.
Questions/Tasks:
[Setup] Start by creating a normal MVC Core web application. Simplify it by deleting the Privacy page. We need to follow the Terms of Service for TMDB so find the required statement and put it in your About page.  Also download one of the required TMDB logos and place it in your About page or at the bottom of your Index page.  
[Setup] Go to https://www.themoviedb.org/ Links to an external site. and create an account.  Then go to Settings and API to request an API key.  Mine was issued immediately.  The key you want is labeled "API Read Access Token".  Copy it and add it as a secret in your project using dotnet user-secrets.  Access it through the Configuration object in Program.cs.
[Requirements] Watch the following video to get a better understanding of all the requirements for building this application:

[Modeling] If the flow of the implementation isn't clear I've done some modeling for you that should help.  At the bottom of this page I've included a sequence diagram showing the entire flow for a typical (successful) use case.  Make sure it makes sense before proceeding to implementation.
[Feature #1] Build the first component: search for movie by title and return movie summaries.  Use the endpoint /search/movie Links to an external site. .  You only need to show the first page of results (in descending order by popularity).  Include at minimum what is shown in the video: the poster image, title, release date (in Month Day, Year format) and description (if you want, only show the first 140 or so characters of it to keep it short).  If you want to add more, make sure it doesn't increase the height of that element very much as we want to show all search matches in a fairly condensed format.  I used Bootstrap cards for mine and I'm not happy with it.  If I had to do it over I'd write my own CSS for these.
[Feature #2] Build the second component: display details of a selected movie in a modal (or equivalent).  Include at a minimum what is shown in the video: background image (different from the poster), title, year, release date, genres, runtime in hours and minutes, popularity, revenue (formatted in USD currency), full overview/description, and cast, including both the actor's name and role name if available.  The endpoints to use are: /movie/{id} Links to an external site. and /movie/{id}/credits Links to an external site..
[Testing] There are two pieces of logic that can be tested easily.  Write appropriate unit tests in a separate test project for
Transforming runtime in minutes to a string in hours and minutes.  e.g. 88 => "1 hour 28 minutes", 200 => "3 hours 20 minutes", 49 => "49 minutes", 0 => "not available"
Transforming release date into Month Day, Year.  e.g. "1980-07-25" => "July 25, 1970", "" => "not available"
Both of these pieces of functionality fall squarely in the "Presentation" domain and so should be handled by JavaScript.  However, we have not yet set up the capacity to test JavaScript so we'll do this in the "Business Logic" layer or service layer on the server and test it with NUnit.
[Testing] Manual testing of the application.  Spend some time manually testing your application through the user interface.  Make sure all the different ways you can use the application lead to acceptable behavior (e.g. no errors, even in the console).  Next
Purposefully corrupt your TMDB bearer token to simulate a problem with authentication and run through your manual testing again.  If you experience errors go back and put in suitable exception handling.
Purposefully alter the URL going to the TMDB API to simulate a network failure (or disconnect your WiFi/network).  Run again / fix.
The last one we'd like to test is too hard for now, but I'll mention it for completeness.  We'd like to test the situation where the JSON returned by the API is not what we expect (either they changed it or it was corrupted).  This might lead to a JsonException being thrown when we try to deserialize it. Look at your code and make sure there is exception handling for this situation.
[Turn it in] Turn it in as usual on GitHub and submit a video demo/code walkthrough here on Canvas.
 

Sequence diagram for normal successful requests:

sequence_diag_2.svg
