namespace QueryFilter.Enums
{
    /// <summary>
    /// This enum determines which type of command is being worked with. 
    /// (This is normally used on an interface.)
    /// </summary>
    public enum FilterCommandTypeEnum
    {
        /// <summary>
        /// An invalid enum was sent.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Represents a FilterGroup object.
        /// </summary>
        Group = 1,
        /// <summary>
        /// Represents a FilterRange object.
        /// </summary>
        Range = 2,
        /// <summary>
        /// Represents a FilterEquatable object.
        /// </summary>
        Equatable = 3,
        /// <summary>
        /// Represents a FilterString object.
        /// </summary>
        String = 4
    }
}
