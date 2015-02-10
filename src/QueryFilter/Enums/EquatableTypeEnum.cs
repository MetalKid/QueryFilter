namespace QueryFilter.Enums
{
    /// <summary>
    /// This enum defines the different queries to perform on a type that can be compared equally.
    /// </summary>
    public enum FilterEquatableTypeEnum
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
        NotEqualTo = 2
    }
}
