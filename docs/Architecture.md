# FleetGuard Architecture

## Overview

FleetGuard is a full-stack Enterprise Mobility Management (EMM) application inspired by enterprise mobility solutions such as **SOTI MobiControl**, **Microsoft Intune**, and **VMware Workspace ONE**.

The project demonstrates how enterprise devices can be enrolled, monitored, managed, and evaluated for compliance through a RESTful ASP.NET Core API and a modern Next.js dashboard.

FleetGuard follows a layered architecture using **ASP.NET Core 10**, **Entity Framework Core**, **SQLite**, **Next.js**, **TypeScript**, **Azure App Service**, and **GitHub Actions CI/CD**.

---

# Overall System Architecture

```text
                 Next.js Dashboard
                        │
                 HTTP / JSON API
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
          Entity Framework Core (ORM)
                        │
                        ▼
             FleetGuardDbContext
                        │
                        ▼
                 SQLite Database
```

---

# Cloud Architecture

```text
             GitHub Repository
                    │
            GitHub Actions CI/CD
                    │
                    ▼
          Azure App Service
                    │
                    ▼
          FleetGuard REST API
                    │
                    ▼
             SQLite Database
```

The backend is deployed to Azure App Service while the frontend communicates with the deployed API using REST endpoints.

---

# Request Lifecycle

Every request follows the same flow.

```text
HTTP Request

↓

ASP.NET Routing

↓

DevicesController

↓

Request Validation

↓

Business Rules

↓

Entity Framework Core

↓

SQLite Database

↓

JSON Response
```

Example

```text
POST /api/devices

↓

Validate Request

↓

Check Duplicate Serial Number

↓

Create Device

↓

Save Changes

↓

Return 201 Created
```

---

# Device Check-In Lifecycle

FleetGuard evaluates device compliance whenever a managed device performs a check-in.

```text
Device Check-In

↓

Receive Device Status

↓

Battery Evaluation

↓

Encryption Evaluation

↓

Screen Lock Evaluation

↓

Root Detection

↓

Generate Health Status

↓

Store Diagnostic Log

↓

Return Updated Device
```

Every check-in creates a historical diagnostic record that can later be viewed inside the dashboard.

---

# Project Structure

```text
FleetGuard
│
├── Controllers
│     └── DevicesController.cs
│
├── Data
│     └── FleetGuardDbContext.cs
│
├── Models
│     ├── Device.cs
│     └── DiagnosticsLog.cs
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
│
├── docs
│
├── fleetguard-ui
│     ├── src
│     ├── lib
│     ├── types
│     └── app
│
├── Program.cs
├── appsettings.json
└── FleetGuard.csproj
```

---

# Layer Responsibilities

## Controllers

Responsible for exposing REST endpoints.

Current controller

- DevicesController

Responsibilities

- Register devices
- Retrieve devices
- Update devices
- Delete devices
- Perform health check-ins
- Retrieve diagnostic history

---

## Entity Framework Core

Responsible for

- Mapping C# models
- Translating LINQ queries
- Executing SQL
- Managing database migrations

---

## Database

FleetGuard currently uses SQLite.

Current tables

- Devices
- DiagnosticsLogs
- __EFMigrationsHistory

---

## Dashboard

Built using

- Next.js
- React
- TypeScript

Responsibilities

- Device inventory
- Search
- Status filtering
- Device registration
- Device editing
- Device deletion
- Device check-ins
- Diagnostic timeline
- Device details

---

# Entity Relationship

```text
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
├── HealthMessage
│
└──────────────┐
               │
               │ 1
               │
               │
               ▼
         DiagnosticsLog
               │
               ├── Id
               ├── DeviceId
               ├── BatteryLevel
               ├── Status
               ├── HealthMessage
               ├── CheckedInAt
               ├── Encryption
               ├── ScreenLock
               ├── RootDetection
               └── IP Address
```

One device can have many diagnostic history records.

---

# Health Evaluation Rules

FleetGuard automatically evaluates every device during check-in.

Priority

```text
Rooted Device
      │
      ▼
Critical

↓

Not Encrypted

↓

Critical

↓

Screen Lock Disabled

↓

Warning

↓

Battery <20%

↓

Warning

↓

Everything Passed

↓

Healthy
```

---

# Current REST API

| Method | Endpoint | Description |
|---------|----------|-------------|
|POST|/api/devices|Register Device|
|GET|/api/devices|Retrieve Devices|
|GET|/api/devices/{id}|Retrieve Device|
|PUT|/api/devices/{id}|Update Device|
|DELETE|/api/devices/{id}|Delete Device|
|POST|/api/devices/{id}/check-in|Device Check-In|
|GET|/api/devices/{id}/diagnostics|Diagnostic History|

---

# Current Features

Backend

- Device Registration
- Device Retrieval
- Device Update
- Device Delete
- Device Check-In
- Duplicate Serial Validation
- Automatic Health Evaluation
- Diagnostic History
- Entity Framework Core
- SQLite
- REST API

Frontend

- Enterprise Dashboard
- Search
- Status Filter
- Device Details
- Device Registration
- Device Editing
- Device Check-In
- Device Deletion
- Diagnostic Timeline
- Responsive Layout

Cloud

- Azure App Service Deployment
- GitHub Actions CI/CD
- Production API
- Environment Variables

---

# Technologies

Backend

- ASP.NET Core 10
- C#
- Entity Framework Core
- SQLite

Frontend

- Next.js
- React
- TypeScript
- CSS
- Lucide React

Cloud

- Azure App Service
- GitHub Actions

Development

- Visual Studio
- Visual Studio Code
- Postman
- DBeaver
- Git
- GitHub

---

# Development Workflow

```text
Create Feature Branch

↓

Develop Feature

↓

Test Using Postman

↓

Verify Database

↓

Update Dashboard

↓

Update Documentation

↓

Commit

↓

Push

↓

Create Pull Request

↓

Review

↓

Merge into Main

↓

Automatic Azure Deployment
```

---

# Current Project Status

## Phase 1 — Backend Foundation ✅

- REST API
- Entity Framework Core
- SQLite
- CRUD Operations
- Validation
- Migrations

---

## Phase 2 — Enterprise Features ✅

- Device Health Evaluation
- Compliance Rules
- Diagnostic History
- Duplicate Serial Detection

---

## Phase 3 — Full Stack Dashboard ✅

- Next.js Dashboard
- Search
- Filters
- Registration
- Editing
- Check-In
- Diagnostic Timeline

---

## Phase 4 — Cloud Deployment ✅

- Azure App Service
- GitHub Actions CI/CD
- Production Deployment

---

# Planned Improvements

To better reflect a production-grade Enterprise Mobility Management platform, the following enhancements are planned.

Backend

- Service Layer
- Repository Pattern
- Global Exception Middleware
- Serilog Logging
- FluentValidation
- Unit Testing
- Integration Testing

Security

- JWT Authentication
- Role-Based Authorization
- Refresh Tokens
- API Rate Limiting

Cloud

- Azure SQL Database
- Azure Key Vault
- Azure Monitor
- Azure Application Insights

Frontend

- Authentication
- User Management
- Compliance Dashboard
- Device Location Map
- Live Notifications
- Dark Mode
- Charts and Analytics

DevOps

- Docker
- Azure Container Apps
- Infrastructure as Code
- Automated Testing Pipeline
