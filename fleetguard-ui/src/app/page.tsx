"use client";

import {
  Activity,
  AlertTriangle,
  Battery,
  CheckCircle2,
  Laptop,
  LoaderCircle,
  RefreshCw,
  Search,
  ShieldAlert,
  Smartphone,
  Trash2,
  Wifi,
  X,
} from "lucide-react";
import { useCallback, useEffect, useMemo, useState } from "react";

import {
  checkInDevice,
  deleteDevice,
  getDevices,
  getDiagnostics,
} from "../lib/api";
import type { Device, DiagnosticsLog } from "../types/device";

const STATUS = {
  Pending: 1,
  Healthy: 2,
  Warning: 3,
  Critical: 4,
} as const;

function getStatusLabel(status: number): string {
  if (status === STATUS.Healthy) return "Healthy";
  if (status === STATUS.Warning) return "Warning";
  if (status === STATUS.Critical) return "Critical";
  return "Pending";
}

function getPlatformLabel(platform: number): string {
  const platforms: Record<number, string> = {
    1: "Android",
    2: "iOS",
    3: "Windows",
    4: "Linux",
    5: "Printer",
    6: "Other",
  };

  return platforms[platform] ?? "Unknown";
}

function formatDate(value: string | null): string {
  if (!value) return "Never";

  return new Intl.DateTimeFormat("en-CA", {
    dateStyle: "medium",
    timeStyle: "short",
  }).format(new Date(value));
}

function StatusBadge({ status }: { status: number }) {
  const label = getStatusLabel(status);

  return (
    <span className={`status-badge status-${label.toLowerCase()}`}>
      {label}
    </span>
  );
}

export default function Home() {
  const [devices, setDevices] = useState<Device[]>([]);
  const [selectedDevice, setSelectedDevice] =
    useState<Device | null>(null);
  const [diagnostics, setDiagnostics] = useState<DiagnosticsLog[]>([]);
  const [loading, setLoading] = useState(true);
  const [detailsLoading, setDetailsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [searchTerm, setSearchTerm] = useState("");
  const [statusFilter, setStatusFilter] = useState("all");
  const [checkInOpen, setCheckInOpen] = useState(false);
  const [checkInSubmitting, setCheckInSubmitting] = useState(false);

  const [checkInForm, setCheckInForm] = useState({
  batteryLevel: 80,
  isEncrypted: true,
  isScreenLockEnabled: true,
  isRootedOrJailbroken: false,
  ipAddress: "192.168.1.10",
});

  const loadDevices = useCallback(async (showLoading = true) => {
    try {
      if (showLoading) {
        setLoading(true);
      }

      setError(null);

      const data = await getDevices();

      setDevices(data);

      setSelectedDevice((currentDevice) => {
        if (!currentDevice) {
          return null;
        }

        return (
          data.find((device) => device.id === currentDevice.id) ??
          null
        );
      });
    } catch (requestError) {
      setError(
        requestError instanceof Error
          ? requestError.message
          : "Unable to load devices.",
      );
    } finally {
      if (showLoading) {
        setLoading(false);
      }
    }
  }, []);

    useEffect(() => {
      let isCancelled = false;

      async function initializeDashboard() {
        try {
          const data = await getDevices();

          if (!isCancelled) {
            setDevices(data);
            setError(null);
          }
        } catch (requestError) {
          if (!isCancelled) {
            setError(
              requestError instanceof Error
                ? requestError.message
                : "Unable to load devices.",
            );
          }
        } finally {
          if (!isCancelled) {
            setLoading(false);
          }
        }
      }

      void initializeDashboard();

      return () => {
        isCancelled = true;
      };
    }, []);

  const totals = useMemo(
    () => ({
      total: devices.length,
      healthy: devices.filter(
        (device) => device.status === STATUS.Healthy,
      ).length,
      warning: devices.filter(
        (device) => device.status === STATUS.Warning,
      ).length,
      critical: devices.filter(
        (device) => device.status === STATUS.Critical,
      ).length,
    }),
    [devices],
  );

  const filteredDevices = useMemo(() => {
  const normalizedSearch = searchTerm.trim().toLowerCase();

    return devices.filter((device) => {
      const matchesSearch =
        device.deviceName.toLowerCase().includes(normalizedSearch) ||
        device.serialNumber.toLowerCase().includes(normalizedSearch);

      const matchesStatus =
        statusFilter === "all" ||
        getStatusLabel(device.status).toLowerCase() === statusFilter;

      return matchesSearch && matchesStatus;
    });
  }, [devices, searchTerm, statusFilter]);

  async function openDevice(device: Device) {
    try {
      setSelectedDevice(device);
      setDetailsLoading(true);
      setError(null);

      const history = await getDiagnostics(device.id);
      setDiagnostics(history);
    } catch (requestError) {
      setDiagnostics([]);
      setError(
        requestError instanceof Error
          ? requestError.message
          : "Unable to load diagnostic history.",
      );
    } finally {
      setDetailsLoading(false);
    }
  }

  async function handleDelete(device: Device) {
    const confirmed = window.confirm(
      `Delete ${device.deviceName}? This cannot be undone.`,
    );

    if (!confirmed) return;

    try {
      setError(null);
      await deleteDevice(device.id);

      if (selectedDevice?.id === device.id) {
        setSelectedDevice(null);
        setDiagnostics([]);
      }

      await loadDevices();
    } catch (requestError) {
      setError(
        requestError instanceof Error
          ? requestError.message
          : "Unable to delete device.",
      );
    }
  }

  return (
    <main className="app-shell">
      <header className="topbar">
        <div>
          <p className="eyebrow">Enterprise Mobility Console</p>
          <h1>FleetGuard</h1>
          <p className="subtitle">
            Monitor devices, compliance, and diagnostic history.
          </p>
        </div>

        <button
          className="secondary-button"
          onClick={() => void loadDevices()}
          disabled={loading}
        >
          <RefreshCw size={17} className={loading ? "spin" : ""} />
          Refresh
        </button>
      </header>

      {error && (
        <div className="error-banner">
          <AlertTriangle size={18} />
          <span>{error}</span>
          <button onClick={() => setError(null)} aria-label="Dismiss">
            <X size={17} />
          </button>
        </div>
      )}

      <section className="stats-grid">
        <article className="stat-card">
          <div className="stat-icon">
            <Laptop size={22} />
          </div>
          <div>
            <span>Total Devices</span>
            <strong>{totals.total}</strong>
          </div>
        </article>

        <article className="stat-card">
          <div className="stat-icon healthy-icon">
            <CheckCircle2 size={22} />
          </div>
          <div>
            <span>Healthy</span>
            <strong>{totals.healthy}</strong>
          </div>
        </article>

        <article className="stat-card">
          <div className="stat-icon warning-icon">
            <AlertTriangle size={22} />
          </div>
          <div>
            <span>Warning</span>
            <strong>{totals.warning}</strong>
          </div>
        </article>

        <article className="stat-card">
          <div className="stat-icon critical-icon">
            <ShieldAlert size={22} />
          </div>
          <div>
            <span>Critical</span>
            <strong>{totals.critical}</strong>
          </div>
        </article>
      </section>

      <section className="content-grid">
        <article className="panel inventory-panel">
          <div className="panel-heading">
            <div className="inventory-toolbar">
              <label className="search-box">
                <Search size={17} />

                <input
                  type="search"
                  placeholder="Search by name or serial number"
                  value={searchTerm}
                  onChange={(event) => setSearchTerm(event.target.value)}
                />
              </label>

              <select
                className="status-filter"
                value={statusFilter}
                onChange={(event) => setStatusFilter(event.target.value)}
              >
                <option value="all">All statuses</option>
                <option value="pending">Pending</option>
                <option value="healthy">Healthy</option>
                <option value="warning">Warning</option>
                <option value="critical">Critical</option>
              </select>
            </div>
            <div>
              <p className="eyebrow">Inventory</p>
              <h2>Managed Devices</h2>
            </div>
            <span>
              {filteredDevices.length} of {devices.length} devices
            </span>
          </div>

          {loading ? (
            <div className="empty-state">
              <LoaderCircle className="spin" size={28} />
              <p>Loading devices...</p>
            </div>
          ) : devices.length === 0 ? (
            <div className="empty-state">
              <Smartphone size={34} />
              <h3>No devices enrolled</h3>
              <p>Register a device through the API to see it here.</p>
            </div>
          ) : (
            <div className="table-wrapper">
              <table>
                <thead>
                  <tr>
                    <th>Device</th>
                    <th>Platform</th>
                    <th>Battery</th>
                    <th>Status</th>
                    <th>Last check-in</th>
                    <th />
                  </tr>
                </thead>

                <tbody>
                  {filteredDevices.map((device) => (
                    <tr
                      key={device.id}
                      className={
                        selectedDevice?.id === device.id
                          ? "selected-row"
                          : ""
                      }
                    >
                      <td>
                        <button
                          className="device-link"
                          onClick={() => void openDevice(device)}
                        >
                          <span>{device.deviceName}</span>
                          <small>{device.serialNumber}</small>
                        </button>
                      </td>
                      <td>{getPlatformLabel(device.platform)}</td>
                      <td>
                        <span className="battery-value">
                          <Battery size={16} />
                          {device.batteryLevel === null
                            ? "Unknown"
                            : `${device.batteryLevel}%`}
                        </span>
                      </td>
                      <td>
                        <StatusBadge status={device.status} />
                      </td>
                      <td>{formatDate(device.lastCheckInAt)}</td>
                      <td>
                        <button
                          className="icon-button danger-button"
                          onClick={() => void handleDelete(device)}
                          aria-label={`Delete ${device.deviceName}`}
                        >
                          <Trash2 size={17} />
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </article>

        <aside className="panel details-panel">
          {!selectedDevice ? (
            <div className="empty-state details-empty">
              <Activity size={34} />
              <h3>Select a device</h3>
              <p>
                Open a device to review current health and diagnostic
                history.
              </p>
            </div>
          ) : (
            <>
            <div className="panel-heading">
              <div>
                <p className="eyebrow">Device Details</p>
                <h2>{selectedDevice.deviceName}</h2>
              </div>

              <div className="panel-actions">
                <button
                  className="primary-button"
                  onClick={() => setCheckInOpen(true)}
                >
                  Run Check-In
                </button>

                <button
                  className="icon-button"
                  onClick={() => {
                    setSelectedDevice(null);
                    setDiagnostics([]);
                  }}
                  aria-label="Close details"
                >
                  <X size={18} />
                </button>
              </div>
            </div>

              <div className="detail-status">
                <StatusBadge status={selectedDevice.status} />
                <p>{selectedDevice.healthMessage}</p>
              </div>

              <dl className="details-list">
                <div>
                  <dt>Serial number</dt>
                  <dd>{selectedDevice.serialNumber}</dd>
                </div>
                <div>
                  <dt>Platform</dt>
                  <dd>{getPlatformLabel(selectedDevice.platform)}</dd>
                </div>
                <div>
                  <dt>Operating system</dt>
                  <dd>{selectedDevice.operatingSystemVersion}</dd>
                </div>
                <div>
                  <dt>Battery</dt>
                  <dd>
                    {selectedDevice.batteryLevel === null
                      ? "Unknown"
                      : `${selectedDevice.batteryLevel}%`}
                  </dd>
                </div>
                <div>
                  <dt>Encryption</dt>
                  <dd>
                    {selectedDevice.isEncrypted === null
                      ? "Unknown"
                      : selectedDevice.isEncrypted
                        ? "Enabled"
                        : "Disabled"}
                  </dd>
                </div>
                <div>
                  <dt>Screen lock</dt>
                  <dd>
                    {selectedDevice.isScreenLockEnabled === null
                      ? "Unknown"
                      : selectedDevice.isScreenLockEnabled
                        ? "Enabled"
                        : "Disabled"}
                  </dd>
                </div>
                <div>
                  <dt>Rooted / jailbroken</dt>
                  <dd>
                    {selectedDevice.isRootedOrJailbroken === null
                      ? "Unknown"
                      : selectedDevice.isRootedOrJailbroken
                        ? "Detected"
                        : "Not detected"}
                  </dd>
                </div>
                <div>
                  <dt>IP address</dt>
                  <dd>{selectedDevice.ipAddress ?? "Unknown"}</dd>
                </div>
              </dl>

              <div className="history-heading">
                <div>
                  <Wifi size={17} />
                  <h3>Check-in History</h3>
                </div>
                <span>{diagnostics.length} events</span>
              </div>

              {detailsLoading ? (
                <div className="empty-state compact-state">
                  <LoaderCircle className="spin" size={24} />
                </div>
              ) : diagnostics.length === 0 ? (
                <div className="empty-state compact-state">
                  <p>No diagnostic history available.</p>
                </div>
              ) : (
                <div className="timeline">
                  {diagnostics.map((log) => (
                    <article key={log.id} className="timeline-item">
                      <div
                        className={`timeline-dot status-${getStatusLabel(
                          log.status,
                        ).toLowerCase()}`}
                      />
                      <div>
                        <div className="timeline-topline">
                          <StatusBadge status={log.status} />
                          <time>{formatDate(log.checkedInAt)}</time>
                        </div>
                        <p>{log.healthMessage}</p>
                        <small>Battery: {log.batteryLevel}%</small>
                      </div>
                    </article>
                  ))}
                </div>
              )}
            </>
          )}
        </aside>
      </section>
            {checkInOpen && selectedDevice && (
        <div className="modal-backdrop">
          <div 
          className="modal-card"
          role="dialog"
          aria-modal="true"
          aria-labelledby="check-in-title"
          >
            <div className="modal-heading">
              <div>
                <p className="eyebrow">Device Action</p>
                <h2 id="check-in-title">Run Check-In</h2>
              </div>

              <button
                type="button"
                className="icon-button"
                onClick={() => setCheckInOpen(false)}
                aria-label="Close check-in modal"
              >
                <X size={18} />
              </button>
            </div>

            <div className="form-grid">
              <label>
                Battery Level
                <input
                  type="number"
                  min="0"
                  max="100"
                  value={checkInForm.batteryLevel}
                  onChange={(event) =>
                    setCheckInForm({
                      ...checkInForm,
                      batteryLevel: Number(event.target.value),
                    })
                  }
                />
              </label>

              <label>
                IP Address
                <input
                  value={checkInForm.ipAddress}
                  onChange={(event) =>
                    setCheckInForm({
                      ...checkInForm,
                      ipAddress: event.target.value,
                    })
                  }
                />
              </label>

              <label className="checkbox-row">
                <input
                  type="checkbox"
                  checked={checkInForm.isEncrypted}
                  onChange={(event) =>
                    setCheckInForm({
                      ...checkInForm,
                      isEncrypted: event.target.checked,
                    })
                  }
                />
                Storage Encrypted
              </label>

              <label className="checkbox-row">
                <input
                  type="checkbox"
                  checked={checkInForm.isScreenLockEnabled}
                  onChange={(event) =>
                    setCheckInForm({
                      ...checkInForm,
                      isScreenLockEnabled: event.target.checked,
                    })
                  }
                />
                Screen Lock Enabled
              </label>

              <label className="checkbox-row">
                <input
                  type="checkbox"
                  checked={checkInForm.isRootedOrJailbroken}
                  onChange={(event) =>
                    setCheckInForm({
                      ...checkInForm,
                      isRootedOrJailbroken: event.target.checked,
                    })
                  }
                />
                Rooted or Jailbroken
              </label>
            </div>

            <div className="modal-actions">
              <button
                className="secondary-button"
                onClick={() => setCheckInOpen(false)}
              >
                Cancel
              </button>

              <button
                type="button"
                className="primary-button"
                disabled={checkInSubmitting}
                onClick={async () => {
                  if (
                    checkInForm.batteryLevel < 0 ||
                    checkInForm.batteryLevel > 100
                  ) {
                    setError("Battery level must be between 0 and 100.");
                    return;
                  }

                  if (!checkInForm.ipAddress.trim()) {
                    setError("IP address is required.");
                    return;
                  }

                  try {
                    setCheckInSubmitting(true);
                    setError(null);

                    const updatedDevice = await checkInDevice(
                      selectedDevice.id,
                      checkInForm,
                    );

                    setSelectedDevice(updatedDevice);

                    const history = await getDiagnostics(updatedDevice.id);
                    setDiagnostics(history);

                    await loadDevices(false);

                    setCheckInOpen(false);
                  } catch (requestError) {
                    setError(
                      requestError instanceof Error
                        ? requestError.message
                        : "Unable to check in device.",
                    );
                  } finally {
                    setCheckInSubmitting(false);
                  }
                }}
              >
                {checkInSubmitting ? "Submitting..." : "Submit Check-In"}
              </button>
            </div>
          </div>
        </div>
      )}
    </main>
  );
}