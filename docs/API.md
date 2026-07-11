# FleetGuard API Documentation

## Overview

FleetGuard is a RESTful ASP.NET Core Web API used to manage enterprise devices.

Current implementation supports:

- Registering a device
- Retrieving all registered devices
- Retrieving a device by its unique ID
- Returning appropriate error responses when a device is not found

---

# Base URL

During local development the API runs at:

```text
http://localhost:5172
```

---

# Endpoint 1 - Register Device

Registers a new enterprise device.

### URL

```http
POST /api/devices
```

### Request Body

```json
{
  "deviceName": "Warehouse Scanner 101",
  "serialNumber": "WH-1001",
  "platform": 1,
  "operatingSystemVersion": "Android 15"
}
```

### Success Response

**Status Code**

```text
201 Created
```

**Response**

```json
{
  "id": "d980372e-c026-4944-b3c5-67c4366e9e59",
  "deviceName": "Warehouse Scanner 101",
  "serialNumber": "WH-1001",
  "platform": 1,
  "status": 1,
  "operatingSystemVersion": "Android 15"
}
```

---

# Endpoint 2 - Get All Devices

Returns every registered device currently stored in memory.

### URL

```http
GET /api/devices
```

### Success Response

**Status Code**

```text
200 OK
```

**Response**

```json
[
  {
    "id": "d980372e-c026-4944-b3c5-67c4366e9e59",
    "deviceName": "Warehouse Scanner 101",
    "serialNumber": "WH-1001",
    "platform": 1,
    "status": 1,
    "operatingSystemVersion": "Android 15"
  }
]
```

---

# Endpoint 3 - Get Device By ID

Returns a single device using its unique GUID.

### URL

```http
GET /api/devices/{id}
```

### Example

```http
GET /api/devices/d980372e-c026-4944-b3c5-67c4366e9e59
```

### Success Response

**Status Code**

```text
200 OK
```

**Response**

```json
{
  "id": "d980372e-c026-4944-b3c5-67c4366e9e59",
  "deviceName": "Warehouse Scanner 101",
  "serialNumber": "WH-1001",
  "platform": 1,
  "status": 1,
  "operatingSystemVersion": "Android 15"
}
```

---

# Endpoint 4 - Device Not Found

If a valid GUID does not exist in the system, the API returns a 404 response.

### Example

```http
GET /api/devices/aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa
```

### Response

**Status Code**

```text
404 Not Found
```

**Response**

```json
{
  "message": "Device not found."
}
```

---

# HTTP Status Codes Used

| Status Code | Description |
|-------------|-------------|
| 200 OK | Request completed successfully |
| 201 Created | Resource was successfully created |
| 404 Not Found | Requested device does not exist |

---

# Current Implementation

The application currently stores devices in an in-memory collection:

```csharp
private static readonly List<Device> Devices = new();
```

This means:

- Devices exist only while the application is running.
- Restarting the API clears all stored devices.
- No database persistence exists yet.

---

# Next Steps

The next development phase will include:

- Entity Framework Core
- SQLite Database
- Repository Pattern
- Dependency Injection
- Azure Deployment