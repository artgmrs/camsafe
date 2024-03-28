# CamSafe Web API

CamSafe is focused on assuring security and well-being for all people.

## Stack

- .NET 8, Dapper, SQL Server, xUnit, Docker

## Building and running the application

The API and its ecosystem are configured via docker. 

1 - Clone the rep
2 - Go to the root folder `src`
3 - Run `docker compose up --build` and everything will be up.
4 - Test the API with swagger at http://localhost:8080/swagger/index.html

## Script to configure and populate the Database

There are several important scripts that are running with docker compose. Here are the scripts and their jobs:

- `init-camsafedb.sh` -> Coordinate the execution of all scripts.
- `01-DDL_Create_Database.sql` -> Create the CamSafeDb database.
- `02-DDL_Config_Tables_CamSafeDB.sql` -> Create and configure all the tables and relationships.
- `03-Insert_Mock_data.sql` -> Insert mock data at the initialization to facilitate testing.
