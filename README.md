# Job Tracker API

A RESTful Job Application Tracker API built with ASP.NET Core .NET 10.

It supports creating, reading, updating, and deleting job application records. The API uses Entity Framework Core and is deployed to Azure App Service with Azure SQL Database.

## Live Demo

Public read-only demo endpoint:

[https://jobtracker-api-jye-2026-cjhdc9ancmh4gadn.southeastasia-01.azurewebsites.net/api/jobapplications](https://jobtracker-api-jye-2026-cjhdc9ancmh4gadn.southeastasia-01.azurewebsites.net/api/jobapplications)

## Tech Stack

- C#
- ASP.NET Core .NET 10
- Entity Framework Core
- SQLite for local development
- Azure SQL Database for cloud deployment
- Azure App Service
- xUnit integration tests
- Swagger UI for local API testing

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/jobapplications` | Get all job applications |
| GET | `/api/jobapplications/{id}` | Get one application by ID |
| POST | `/api/jobapplications` | Create a job application |
| PUT | `/api/jobapplications/{id}` | Update a job application |
| DELETE | `/api/jobapplications/{id}` | Delete a job application |

## Example Job Application

```json
{
  "companyName": "Azure Demo Company",
  "positionTitle": ".NET Developer",
  "status": "Applied",
  "dateApplied": "2026-08-07",
  "jobUrl": "https://azure.microsoft.com",
  "notes": "Created through the deployed Azure API"
}