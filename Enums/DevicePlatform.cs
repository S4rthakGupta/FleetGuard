namespace FleetGuard.Enums
{
    // They basically represent a fixed group of allowed choices.
    // Why we used it: For eg: Somebody can write Andriod instead of Android if we use string.
    // Using enum -> It imrpoves consistency, validation, type safety, database storage, comparisons etc.
    public enum DevicePlatform
    {
        Android = 1,
        iOS = 2,
        Windows = 3,
        Linux = 4,
        Printer = 5,
        Other = 6
    }
}
