#region << Usings >>

using System;
using QueryFilter.Enums;
using QueryFilter.Interfaces;

#endregion

namespace QueryFilter
{
    /// <summary>
    /// This struct is used to do filtering on types that can only be filtered on whether they equal each other or not.
    /// </summary>
    /// <typeparam name="T">The type of value this filter is for.</typeparam>
    public class FilterEquatableItem<T> : IFilterItem
    {
        
        #region << Properties >>

        /// <summary>
        /// Gets a key to this command in order to maintain the FilterGroup relationships after deserializing JSON.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the value to filter on.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Gets or sets the type of filter to perform.
        /// </summary>
        public FilterEquatableTypeEnum Operator { get; set; }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Constructor to initialize all properties.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <param name="equatableType">The type of filter to perform.</param>
        public FilterEquatableItem(
            T value = default(T), 
            FilterEquatableTypeEnum equatableType = FilterEquatableTypeEnum.EqualTo)
        {
            Id = Guid.NewGuid();
            Value = value;
            Operator = equatableType;
        }

        #endregion

    }
}
