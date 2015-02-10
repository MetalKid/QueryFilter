namespace QueryFilter.Enums
{
    /// <summary>
    /// This enum defines the different queries to perform on a type that can be compared in a range.
    /// </summary>
    public enum FilterRangeTypeEnum
    {
        /// <summary>
        /// An invalid enum was sent.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Check that the values equal.
        /// </summary>
        EqualTo = 1,
        /// <summary>
        /// Check that the values do not equal.
        /// </summary>
        NotEqualTo = 2,
        /// <summary>
        /// Check that the value is less than another.
        /// </summary>
        LessThan = 3,
        /// <summary>
        /// Check that the value is less than or equal to another.
        /// </summary>
        LessThanOrEqualTo = 4,
        /// <summary>
        /// Check that the value is greater than another.
        /// </summary>
        GreaterThan = 5,
        /// <summary>
        /// Check that the value is greater than or equal to another.
        /// </summary>
        GreaterThanOrEqualTo = 6
    }
}
