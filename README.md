# Goals for This Homework

- Be able to write an MVC Core web application that employs AJAX to build a single-page responsive application.
- Learn how to use an existing external API that requires authentication to acquire data server-side.
- Appreciate and understand the interface of a modern web REST API by using several of its endpoints.
- Demonstrate the use of JSON and AJAX, including parsing and generating JSON on the server in C#.
- Understand the purpose, operation, and some benefits and drawbacks of AJAX-style pages.

---

# Overall Requirements

- This application should have **only one page**: one MVC controller (`Home`) and one view (`Index`).
- All dynamic functionality is created with JavaScript AJAX calls to Web API methods that return JSON data.
- Use **JSON** as the data exchange format.
- All JavaScript must be placed into a separate file (in the `Scripts` folder) and included via a `@section` section.
- Use routing in an `ApiController` for retrieving data via AJAX with appropriate REST-like URLs provided below.
- Must use the **Service pattern** with Dependency Injection (DI) to abstract out the interaction with TMDB's API.
- Use either **jQuery** or vanilla JavaScript.
- Your API key or bearer token **MUST NOT** be anywhere in your project source code or Git repository. It **absolutely may not** be sent to the client browser.
- You **may not** use any pre-built packages to implement AJAX for you, either server-side or client-side. It must be done with the basic tools covered in class.
- You **may not** use a library to access the TMDB API (e.g., `TMDbLib`). You must implement all access to the TMDB API yourself with primitive HTTP requests from C# and model classes you build yourself.
- No AJAX requests may be made from the client browser to TMDB directly. All data must come from your own server, which gets it from the TMDB API.
- Do not send the entire JSON data object that you receive from TMDB to the client. Process it server-side so that you only send what is needed for your application. Sending the whole object is bad practice, a data leak, and a security violation.

---

# Introduction

This homework focuses on creating a **responsive, single-page application**. An extreme case of this kind of application is the Google Mail client. While we won't aim for such complexity, we can still implement the main process.

The page that initially loads (from the server) doesn't include any data. It presents a minimal interface but loads your JavaScript, which then requests data from your server via asynchronous HTTP requests. The page content is built dynamically as the requests come back from the server. Users interact with the page, triggering more asynchronous calls to send and receive data from your server. 

This approach minimizes the time required to load HTML, CSS, and associated resources, building and rendering the DOM. By modifying the DOM with JavaScript instead, the page often becomes more responsive. This principle underlies modern frameworks like React.

It has become significantly easier to use JavaScript for this kind of functionality since Google's early implementations. For example:
- The [Fetch API](https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API) simplifies asynchronous requests.
- Alternatively, you can use [jQuery](https://jquery.com/).

A second purpose of this homework is to learn how to use a **professional/commercial API**. This skill will be necessary for team projects, where you're required to use an external API for core features.

---

# Application Theme

This application continues the **Movies and Shows** theme, using **The Movie Database (TMDB)** as the data source.  
- We **will not** use the database from HW3.
- This application **will not have a database** at all.

Instead of actor searches, this homework focuses on **movie searches**. The TMDB homepage has a great search feature—your task is to replicate part of it.

---

# Planning and Design

Before building the application, sketch out or whiteboard what you want.  
Here's an example plan:

1. **Homepage**:
   - A search bar and button.
   - Users type in the name or partial name of a movie and click "Search."
   
2. **Results**:
   - Display the top 10-20 results, ordered by popularity.
   - For each match, show a quick overview of the movie.
   - The goal is to present multiple results, allowing users to quickly find the movie they're looking for.

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
