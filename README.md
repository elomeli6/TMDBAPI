[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/KBdIuaWG)

dotnet new mvc --output homework4/movieDatabaseAPI --framework net8.0 --auth None --use-program-main true
dotnet new sln -o homework4
dotnet sln homework4 add homework4/movieDatabaseAPI
dotnet new nunit --output homework4/movieDatabaseAPI_Tests --framework net8.0
dotnet sln homework4 add homework4/movieDatabaseAPI_Tests
dotnet add homework4/movieDatabaseAPI_Tests reference homework4/movieDatabaseAPI

curl -X GET "https://api.themoviedb.org/3/search/movie?query=titanic" \
     -H "Authorization: Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJkZDljYTNkNDkwOWZmNTI0NDY0MmI0NGZmZjIxOGQyZSIsIm5iZiI6MTczMjMyNzAzMi4zMTg0MTI1LCJzdWIiOiI2NzNlMjBjOTg3OTE3MDc4ZDAxMDlhZDMiLCJzY29wZXMiOlsiYXBpX3JlYWQiXSwidmVyc2lvbiI6MX0.vl-jq0hSdzTbXfiwYotb04W-3mCeGKO4YX7ZbS-CN34" \
     -H "Accept: application/json"