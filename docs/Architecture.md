# FleetGuard Architecture

## Overview

FleetGuard is a RESTful ASP.NET Core Web API designed to manage enterprise devices such as Android, iOS, Windows, Linux, printers, and IoT devices.

The current implementation demonstrates a production-style backend architecture using ASP.NET Core while following common software engineering practices such as feature branching, pull requests, REST API design, and layered project organization.

---

# High-Level Architecture

```text
                Client
          (Postman / Future Frontend)
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
         ┌───────────┴───────────┐
         ▼                       ▼
 RegisterDeviceRequest      Device Model
         │                       │
         └───────────┬───────────┘
                     ▼
          In-Memory List<Device>
                     │
                     ▼
              JSON Response
```

---

# Current Request Flow

When a client sends a request to the API, the following steps occur:

1. The client sends an HTTP request.
2. ASP.NET Core matches the request URL with the appropriate controller.
3. The controller receives the request model.
4. A new Device object is created.
5. The device is stored inside an in-memory collection.
6. The API returns a JSON response with the appropriate HTTP status code.

---

# Project Structure

```
FleetGuard
│
├── Controllers
│     └── DevicesController.cs
│
├── Models
│     └── Device.cs
│
├── Requests
│     └── RegisterDeviceRequest.cs
│
├── Enums
│     ├── DevicePlatform.cs
│     └── DeviceStatus.cs
│
├── docs
│     ├── API.md
│     └── Architecture.md
│
├── Program.cs
├── appsettings.json
└── FleetGuard.csproj
```

---

# Folder Responsibilities

## Controllers

Responsible for handling incoming HTTP requests and returning HTTP responses.

Current controller:

- DevicesController

---

## Models

Represents the application's business objects.

Current model:

- Device

---

## Requests

Contains DTOs (Data Transfer Objects) used to receive incoming API data.

Current request model:

- RegisterDeviceRequest

---

## Enums

Contains predefined values used throughout the application.

Current enums:

- DevicePlatform
- DeviceStatus

---

## docs

Contains technical documentation for the project.

Current documentation:

- API.md
- Architecture.md

---

# Current Data Storage

The application currently stores devices inside an in-memory collection.

```csharp
private static readonly List<Device> Devices = new();
```

Advantages:

- Fast
- Simple
- Great for learning
- No database configuration required

Limitations:

- Data is lost when the application stops.
- Data cannot be shared across multiple application instances.
- Not suitable for production environments.

---

# Current API Endpoints

| Method | Endpoint | Description |
|---------|----------|-------------|
| POST | /api/devices | Register a new device |
| GET | /api/devices | Retrieve all devices |
| GET | /api/devices/{id} | Retrieve a device by ID |

---

# Current Technologies

- ASP.NET Core Web API (.NET 10)
- C#
- Git
- GitHub
- Postman
- Markdown Documentation

---

# Planned Architecture

The next implementation will replace the in-memory collection with a persistent database.

```text
                Client
                    │
                    ▼
           DevicesController
                    │
                    ▼
            Repository Layer
                    │
                    ▼
        Entity Framework Core
                    │
                    ▼
             SQLite Database
```

Eventually the application will be deployed to Azure.

```text
Client
    │
    ▼
Azure App Service
    │
    ▼
ASP.NET Core API
    │
    ▼
Azure SQL Database
```

---

# Future Improvements

The following features are planned for future development:

- Entity Framework Core
- SQLite Database
- Repository Pattern
- Dependency Injection
- Device Update Endpoint
- Device Delete Endpoint
- Validation
- Logging
- Unit Testing
- Azure Deployment
- Authentication & Authorization
- Device Health Monitoring
- Device Compliance Tracking

---

# Current Development Status

Completed:

- ASP.NET Core Web API setup
- GitHub repository
- Feature branch workflow
- Pull request workflow
- Device model
- Request model
- Enums
- Device registration endpoint
- Retrieve all devices endpoint
- Retrieve device by ID endpoint
- Error handling
- API testing using Postman
- API documentation
- Architecture documentation

In Progress:

- Database integration using Entity Framework Core

Upcoming:

- SQLite
- Repository Pattern
- Dependency Injection
- Azure Deployment