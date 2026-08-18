# Job Tracker API

[![.NET CI](https://github.com/anatomy08/job-tracker-api/actions/workflows/dotnet-ci.yml/badge.svg)](https://github.com/anatomy08/job-tracker-api/actions/workflows/dotnet-ci.yml)

A RESTful Job Application Tracker API built with ASP.NET Core .NET 10.

The project demonstrates CRUD API development, DTO validation, Entity Framework Core, automated integration testing, Azure SQL Database, and Azure App Service deployment.

## Live Demo

Public read-only endpoint:

[https://jobtracker-api-jye-2026-cjhdc9ancmh4gadn.southeastasia-01.azurewebsites.net/api/jobapplications](https://jobtracker-api-jye-2026-cjhdc9ancmh4gadn.southeastasia-01.azurewebsites.net/api/jobapplications)

The deployed portfolio demo allows public `GET` requests only. Write requests are blocked in production to protect the demo database.

## Tech Stack

- C#
- ASP.NET Core .NET 10
- Entity Framework Core
- Azure SQL Database
- Azure App Service
- SQLite for local development
- xUnit integration tests
- Swagger UI for local API exploration
- Git and GitHub

## API Endpoints

| Method | Endpoint | Local development | Public Azure demo |
|---|---|---:|---:|
| GET | `/api/jobapplications` | Available | Available |
| GET | `/api/jobapplications/{id}` | Available | Available |
| POST | `/api/jobapplications` | Available | Blocked (`403`) |
| PUT | `/api/jobapplications/{id}` | Available | Blocked (`403`) |
| DELETE | `/api/jobapplications/{id}` | Available | Blocked (`403`) |

## Example Response

```json
{
  "id": 1,
  "companyName": "Azure Demo Company",
  "positionTitle": ".NET Developer",
  "status": "Applied",
  "dateApplied": "2026-08-07",
  "jobUrl": "https://azure.microsoft.com",
  "notes": "Created through the deployed Azure API",
  "createdAt": "2026-08-06T19:56:24.8985835Z",
  "updatedAt": null
}
