#region << Usings >>

using System;
using QueryFilter.Enums;
using QueryFilter.Interfaces;

#endregion

namespace QueryFilter
{
    /// <summary>
    /// This struct is used to do filtering on types that are often filtered based on a range of values. 
    /// (i.e. numbers, dates, etc)
    /// </summary>
    /// <typeparam name="T">The type of value this struct is for.</typeparam>
    public class FilterRangeItem<T> : IFilterItem
        where T: struct
    {

        #region << Properties >>

        /// <summary>
        /// Gets a key to this command in order to maintain the FilterGroup relationships after deserializing JSON.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the value to filter on.
        /// </summary>
        public T? Value { get; set; }

        /// <summary>
        /// Gets the non-nullable value.
        /// </summary>
        public T NonNullableValue { get { return Value.HasValue ? Value.Value : default(T); } }

        /// <summary>
        /// Gets or sets the type of filter to perform.
        /// </summary>
        public FilterRangeTypeEnum Operator { get; set; }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Constructor to initialize all properties.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <param name="rangeType">The type of filter to perform.</param>
        public FilterRangeItem(
            T? value = default(T?), 
            FilterRangeTypeEnum rangeType = FilterRangeTypeEnum.EqualTo)
        {
            Id = Guid.NewGuid().ToString();
            Value = value;
            Operator = rangeType;
        }

        #endregion

    }
}
