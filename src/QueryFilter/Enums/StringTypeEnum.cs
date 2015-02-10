namespace QueryFilter.Enums
{
    /// <summary>
    /// This enum defines the different queries to perform on a string.
    /// </summary>
    public enum FilterStringEnum
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
        /// Check that the value contains another.
        /// </summary>
        Contains = 3,
        /// <summary>
        /// Check that the value does not contain another.
        /// </summary>
        NotContains = 4,
        /// <summary>
        /// Check that the value starts with another.
        /// </summary>
        StartsWith = 5,
        /// <summary>
        /// Check that the value does not start with another.
        /// </summary>
        NotStartsWith = 6,
        /// <summary>
        /// Check that the value ends with another.
        /// </summary>
        EndsWith = 7,
        /// <summary>
        /// Check that the value does not end with another.
        /// </summary>
        NotEndsWith = 8
    }
}
