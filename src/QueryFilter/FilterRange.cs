#region << Usings >>

using System.Collections.Generic;
using System.Linq;
using QueryFilter.Enums;
using QueryFilter.Interfaces;

#endregion

namespace QueryFilter
{
    /// <summary>
    /// This class is used to store data about filtering on types that are often filtered based on a range of values. 
    /// (i.e. numbers, dates, etc)
    /// </summary>
    /// <typeparam name="T">The type of value this struct is for.</typeparam>
    public class FilterRange<T> : IFilterCommand
        where T: struct
    {

        #region << Properties >>

        /// <summary>
        /// Gets the list of filters to apply, if any.
        /// </summary>
        public IList<FilterRangeItem<T>> Filters { get; private set; }

        /// <summary>
        /// Sets a value to filter on with an EqualTo check.
        /// </summary>
        /// <remarks>This is mainly to make it easier to filter on one value.</remarks>
        /// <remarks>Get is here only so JSON can deserialize correctly.</remarks>
        public T? Value
        {
            get { return null; }
            set { Filters.Add(new FilterRangeItem<T>(value)); }
        }

        /// <summary>
        /// Gets the total number of filters added.
        /// </summary>
        public int TotalItems
        {
            get { return Filters == null ? 0 : Filters.Count; }
        }

        /// <summary>
        /// Gets the type of command this class represents.
        /// </summary>
        public FilterCommandTypeEnum CommandType
        {
            get { return FilterCommandTypeEnum.Range; }
        }

        /// <summary>
        /// Gets the list of Filters via their interface.
        /// </summary>
        public IList<IFilterItem> FilterItems
        {
            get { return Filters.Cast<IFilterItem>().ToList(); }
        }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Default constuctor initializes properties.
        /// </summary>
        public FilterRange()
        {
            Filters = new List<FilterRangeItem<T>>();
        }

        /// <summary>
        /// Constuctor that assumes an initial Filter of EqualTo to the given value.
        /// </summary>
        /// <param name="value">The value to filter on with an EqualTo check.</param>
        public FilterRange(T? value) : this()
        {
            Value = value;
        }

        #endregion

        #region << Public Methods >>

        /// <summary>
        /// Adds a new value to filter on with an EqualTo check and returns this object.
        /// </summary>
        /// <param name="value">The value to filter on with an EqualTo check.</param>
        /// <returns>FilterRangeItem.</returns>
        public FilterRangeItem<T> EqualTo(T value)
        {
            return Add(value, FilterRangeTypeEnum.EqualTo);
        }

        /// <summary>
        /// Adds a new value to filter on with an Not EqualTo check and returns this object.
        /// </summary>
        /// <param name="value">The value to filter on with an Not EqualTo check.</param>
        /// <returns>FilterRangeItem.</returns>
        public FilterRangeItem<T> NotEqualTo(T value)
        {
            return Add(value, FilterRangeTypeEnum.NotEqualTo);
        }

        /// <summary>
        /// Adds a new value to filter on with a GreaterThan check and returns this object.
        /// </summary>
        /// <param name="value">The value to filter on with a GreaterThan check.</param>
        /// <returns>FilterRangeItem.</returns>
        public FilterRangeItem<T> GreaterThan(T value)
        {
            return Add(value, FilterRangeTypeEnum.GreaterThan);
        }

        /// <summary>
        /// Adds a new value to filter on with a GreaterThanOrEqualTo check and returns this object.
        /// </summary>
        /// <param name="value">The value to filter on with a GreaterThanOrEqualTo check.</param>
        /// <returns>FilterRangeItem.</returns>
        public FilterRangeItem<T> GreaterThanOrEqualTo(T value)
        {
            return Add(value, FilterRangeTypeEnum.GreaterThanOrEqualTo);
        }

        /// <summary>
        /// Adds a new value to filter on with a LessThan check and returns this object.
        /// </summary>
        /// <param name="value">The value to filter on with a LessThan check.</param>
        /// <returns>FilterRangeItem.</returns>
        public FilterRangeItem<T> LessThan(T value)
        {
            return Add(value, FilterRangeTypeEnum.LessThan);
        }

        /// <summary>
        /// Adds a new value to filter on with a LessThanOrEqualTo check and returns this object.
        /// </summary>
        /// <param name="value">The value to filter on with a LessThanOrEqualTo check.</param>
        /// <returns>FilterRangeItem.</returns>
        public FilterRangeItem<T> LessThanOrEqualTo(T value)
        {
            return Add(value, FilterRangeTypeEnum.LessThanOrEqualTo);
        }

        #endregion

        #region << Helper Methods >>

        /// <summary>
        /// Adds a new value to filter on.
        /// </summary>
        /// <param name="value">The value to filter to check with.</param>
        /// <param name="type">The type of filter to perform.</param>
        /// <returns>FilterRangeItem.</returns>
        private FilterRangeItem<T> Add(T value, FilterRangeTypeEnum type)
        {
            var item = new FilterRangeItem<T>(value, type);
            Filters.Add(item);
            return item;
        }

        #endregion

    }
}
