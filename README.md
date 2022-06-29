# AFS Yoda Translator
This is a simple ASP.NET MVC application that takes a text string from the user and turns it
into yoda text.

## Install
- Restore dependencies. [Dotnet Cli](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-dotnet-cli)
- Inside AFS.App, update connection string inside appsettings.json .
- Inside AFS.App, create database using `dotnet ef database update` [Applying Migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli)