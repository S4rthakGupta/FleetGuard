export type Device = {
  id: string;
  deviceName: string;
  serialNumber: string;
  platform: number;
  status: number;
  operatingSystemVersion: string;
  batteryLevel: number | null;
  isEncrypted: boolean | null;
  isScreenLockEnabled: boolean | null;
  isRootedOrJailbroken: boolean | null;
  ipAddress: string | null;
  lastCheckInAt: string | null;
  healthMessage: string;
};

export type DiagnosticsLog = {
  id: string;
  deviceId: string;
  batteryLevel: number;
  status: number;
  healthMessage: string;
  checkedInAt: string;
};