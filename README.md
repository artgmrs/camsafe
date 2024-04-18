# CamSafe Web API

CamSafe is focused on assuring security and well-being for all people.

## Stack

- .NET 8, Dapper, SQL Server, xUnit, Docker

## Running the application with local database and Visual Studio for the API

1. To start the SQL Server instance, run the command `docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Password@1" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest`
2. Go to the folder `CamSafe` and set the connection string properly at `appsettings.Development.json`
3. In Visual Studio, run the API with profile 'CamSafe'

## Running the application with docker compose

The API and its ecosystem are configured via docker.

1. Clone the repo
2. Go to the root folder
3. Run `docker compose up --build` and everything will be up.
4. Test the API with swagger at http://localhost:8080/swagger/index.html

## Unit Testing

To execute the unit tests you can follow these steps:

1. Navigate to the root folder
2. Run `dotnet test`

### Database scripts

There are several important scripts that are running with docker compose. Here are the scripts and their jobs:

- `Init_CamSafeDb.sh` -> Coordinate the execution of all scripts.
- `01-DDL_Create_Database.sql` -> Create the CamSafeDb database.
- `02-DDL_Config_Tables_CamSafeDB.sql` -> Create and configure all the tables and relationships.
- `03-Insert_Mock_data.sql` -> Insert mock data at the initialization to facilitate testing.
