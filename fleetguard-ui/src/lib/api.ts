import type {
  Device,
  DeviceCheckInRequest,
  DiagnosticsLog,
  RegisterDeviceRequest,
  UpdateDeviceRequest,
} from "../types/device";

const API_BASE_URL =
  process.env.NEXT_PUBLIC_API_BASE_URL ?? "http://localhost:5172";

async function parseError(response: Response): Promise<string> {
  try {
    const body = await response.json();

    if (body.message) {
      return body.message;
    }

    if (body.errors) {
      const validationMessages = Object.values(body.errors)
        .flat()
        .join(" ");

      if (validationMessages) {
        return validationMessages;
      }
    }

    return body.title ?? `Request failed with status ${response.status}`;
  } catch {
    return `Request failed with status ${response.status}`;
  }
}

export async function getDevices(): Promise<Device[]> {
  const response = await fetch(`${API_BASE_URL}/api/devices`, {
    cache: "no-store",
  });

  if (!response.ok) {
    throw new Error(await parseError(response));
  }

  return response.json();
}

export async function getDevice(id: string): Promise<Device> {
  const response = await fetch(`${API_BASE_URL}/api/devices/${id}`, {
    cache: "no-store",
  });

  if (!response.ok) {
    throw new Error(await parseError(response));
  }

  return response.json();
}

export async function getDiagnostics(
  id: string,
): Promise<DiagnosticsLog[]> {
  const response = await fetch(
    `${API_BASE_URL}/api/devices/${id}/diagnostics`,
    {
      cache: "no-store",
    },
  );

  if (!response.ok) {
    throw new Error(await parseError(response));
  }

  return response.json();
}

export async function registerDevice(
  request: RegisterDeviceRequest,
): Promise<Device> {
  const response = await fetch(`${API_BASE_URL}/api/devices`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(request),
  });

  if (!response.ok) {
    throw new Error(await parseError(response));
  }

  return response.json();
}

export async function updateDevice(
  id: string,
  request: UpdateDeviceRequest,
): Promise<Device> {
  const response = await fetch(`${API_BASE_URL}/api/devices/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(request),
  });

  if (!response.ok) {
    throw new Error(await parseError(response));
  }

  return response.json();
}

export async function checkInDevice(
  id: string,
  request: DeviceCheckInRequest,
): Promise<Device> {
  const response = await fetch(
    `${API_BASE_URL}/api/devices/${id}/check-in`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(request),
    },
  );

  if (!response.ok) {
    throw new Error(await parseError(response));
  }

  return response.json();
}

export async function deleteDevice(id: string): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/api/devices/${id}`, {
    method: "DELETE",
  });

  if (!response.ok) {
    throw new Error(await parseError(response));
  }
}