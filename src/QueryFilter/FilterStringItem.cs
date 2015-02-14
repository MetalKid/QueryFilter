#region << Usings >>

using System;
using QueryFilter.Enums;
using QueryFilter.Interfaces;

#endregion

namespace QueryFilter
{
    /// <summary>
    /// This struct is used to do filtering on strings.
    /// </summary>
    public class FilterStringItem : IFilterItem
    {

        #region << Properties >>

        /// <summary>
        /// Gets a key to this command in order to maintain the FilterGroup relationships after deserializing JSON.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the value to filter on.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets whether to ignore the case of the string when filtering.
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        /// Gets or sets the type of filter to perform.
        /// </summary>
        public FilterStringEnum Operator { get; set; }
        
        #endregion

        #region << Constructors >>

        /// <summary>
        /// Constructor to initialize all properties.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <param name="rangeType">The type of filter to perform.</param>
        /// <param name="ignoreCase">Whether to ignore the case of the string when filtering.</param>
        public FilterStringItem(
            string value = null, 
            FilterStringEnum rangeType = FilterStringEnum.EqualTo,
            bool ignoreCase = false)
        {
            Id = Guid.NewGuid().ToString();
            Value = value;
            Operator = rangeType;
            IgnoreCase = ignoreCase;
        }

        #endregion

    }
}
