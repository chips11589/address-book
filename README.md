# README #

Demo: http://web.chipsvien.uk/contact

### What is this repository for? ###

* This is a simple Address Book using .NET 6, SignalR Core 3.1 and AngularJs 8.2.12
* Solution structure follows Clean Architecture (https://github.com/jasontaylordev/CleanArchitecture), with CQRS + MediatR
* Full-text search - FREETEXT predicate is used to generate the autocomplete suggestions for the search box (https://docs.microsoft.com/en-us/sql/t-sql/queries/freetext-transact-sql)
* .NET 6 provides back-end Web API, with CORS enabled
* SignalR Core provides Tag CRUD notification and tag-list synchronisation among user sessions
* AngularJs 8.2.12 provides front-end templating. Front-end project utilises modular structure, components and directives in order to maximise the code reusability and logical separation
* RxJs Observables / Subjects are widely used in order to facilitate communication among app components
* Automapper is used to map objects from Entity to DTO and vice versa
* Dapper is used for some frequently called database requests in order to improve query performance and flexibly map query results to custom objects. Note that Entity Framework Core also supports custom object mapping, however, the custom object type needs to be explicitly declared as a DbSet of the DbContext, which is not desirable in this case.

### How are these services being deployed?
* The services are deployed as docker containers on Google Cloud free instances.
* Docker compose is used to initialise the services and set up the internal networks.
* Nginx is used as a reverse proxy to redirect the traffic to the appropriate services based on request domain.

### How do I get set up locally? ###

* Precondition: .NET 6 or above, SQL Server (with Full-Text Search enabled) and NodeJs have been installed on the server(s)
* Update the ConnectionStrings:DefaultConnection in the WebAPI\appsettings.json
* Set WebAPI as the startup project, run it to trigger EF Core migrations and schema SQL file execution. Or run ef core command to start the migrations manually:  `dotnet ef database update --project Infrastructure\Infrastructure.csproj --startup-project WebAPI\WebAPI.csproj`
* Update the _baseURI in the Web\ClientApp\app\shared\utils\config.service.ts to match the applicationUrl of WebAPI project
* Run the Web project, type in some sample searching key words like "sam, wil or wel..." and play around! You may want to open multiple browser sessions to see how SignalR is used
* This app has been tested and working on Chrome, Firefox and Edge (Not IE).
