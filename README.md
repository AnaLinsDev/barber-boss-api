# Barber Boss API 

A RESTful Web API built with **C# and ASP.NET Core** for manage a barber's charges.

## Technologies
![badge-dot-net]
![badge-windows]
![badge-visual-studio]
![badge-postgresql]
![badge-swagger]

## Features

- CRUD expenses
- Download Excel report by month
- Download PDF report by month
- Business rule validation
- Status and Priority validation
- Exception filter
- Swagger documentation (HTTP status code handling)

## Layered Architecture

For this project, the following layered architecture was adopted:

1. BarberBoss.API — The entry point of the application, responsible for handling HTTP requests through the controllers.
2. BarberBoss.Application — The application/service layer, responsible for implementing the application's use cases and business rules.
3. BarberBoss.Communication — The DTO layer, responsible for defining and organizing the request and response objects used to communicate between the API and the application layer.
4. BarberBoss.Exception — The Exception layer, responsible for defining and organizing the aplication errors and saving the resourse message errors.
5. BarberBoss.Infrastructure — The Infrastructure layer, responsible for defining and organizing the database communication.
6. BarberBoss.Domain — The Domain layer, responsible for defining the entities and interfaces used in the Infrastructure layer.

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/reports` | Get all reports |
| GET | `/api/reports/{id}` | Get a report by ID |
| GET | `/api/reports/excel?month=2026-09` | Get reports by the year-month and download the Excel report  |
| GET | `/api/reports/pdf?month=2026-09` | Get reports by the year-month and download the PDF report  |
| POST | `/api/reports` | Create a report |
| PUT | `/api/reports/{id}` | Update a report |
| DELETE | `/api/reports/{id}` | Delete a report |


## What I Practiced

Through this project, I practiced:

- Layered architecture in .NET
- ASP.NET Core Web API
- RESTful API design
- HTTP methods and status codes
- Business rule implementation
- Exception handling
- Debugging with Visual Studio
- API documentation with Swagger
- Implement dependency injection
- How to use the MigraDoc to create and style a PDF report
- How to use the ClosedXML to create and style an Excel report

## Next Steps

- Create the Migrations
- Implement unit tests
- Add authentication
- Add authorization for specific endpoints

## How to run

### Requirements
* Visual Studio version 2022+ or Visual Studio Code
* Windows 10+ or ​​Linux/MacOS with .NET SDK 8.0 or 9.0 installed
* PostgreSQL

### Installation

1. Clone the repository:

```bash
git clone https://github.com/AnaLinsDev/barber-boss-api.git
```

### 2. Navigate to the project

```bash
cd barber-boss-api
```

### 3. Add your DB_PATH

Fill in the database information in the `appsettings.Development.json` file inside the BarberBoss.API.

### 4. Restore dependencies

```bash
dotnet restore
```

### 5. Build the project

```bash
dotnet build
```

### 6. Creating database tables

// Here will be added the steps to run the migrations, but it will be added in the next steps

### 7. Run the API

```bash
dotnet run --project src/BarberBoss.API
```

The terminal will display the URL where the API is running.

### 8. Open Swagger

Open the Swagger URL displayed by the application in your browser.

Swagger can be used to test the available API endpoints without requiring Postman or another API client.

<!-- Badges -->
[badge-dot-net]: https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge
[badge-windows]: https://img.shields.io/badge/Windows-0078D4?logo=windows&logoColor=fff&style=for-the-badge
[badge-visual-studio]: https://img.shields.io/badge/Visual%20Studio-5C2D91?logo=visualstudio&logoColor=fff&style=for-the-badge
[badge-postgresql]: https://img.shields.io/badge/PostgreSQL-4169E1?logo=postgresql&logoColor=fff&style=for-the-badge
[badge-swagger]: https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=for-the-badge
