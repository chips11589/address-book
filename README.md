# README #

Demo: https://app-address-book.herokuapp.com

### What is this repository for? ###

* This is a simple Address Book using ASP.NET Core 2.0, SignalR Core 1.0.0-alpha2-final and AngularJs 4.4.6
* Full-text search - FREETEXT predicate is used to generate the autocomplete suggestions for the search box (https://docs.microsoft.com/en-us/sql/t-sql/queries/freetext-transact-sql)
* ASP.NET Core 2.0 provides back-end Web API, with CORS enabled
* SignalR Core provides Tag CRUD notification and tag-list synchronisation among user sessions
* AngularJs 4.4.6 provides front-end templating. Front-end project utilises modular structure, components and directives in order to maximise the code reusability and logical separation
* RxJs Observables / Subjects are widely used in order to facilitate communication among app components
* Automapper is used to map objects from Entity to DTO and vice versa
* Dapper is used for some frequently called database requests in order to slightly improve performance and flexibly map query results to custom objects. Note that Entity Framework Core also supports custom object mapping, however, the custom object type needs to be explicitly declared as a DbSet of the DbContext, which is not wanted in this case

### How do I get set up? ###

* Precondition: .NET Core 2.0, SQL Server (with Full-Text Search enabled) and NodeJs are installed
* Create a blank database, i.e. AddressBook
* Execute the DataAccess\SQL\CreateScripts\1.00 AddressBookCreate.sql against the newly created database
* Correct the ConnectionStrings:DefaultConnection in the WebAPI\appsettings.json
* Update the _baseURI in the Web\ClientApp\app\shared\utils\config.service.ts to match the applicationUrl of WebAPI project
* Run the Web project, type in some sample searching key words like "sam, wil or wel..." and play around! You may want to open multiple browser sessions to see how SignalR is used
* This has been tested and worked well on Chrome