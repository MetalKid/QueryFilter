namespace QueryFilter.Enums
{
    /// <summary>
    /// This enum determines if a group should be using "and" or "or" in the query.
    /// </summary>
    public enum FilterGroupTypeEnum
    {
        /// <summary>
        /// An invalid enum was sent.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// All direct commands under this group should be "and"ed together.
        /// </summary>
        And = 1,
        /// <summary>
        /// All direct commands under this group should be "or"ed together.
        /// </summary>
        Or = 2
    }
}
