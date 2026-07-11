# FleetGuard Architecture

## Overview

FleetGuard is a RESTful ASP.NET Core Web API designed to manage enterprise devices such as Android, iOS, Windows, Linux, printers, and IoT devices.

The application follows a layered architecture using ASP.NET Core, Entity Framework Core, and SQLite while following modern software engineering practices including Git feature branching, pull requests, code reviews, and documentation.

---

# High-Level Architecture

```text
                 Client
      (Postman / Future React UI)
                    │
                    ▼
        ASP.NET Core Web API
                    │
                    ▼
         ASP.NET Core Routing
                    │
                    ▼
          DevicesController
                    │
                    ▼
         FleetGuardDbContext
                    │
                    ▼
      Entity Framework Core
                    │
                    ▼
            SQLite Database
```

---

# Request Lifecycle

When a request reaches the API, the following sequence occurs.

```text
HTTP Request
      │
      ▼
Routing
      │
      ▼
DevicesController
      │
      ▼
Model Validation
      │
      ▼
Business Logic
      │
      ▼
Entity Framework Core
      │
      ▼
SQLite Database
      │
      ▼
JSON Response
```

---

# Project Structure

```
FleetGuard
│
├── Controllers
│     └── DevicesController.cs
│
├── Data
│     └── FleetGuardDbContext.cs
│
├── Models
│     └── Device.cs
│
├── Requests
│     ├── RegisterDeviceRequest.cs
│     └── UpdateDeviceRequest.cs
│
├── Enums
│     ├── DevicePlatform.cs
│     └── DeviceStatus.cs
│
├── Migrations
│     ├── InitialCreate.cs
│     └── FleetGuardDbContextModelSnapshot.cs
│
├── docs
│     ├── API.md
│     └── Architecture.md
│
├── fleetguard.db
├── Program.cs
├── appsettings.json
└── FleetGuard.csproj
```

---

# Folder Responsibilities

## Controllers

Responsible for handling HTTP requests and returning HTTP responses.

Current controller

- DevicesController

---

## Data

Contains the application's database context.

Current class

- FleetGuardDbContext

Responsibilities

- Configures Entity Framework
- Maps models to database tables
- Manages database connections

---

## Models

Represents database entities.

Current model

- Device

---

## Requests

Contains Data Transfer Objects (DTOs) used to receive client requests.

Current DTOs

- RegisterDeviceRequest
- UpdateDeviceRequest

---

## Enums

Stores strongly typed application constants.

Current enums

- DevicePlatform
- DeviceStatus

---

## Migrations

Contains Entity Framework Core migrations used to create and update the database schema.

---

## docs

Contains project documentation.

- API.md
- Architecture.md

---

# Database Architecture

FleetGuard uses SQLite together with Entity Framework Core.

```text
Device Model
      │
      ▼
FleetGuardDbContext
      │
      ▼
Entity Framework Core
      │
      ▼
SQLite Database
```

Entity Framework automatically translates LINQ queries into SQL.

---

# Current Database

Current Tables

- Devices
- __EFMigrationsHistory

Each device stores

- Id (GUID)
- Device Name
- Serial Number
- Platform
- Status
- Operating System Version

---

# Current API Endpoints

| Method | Endpoint | Description |
|---------|----------|-------------|
| POST | /api/devices | Register a device |
| GET | /api/devices | Retrieve all devices |
| GET | /api/devices/{id} | Retrieve a device |
| PUT | /api/devices/{id} | Update a device |
| DELETE | /api/devices/{id} | Delete a device |

---

# Validation

Current validation includes

- Required fields
- Duplicate serial number prevention
- Device existence checks
- Automatic model validation using Data Annotations

---

# Technologies

- ASP.NET Core 10
- C#
- Entity Framework Core
- SQLite
- Git
- GitHub
- Postman
- DBeaver
- Markdown Documentation

---

# Current Development Workflow

```text
Feature Branch
       │
       ▼
Development
       │
       ▼
Postman Testing
       │
       ▼
Documentation
       │
       ▼
Commit
       │
       ▼
Push
       │
       ▼
Pull Request
       │
       ▼
Merge into Main
```

---

# Future Architecture

As FleetGuard grows, the architecture will evolve into a more scalable layered design.

```text
                 React Frontend
                        │
                        ▼
               ASP.NET Core API
                        │
                        ▼
              Service Layer
                        │
                        ▼
            Repository Layer
                        │
                        ▼
         Entity Framework Core
                        │
                        ▼
                Azure SQL Database
```

The application will eventually be deployed to Azure App Service.

---

# Planned Features

- Service Layer
- Repository Pattern
- JWT Authentication
- Role-Based Authorization
- Device Health Monitoring
- Device Compliance Tracking
- Logging
- Global Exception Handling
- Unit Testing
- Azure App Service Deployment
- Azure SQL Migration
- React Dashboard
- CI/CD using GitHub Actions

---

# Current Development Status

## Completed

- ASP.NET Core Web API
- RESTful API Design
- SQLite Database Integration
- Entity Framework Core
- Database Migrations
- Device Registration
- Retrieve All Devices
- Retrieve Device by ID
- Update Device
- Delete Device
- Request Validation
- Duplicate Serial Number Validation
- Feature Branch Workflow
- Pull Request Workflow
- Postman Testing
- DBeaver Database Inspection
- API Documentation
- Architecture Documentation

---

## Next Sprint

- Service Layer
- Repository Pattern
- Dependency Injection Improvements
- Global Exception Handling
- Azure Deployment