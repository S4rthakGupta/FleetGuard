# FleetGuard

> Enterprise Mobility Management (EMM) Simulation Platform built with ASP.NET Core, Entity Framework Core, SQLite, Next.js, Azure App Service, GitHub Actions, and Vercel.

FleetGuard is a full-stack enterprise device management platform inspired by modern Enterprise Mobility Management (EMM) solutions.

The application demonstrates how organizations can securely manage, monitor, and diagnose enterprise devices through a cloud-hosted REST API and a modern web dashboard.

The project was built to simulate how enterprise IT teams manage thousands of company-owned devices while following real-world software engineering practices including Git feature branching, pull requests, CI/CD pipelines, cloud deployment, RESTful API design, and Entity Framework Core.

---

# Live Application

## Frontend

https://fleet-guard-three.vercel.app

Hosted using **Vercel**

---

## Backend API

https://fleetguard-api-sarthak-ckbchjgyh9f8exdr.canadacentral-01.azurewebsites.net

Hosted using **Microsoft Azure App Service**

---

## Health Endpoint

```
GET /health
```

Returns

```json
{
    "status":"healthy",
    "application":"FleetGuard API"
}
```

---

# Project Overview

FleetGuard consists of two independent applications.

```
Next.js Dashboard

↓

REST API

↓

ASP.NET Core Web API

↓

Entity Framework Core

↓

SQLite Database
```

The frontend communicates with the backend entirely through REST APIs.

The backend is responsible for

- Device registration
- Device management
- Health evaluation
- Compliance monitoring
- Diagnostic history
- Data persistence

The frontend focuses purely on presenting information and consuming backend APIs.

---

# Project Architecture

```
                        User

                         │

                         ▼

          Next.js Dashboard (Vercel)

                         │

               HTTP / JSON REST APIs

                         │

                         ▼

      ASP.NET Core 10 Web API (Azure)

                         │

                    Routing

                         │

                         ▼

               DevicesController

                         │

                         ▼

          Entity Framework Core

                         │

                         ▼

         Persistent SQLite Database

             (Azure App Service)
```

---

# Technology Stack

## Backend

- ASP.NET Core 10
- C#
- REST APIs
- Entity Framework Core
- LINQ
- Dependency Injection

### Why ASP.NET Core?

ASP.NET Core provides a modern, cross-platform framework for building scalable REST APIs with built-in dependency injection, middleware support, routing, model validation, and high performance.

It closely aligns with enterprise software development practices.

---

## Frontend

- Next.js
- React
- TypeScript
- CSS
- Lucide React

### Why Next.js?

Next.js provides an excellent developer experience with server-side rendering, routing, optimized production builds, and excellent performance.

The dashboard consumes the backend API without requiring page reloads.

---

## Database

- SQLite
- Entity Framework Core

### Why SQLite?

SQLite provides a lightweight relational database requiring zero configuration.

For this project it allows rapid development while still demonstrating

- Entity Framework Core
- Migrations
- LINQ
- Relational database concepts

The SQLite database is stored persistently inside Azure App Service rather than locally.

---

## Cloud

- Microsoft Azure App Service
- GitHub Actions
- Vercel

### Azure App Service

The backend API is deployed to Azure App Service.

Benefits include

- Production hosting
- HTTPS
- Cloud accessibility
- Automatic deployment
- Persistent storage
- Easy scaling

---

### GitHub Actions

Every push to the main branch automatically

- Builds the project
- Publishes the API
- Deploys to Azure

No manual deployment is required.

---

### Vercel

The frontend dashboard is hosted using Vercel.

Advantages

- Global CDN
- Fast deployments
- Automatic builds
- Environment variable management
- HTTPS by default

---

# Current Features

## Device Management

✔ Register Device

✔ View Devices

✔ Retrieve Device Details

✔ Update Device

✔ Delete Device

---

## Health Monitoring

✔ Device Health Check-In

✔ Automatic Health Evaluation

✔ Healthy Status

✔ Warning Status

✔ Critical Status

✔ Battery Monitoring

✔ Encryption Monitoring

✔ Screen Lock Monitoring

✔ Root / Jailbreak Detection

---

## Dashboard

✔ Enterprise Dashboard

✔ Device Inventory

✔ Search Devices

✔ Status Filtering

✔ Device Details Panel

✔ Diagnostic History

✔ Register Device Modal

✔ Refresh Dashboard

---

## Backend

✔ RESTful API

✔ CRUD Operations

✔ Validation

✔ Duplicate Serial Number Detection

✔ Entity Framework Core

✔ Dependency Injection

✔ Database Migrations

✔ SQLite Persistence

✔ Cloud Deployment

---

## DevOps

✔ Git

✔ GitHub

✔ Feature Branch Workflow

✔ Pull Requests

✔ GitHub Actions CI/CD

✔ Azure Deployment

✔ Vercel Deployment

---

# Device Health Evaluation

Every device periodically performs a health check.

FleetGuard evaluates the following conditions.

Priority order

```
Rooted Device
↓

Critical

------------------

Not Encrypted

↓

Critical

------------------

Screen Lock Disabled

↓

Warning

------------------

Battery Below 20%

↓

Warning

------------------

Everything OK

↓

Healthy
```

---

# REST API

Current endpoints

| Method | Endpoint |
|----------|----------------------------|
| GET | /health |
| POST | /api/devices |
| GET | /api/devices |
| GET | /api/devices/{id} |
| PUT | /api/devices/{id} |
| DELETE | /api/devices/{id} |
| POST | /api/devices/{id}/check-in |

Detailed API documentation is available inside

```
docs/API.md
```

---

# Database

FleetGuard currently stores

- Device Information
- Device Status
- Battery Information
- Encryption State
- Screen Lock State
- Root Detection
- Diagnostic History
- Last Check-In Timestamp

using

Entity Framework Core + SQLite.

The database is persisted inside Azure App Service storage so information remains available after deployments.

---

# Project Structure

```
FleetGuard

├── Controllers
├── Data
├── Models
├── Requests
├── Enums
├── Migrations
├── docs
├── fleetguard-ui
├── Program.cs
├── appsettings.json
└── FleetGuard.csproj
```

---

# Development Workflow

```
Create Feature Branch

↓

Develop Feature

↓

Test using Postman

↓

Verify Database using DBeaver

↓

Commit Changes

↓

Push to GitHub

↓

Create Pull Request

↓

Review Changes

↓

Merge into Main

↓

GitHub Actions

↓

Deploy to Azure

↓

Production API Updated

↓

Vercel Dashboard Uses Latest API
```

---

# Why These Technologies?

| Technology | Reason |
|------------|--------|
| ASP.NET Core | Enterprise REST API framework |
| C# | Strongly typed backend language |
| Entity Framework Core | ORM for database operations |
| SQLite | Lightweight relational database |
| LINQ | Simplifies querying without raw SQL |
| Next.js | Modern frontend framework |
| React | Component-based UI |
| TypeScript | Safer frontend development |
| Azure App Service | Cloud hosting for backend |
| GitHub Actions | Continuous Integration / Deployment |
| Vercel | Frontend hosting |
| Git | Version Control |
| GitHub | Collaboration & Pull Requests |
| Postman | API Testing |
| DBeaver | Database Inspection |

---

# Future Improvements

FleetGuard has been designed to evolve toward a production-grade enterprise application.

Planned improvements include

## Architecture

- Service Layer
- Repository Pattern
- CQRS
- MediatR
- Unit of Work Pattern

---

## Security

- JWT Authentication
- Refresh Tokens
- Role-Based Authorization (RBAC)
- API Key Authentication
- Identity Framework
- Password Hashing
- Multi-Factor Authentication (MFA)

---

## Cloud

- Azure SQL Database
- Azure Blob Storage
- Azure Key Vault
- Azure Application Insights
- Azure Monitor
- Azure Log Analytics

---

## Performance

- Redis Caching
- Response Compression
- Pagination
- Background Workers
- Distributed Caching

---

## Monitoring

- Structured Logging
- Serilog
- Centralized Logs
- Exception Tracking
- Performance Metrics

---

## Testing

- MSTest
- xUnit
- Moq
- Integration Testing
- End-to-End Testing
- Automated API Testing

---

## Documentation

- Swagger / OpenAPI
- XML Documentation
- API Versioning

---

## Enterprise Features

- Device Compliance Policies
- Device Groups
- User Management
- Organization Management
- Device Audit Logs
- Activity History
- Device Location Tracking
- Push Notifications
- Scheduled Device Check-Ins
- Remote Device Actions
- Remote Lock
- Remote Wipe
- Compliance Reporting

---

## DevOps

- Docker
- Kubernetes
- Azure Container Apps
- Terraform
- Infrastructure as Code
- Blue/Green Deployment
- Canary Deployment

---

# Learning Outcomes

This project demonstrates practical experience with

- REST API Development
- ASP.NET Core
- Entity Framework Core
- LINQ
- Database Design
- Cloud Deployment
- Azure App Service
- GitHub Actions
- CI/CD
- Full Stack Development
- React
- Next.js
- TypeScript
- Feature Branch Workflow
- Pull Requests
- Software Documentation
- Enterprise Device Management Concepts

---

# License

This project is intended for educational and portfolio purposes.
