# FleetGuard Architecture

## Overview

FleetGuard is a RESTful ASP.NET Core Web API that simulates an Enterprise Mobility Management (EMM) backend similar to products such as Microsoft Intune, VMware Workspace ONE, and SOTI MobiControl.

The application manages enterprise devices by allowing administrators to register, update, retrieve, delete, and monitor device health through REST APIs.

FleetGuard is built using **ASP.NET Core 10**, **Entity Framework Core**, and **SQLite**, while following modern backend development practices such as dependency injection, feature branching, pull requests, migrations, and layered architecture.

---

# System Architecture

```text
                        Client
              (Postman / Future React UI)
                         │
                         ▼
                 ASP.NET Core Web API
                         │
                         ▼
                  ASP.NET Routing
                         │
                         ▼
                DevicesController
                         │
                         ▼
              Entity Framework Core
                         │
                         ▼
               FleetGuardDbContext
                         │
                         ▼
                 SQLite Database
```

---

# Request Lifecycle

Every request follows the same lifecycle.

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

Example

```
POST /api/devices

↓

DevicesController

↓

Validate Request

↓

Check Duplicate Serial Number

↓

Create Device Object

↓

Save using Entity Framework

↓

SQLite Database

↓

201 Created
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
│     ├── UpdateDeviceRequest.cs
│     └── DeviceCheckInRequest.cs
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
│     ├── Architecture.md
│     └── images
│
├── fleetguard.db
├── Program.cs
├── appsettings.json
└── FleetGuard.csproj
```

---

# Folder Responsibilities

## Controllers

Responsible for receiving HTTP requests, executing business logic, interacting with the database, and returning HTTP responses.

Current Controller

- DevicesController

Responsibilities

- Register Device
- Retrieve Devices
- Update Devices
- Delete Devices
- Device Health Check-In

---

## Data

Contains the application's database context.

Current Class

- FleetGuardDbContext

Responsibilities

- Creates database connection
- Maps models to tables
- Executes LINQ queries
- Handles database changes

---

## Models

Represents database entities.

Current Model

- Device

Stores

- Device Information
- Platform
- Status
- Battery
- Security Information
- Health Information

---

## Requests

Contains Data Transfer Objects (DTOs).

Current DTOs

- RegisterDeviceRequest
- UpdateDeviceRequest
- DeviceCheckInRequest

Purpose

Incoming JSON is first converted into these objects before business logic is executed.

---

## Enums

Contains strongly typed constants.

Current Enums

- DevicePlatform
- DeviceStatus

Using enums prevents invalid values from being stored in the database.

---

## Migrations

Contains Entity Framework Core migrations.

Responsibilities

- Create database schema
- Update schema
- Version database changes

Current Migration

- InitialCreate

---

## Documentation

Contains technical project documentation.

Current Files

- API.md
- Architecture.md

Images

- Postman request screenshots
- Validation screenshots
- Device health screenshots

---

# Entity Relationship

```
Device
│
├── Id
├── DeviceName
├── SerialNumber
├── Platform
├── Status
├── OperatingSystemVersion
├── BatteryLevel
├── IsEncrypted
├── IsScreenLockEnabled
├── IsRootedOrJailbroken
├── IpAddress
├── LastCheckInAt
└── HealthMessage
```

---

# Database Architecture

FleetGuard uses Entity Framework Core as an Object Relational Mapper (ORM).

```text
Device Object

↓

DbContext

↓

Entity Framework Core

↓

SQL Query

↓

SQLite Database
```

Entity Framework automatically converts C# LINQ queries into SQL.

Example

```csharp
_context.Devices.ToListAsync();
```

becomes

```sql
SELECT * FROM Devices;
```

without writing SQL manually.

---

# Current API Endpoints

| Method | Endpoint | Purpose |
|---------|----------|---------|
|POST|/api/devices|Register Device|
|GET|/api/devices|Retrieve All Devices|
|GET|/api/devices/{id}|Retrieve Device|
|PUT|/api/devices/{id}|Update Device|
|DELETE|/api/devices/{id}|Delete Device|
|POST|/api/devices/{id}/check-in|Device Health Check|

---

# Device Health Workflow

FleetGuard evaluates every device whenever it performs a health check.

```
Device Check-In

↓

Battery

↓

Encryption

↓

Screen Lock

↓

Root Detection

↓

Health Evaluation

↓

Healthy
Warning
Critical
```

Health Rules

| Condition | Result |
|------------|---------|
|Battery <20%|Warning|
|Screen Lock Disabled|Warning|
|Not Encrypted|Critical|
|Rooted/Jailbroken|Critical|
|Everything OK|Healthy|

---

# Technologies

Backend

- ASP.NET Core 10
- C#

Database

- SQLite
- Entity Framework Core

Development Tools

- Visual Studio
- Postman
- DBeaver

Version Control

- Git
- GitHub
- Feature Branch Workflow
- Pull Requests

Documentation

- Markdown
- GitHub

---

# Development Workflow

```
Create Feature Branch

↓

Implement Feature

↓

Test Using Postman

↓

Inspect Database

↓

Update Documentation

↓

Commit Changes

↓

Push Branch

↓

Create Pull Request

↓

Review

↓

Merge into Main
```

This workflow was followed throughout FleetGuard development.

---

# Current Features

✅ Device Registration

✅ Retrieve All Devices

✅ Retrieve Device By ID

✅ Update Device

✅ Delete Device

✅ Request Validation

✅ Duplicate Serial Number Detection

✅ SQLite Persistence

✅ Entity Framework Core

✅ Device Health Check-In

✅ Device Health Evaluation

✅ Postman Testing

✅ DBeaver Verification

✅ Documentation

---

# Future Architecture

As FleetGuard evolves, business logic will move into dedicated service classes.

```text
               React Dashboard
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

The application will eventually be deployed to **Azure App Service**, replacing SQLite with **Azure SQL Database** for production use.

---

# Planned Features

- Service Layer
- Repository Pattern
- JWT Authentication
- Role-Based Authorization
- Global Exception Handling
- Logging
- Unit Testing
- Swagger Documentation
- Azure App Service Deployment
- Azure SQL Migration
- React Frontend Dashboard
- GitHub Actions CI/CD Pipeline
- Device Compliance Policies
- Device Audit Logs
- Device Location Tracking
- Push Notification Support

---

# Current Project Status

## Phase 1 — Backend Foundation ✅

- ASP.NET Core Web API
- REST API Design
- Entity Framework Core
- SQLite Integration
- CRUD Operations
- Validation
- Database Migrations
- Documentation

---

## Phase 2 — Enterprise Features ✅

- Duplicate Serial Validation
- Device Health Check-In
- Compliance Evaluation
- Device Status Monitoring

---

## Phase 3 — Next Sprint

- Service Layer
- Repository Pattern
- Global Exception Handling
- JWT Authentication
- Azure Deployment
- React Dashboard