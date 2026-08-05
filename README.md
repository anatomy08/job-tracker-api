# Job Tracker API

A RESTful ASP.NET Core Web API for tracking job applications. Built as a hands-on .NET backend learning project.

## Features

* Create, read, update, and delete job applications
* Local SQLite database with Entity Framework Core
* Input validation for required fields, status, and job URLs
* Server-generated `CreatedAt` and `UpdatedAt` timestamps
* Swagger UI for interactive API documentation and testing
* Database migrations for schema changes

## Tech Stack

* C#
* .NET 10 / ASP.NET Core Web API
* Entity Framework Core
* SQLite
* Swagger / OpenAPI
* Visual Studio 2022
* Git and GitHub

## API Endpoints

| Method | Endpoint                    | Purpose                       | Success response |
| ------ | --------------------------- | ----------------------------- | ---------------- |
| GET    | `/api/jobapplications`      | Get all job applications      | `200 OK`         |
| GET    | `/api/jobapplications/{id}` | Get one job application by ID | `200 OK`         |
| POST   | `/api/jobapplications`      | Create a job application      | `201 Created`    |
| PUT    | `/api/jobapplications/{id}` | Update a job application      | `200 OK`         |
| DELETE | `/api/jobapplications/{id}` | Delete a job application      | `204 No Content` |

The API also returns `400 Bad Request` for invalid request data and `404 Not Found` when an ID does not exist.

## Job Application Data

```json
{
  "companyName": "Nintendo",
  "positionTitle": "Gameplay Programmer",
  "status": "Applied",
  "dateApplied": "2026-08-05",
  "jobUrl": "https://www.nintendo.com/careers",
  "notes": "Created through Swagger"
}
```

Allowed status values:

```text
Applied
Interviewing
Offered
Rejected
```

## Run Locally

### Prerequisites

* .NET 10 SDK
* Visual Studio 2022 or another C# editor

### 1. Clone the repository

```powershell
git clone https://github.com/anatomy08/job-tracker-api.git
cd job-tracker-api
```

### 2. Apply database migrations

```powershell
dotnet ef database update --project .\JobTracker.Api\JobTracker.Api.csproj --startup-project .\JobTracker.Api\JobTracker.Api.csproj
```

### 3. Run the API

```powershell
dotnet run --project .\JobTracker.Api\JobTracker.Api.csproj
```

Or open `JobTracker.sln` in Visual Studio and press **F5**.

## Swagger UI

While the API is running, open:

```text
https://localhost:7281/swagger
```

Swagger UI shows each endpoint, its request body, possible response codes, and allows interactive testing.

> Use the HTTPS port shown by Visual Studio or the terminal if it differs from `7281`.

## Database Migrations

When a database model changes:

```text
Update the C# model
→ Build the project
→ Create a migration
→ Review the migration
→ Update the local database
```

Example:

```powershell
dotnet ef migrations add MigrationName --project .\JobTracker.Api\JobTracker.Api.csproj --startup-project .\JobTracker.Api\JobTracker.Api.csproj

dotnet ef database update --project .\JobTracker.Api\JobTracker.Api.csproj --startup-project .\JobTracker.Api\JobTracker.Api.csproj
```

## What I Learned

```text
OOP                 → JobApplication and AppDbContext classes
ASP.NET Core        → controllers, routes, HTTP requests
Entity Framework    → C# objects ↔ SQLite database rows
REST API design     → GET, POST, PUT, DELETE, and status codes
Validation          → [Required], [Url], and [RegularExpression]
DTOs                → request data shapes accepted from the frontend
Migrations          → versioned database schema changes
Swagger / OpenAPI   → interactive API documentation and testing
```
