# FleetGuard

FleetGuard is a full-stack enterprise device-management and diagnostics platform built with ASP.NET Core, Entity Framework Core, SQLite, Next.js, and TypeScript.

It demonstrates how organizations can register managed devices, monitor their health and compliance state, process device check-ins, and retain diagnostic history for troubleshooting.

## Features

- Device registration
- Device inventory
- Device details
- Device update and deletion
- Case-insensitive unique serial-number enforcement
- Device health check-ins
- Automatic health classification
- Encryption monitoring
- Screen-lock monitoring
- Root/jailbreak detection
- Battery monitoring
- Diagnostic history
- Search and status filtering
- Responsive enterprise dashboard
- SQLite persistence
- Entity Framework Core migrations

## Health Evaluation

FleetGuard evaluates each device check-in using the following priority:

1. Rooted or jailbroken device → Critical
2. Storage not encrypted → Critical
3. Screen lock disabled → Warning
4. Battery below 20% → Warning
5. All checks passed → Healthy

## Technology Stack

### Backend

- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- LINQ
- Dependency Injection

### Frontend

- Next.js
- React
- TypeScript
- CSS
- Lucide React

### Development Tools

- Visual Studio
- Visual Studio Code
- Git
- GitHub
- Postman
- DBeaver

## Architecture

```text
Next.js Dashboard
        |
        | HTTP / JSON
        v
ASP.NET Core Web API
        |
        v
DevicesController
        |
        v
FleetGuardDbContext
        |
        v
Entity Framework Core
        |
        v
SQLite Database

## Screenshots

### Dashboard Overview

The dashboard provides a live overview of total, healthy, warning, and critical devices, along with search, filtering, and device management actions.

![FleetGuard Dashboard](docs/images/dashboard/dashboard-ui-4types.jpeg)

### Device Details

Selecting a device opens its current health, compliance information, platform details, battery status, IP address, and latest check-in data.

![FleetGuard Device Details](docs/images/dashboard/device-details.jpeg)

### Diagnostic History

Every device check-in is stored as a diagnostic event, allowing users to review historical health changes and troubleshoot device issues.

![FleetGuard Diagnostic History](docs/images/dashboard/device-logs-on-dashboard.jpeg)