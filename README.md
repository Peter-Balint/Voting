# Voting

//In progress//

A simple voting web application that allows registered users to view and vote on active polls, as well as post new ones and see the results of finished ones.
Originally started as part of the course "Modern webalkalmazások fejelesztése .NET környezetben" at ELTE.

Made of an ASP.NET webapi, using Entity Framework ORM (local MSSSQL database) with code-first approach for persistence, and a Blazor WASM app serving as the frontend. Proper user authentication and authorization is ensured by the ASP.NET Identity framework.

Only brought to GitHub recently, which is why the commit history is relatively empty.

Setup: The projects Voting.WebAPI and Voting.Blazor should be ran together. Ports and the database connection string can be modified in their respective appsettings.json files, should the default values be unsuitable.
User account registered for testing: user@example.com, pw: user@123
