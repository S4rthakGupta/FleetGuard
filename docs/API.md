# FleetGuard API Documentation

## Overview

FleetGuard is a RESTful ASP.NET Core Web API built using **ASP.NET Core 10**, **Entity Framework Core**, and **SQLite**.

The API manages enterprise devices by allowing administrators to register, retrieve, update, delete, and monitor devices through health check-ins while enforcing validation and data integrity.

---

# Base URL

During local development

```text
http://localhost:5172
```

---

# Device Model

```json
{
    "id": "4ccc9e3c-2418-4dd8-a757-dfa38cb87096",
    "deviceName": "Warehouse Scanner 101",
    "serialNumber": "WH-1001",
    "platform": 1,
    "status": 1,
    "operatingSystemVersion": "Android 15",
    "batteryLevel": 98,
    "isEncrypted": true,
    "isScreenLockEnabled": true,
    "isRootedOrJailbroken": false,
    "ipAddress": "192.168.1.25",
    "lastCheckInAt": "2026-07-11T21:20:17Z",
    "healthMessage": "Healthy: Device passed all current checks."
}
```

---

# Endpoint 1 — Register Device

Registers a new enterprise device.

## URL

```http
POST /api/devices
```

### Request

```json
{
    "deviceName": "Warehouse Scanner 101",
    "serialNumber": "WH-1001",
    "platform": 1,
    "operatingSystemVersion": "Android 15"
}
```

### Success

```
201 Created
```

### Possible Responses

```
400 Bad Request
```

```
409 Conflict
```

### Example

<img src="./images/post-device.jpeg" width="850"/>

---

# Endpoint 2 — Retrieve All Devices

Returns every registered device.

## URL

```http
GET /api/devices
```

### Success

```
200 OK
```

---

# Endpoint 3 — Retrieve Device By ID

Returns a single registered device.

## URL

```http
GET /api/devices/{id}
```

### Success

```
200 OK
```

### Error

```
404 Not Found
```

### Example

<img src="./images/invalid-device-id.jpeg" width="850"/>

---

# Endpoint 4 — Update Device

Updates an existing device.

## URL

```http
PUT /api/devices/{id}
```

### Request

```json
{
    "deviceName": "Updated Warehouse Scanner",
    "serialNumber": "WH-1001",
    "platform": 1,
    "operatingSystemVersion": "Android 16",
    "status": 2
}
```

### Success

```
200 OK
```

### Possible Responses

```
404 Not Found
```

```
409 Conflict
```

### Example

<img src="./images/updating-put.jpeg" width="850"/>

---

# Endpoint 5 — Delete Device

Deletes an existing device.

## URL

```http
DELETE /api/devices/{id}
```

### Success

```
204 No Content
```

### Error

```
404 Not Found
```

---

# Endpoint 6 — Device Health Check-In

Allows a managed device to periodically report its current health and compliance information.

## URL

```http
POST /api/devices/{id}/check-in
```

### Request

```json
{
    "batteryLevel": 18,
    "isEncrypted": false,
    "isScreenLockEnabled": true,
    "isRootedOrJailbroken": false,
    "ipAddress": "192.168.1.25"
}
```

The API evaluates the device and automatically assigns an overall health status.

---

## Healthy Device

Conditions

- Battery ≥ 20%
- Storage encrypted
- Screen lock enabled
- Device not rooted

Example

<img src="./images/healthy-device-1.jpeg" width="850"/>

---

## Low Battery Warning

Battery level below 20%.

Example

<img src="./images/low-battery-warning-2.jpeg" width="850"/>

---

## Screen Lock Warning

Triggered when screen lock is disabled.

Example

<img src="./images/no-screenlock-warning-3.jpeg" width="850"/>

---

## Rooted/Jailbroken Device

Marked as Critical.

Example

<img src="./images/rooted-device-critical-4.jpeg" width="850"/>

---

## Validation Example

Missing required request values.

Example

<img src="./images/null-values-device-details.jpeg" width="850"/>

---

## Battery Validation Example

Additional battery validation example.

<img src="./images/validation-test-battery-5.jpeg" width="850"/>

---

# Validation Rules

FleetGuard currently validates

- Device Name is required
- Serial Number is required
- Platform is required
- Operating System Version is required
- Duplicate Serial Numbers are not allowed
- Device IDs must exist before update/delete/check-in

---

# HTTP Status Codes

| Status | Meaning |
|---------|----------|
|200 OK|Request completed successfully|
|201 Created|Device created|
|204 No Content|Device deleted|
|400 Bad Request|Validation failed|
|404 Not Found|Device not found|
|409 Conflict|Duplicate serial number|

---

# Technologies Used

- ASP.NET Core 10
- C#
- Entity Framework Core
- SQLite
- LINQ
- Dependency Injection
- Postman
- DBeaver
- Git
- GitHub

---

# Database

FleetGuard stores all device information inside a SQLite database.

Entity Framework Core automatically creates and updates the schema using migrations.

Current tables

- Devices
- __EFMigrationsHistory

---

# Testing

The API has been manually tested using

- Postman
- DBeaver
- Entity Framework Core Migrations

---

# Future Enhancements

- Service Layer
- Repository Pattern
- JWT Authentication
- Role-Based Authorization
- Logging
- Global Exception Handling
- Azure SQL
- Azure App Service
- React Dashboard
- GitHub Actions CI/CD

# Dashboard Integration

The Next.js dashboard consumes the FleetGuard REST API and displays device health, status, and diagnostic history.

![FleetGuard Diagnostic History](images/dashboard/device-logs-on-dashboard.jpeg)