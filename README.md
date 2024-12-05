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

# Summary of the Feature

**As a movie enthusiast**,  
I want to find information about a movie, such as runtime, rating, revenue generated, and cast members,  
So I can learn about it, compare it to other movies, and discover new actors.  
I'd like it to be faster and easier than other methods such as looking it up on IMDb or Wikipedia.

---

# Tasks and Steps

### Setup

1. **Start by creating a normal MVC Core web application**.  
   Simplify it by deleting the Privacy page.
2. **Follow the Terms of Service for TMDB**:  
   - Find the required statement and put it in your About page.  
   - Download one of the required TMDB logos and place it in your About page or at the bottom of your Index page.
3. **Create a TMDB account**:  
   - Go to [TMDB](https://www.themoviedb.org/) and create an account.  
   - Request an API key under Settings → API. Use the "API Read Access Token."  
   - Add it as a secret in your project using `dotnet user-secrets`. Access it through the Configuration object in `Program.cs`.

---

### Modeling and Requirements

- **Modeling**: Review the provided sequence diagram at the bottom of this page to understand the flow for a typical (successful) use case. Ensure it makes sense before implementing.
- **Watch the assigned video** to understand all requirements for building the application.

---

### Feature #1: Search for Movies by Title

1. Use the `/search/movie` endpoint from TMDB.
2. Display the first page of results, sorted by popularity (descending order).
3. Include the following information:
   - Poster image
   - Title
   - Release date (formatted as "Month Day, Year")
   - Description (truncate to ~140 characters if necessary)
4. Results should be presented in a condensed format:
   - Avoid increasing the height of individual elements unnecessarily.
   - Optionally write custom CSS instead of relying on Bootstrap cards.

---

### Feature #2: Display Movie Details in a Modal

1. Use the endpoints `/movie/{id}` and `/movie/{id}/credits`.
2. Include the following information in the modal:
   - Background image (different from the poster)
   - Title
   - Year
   - Release date
   - Genres
   - Runtime (formatted in hours and minutes)
   - Popularity
   - Revenue (formatted in USD currency)
   - Full overview/description
   - Cast (actor names and roles, if available)

---

### Testing

1. **Unit Testing**: Write tests for the following:
   - Transforming runtime in minutes to a string in hours and minutes (e.g., `88 → "1 hour 28 minutes"`).
   - Transforming release dates into the format "Month Day, Year" (e.g., `"1980-07-25 → July 25, 1980"`).
   - These tests should be written in a separate test project using NUnit.
2. **Manual Testing**: Verify functionality through the UI:
   - Ensure all use cases function without errors, including browser console errors.
   - Simulate failure scenarios:
     - Corrupt your TMDB bearer token to test authentication handling.
     - Alter the URL to simulate a network failure or disconnect your network.
     - Ensure proper exception handling for JSON deserialization issues (e.g., unexpected JSON format).

---

### Turn It In

1. Submit your project on GitHub.
2. Upload a video demo/code walkthrough to Canvas.
