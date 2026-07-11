namespace FleetGuard.Enums
{

    // This models the device's lifecycle or the health state.
    public enum DeviceStatus
    {
        Pending = 1,
        Healthy = 2,
        Warning = 3,
        Critical = 4,
        Offline = 5,
        Retired = 6
    }
}
