#region << Usings >>

using System.Collections.Generic;
using System.Linq;
using QueryFilter.Enums;
using QueryFilter.Interfaces;

#endregion

namespace QueryFilter
{
    /// <summary>
    /// This class is used to store data about filtering on types that can only be filtered on 
    /// whether they equal each other or not.
    /// </summary>
    /// <typeparam name="T">The type of value this filter is for.</typeparam>
    public class FilterEquatable<T> : IFilterCommand
    {

        #region << Properties >>

        /// <summary>
        /// Gets the list of filters to apply, if any.
        /// </summary>
        public IList<FilterEquatableItem<T>> Filters { get; private set; }

        /// <summary>
        /// Sets a value to filter on with an EqualTo check.
        /// </summary>
        /// <remarks>This is mainly to make it easier to filter on one value.</remarks>
        /// <remarks>Get is here only so JSON can deserialize correctly.</remarks>
        public T Value
        {
            get { return default(T); }
            set { Filters.Add(new FilterEquatableItem<T>(value)); }
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
            get { return FilterCommandTypeEnum.Equatable; }
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
        public FilterEquatable()
        {
            Filters = new List<FilterEquatableItem<T>>();
        }

        /// <summary>
        /// Constuctor that assumes an initial Filter of EqualTo to the given value.
        /// </summary>
        /// <param name="value">The value to filter on with an EqualTo check.</param>
        public FilterEquatable(T value)
            : this()
        {
            Value = value;
        }

        #endregion

        #region << Operators >>

        /// <summary>
        /// Defaults to the EqualTo clause for the given value.
        /// </summary>
        /// <param name="value">The value to perform an equals on</param>
        /// <returns>New instance of FilterEquatable.</returns>
        public static implicit operator FilterEquatable<T>(T value)
        {
            return new FilterEquatable<T>(value);
        }

        #endregion

        #region << Public Methods >>

        /// <summary>
        /// Merges another FilterEquatable into this one.
        /// </summary>
        /// <param name="fe">The FilterEquatable to merge into this one.</param>
        public void Merge(FilterEquatable<T> fe)
        {
            foreach (var filter in fe.Filters)
            {
                Filters.Add(filter);
            }
        }

        /// <summary>
        /// Adds a new value to filter on with an EqualTo check and returns this object.
        /// </summary>
        /// <param name="value">The value to filter on with an EqualTo check.</param>
        /// <returns>FilterEquatable.</returns>
        public FilterEquatableItem<T> EqualTo(T value)
        {
            return Add(value, FilterEquatableTypeEnum.EqualTo);
        }

        /// <summary>
        /// Adds a new value to filter on with an Not EqualTo check and returns this object.
        /// </summary>
        /// <param name="value">The value to filter on with an Not EqualTo check.</param>
        /// <returns>FilterEquatable.</returns>
        public FilterEquatableItem<T> NotEqualTo(T value)
        {
            return Add(value, FilterEquatableTypeEnum.NotEqualTo);
        }

        #endregion

        #region << Helper Methods >>

        /// <summary>
        /// Adds a new value to filter on.
        /// </summary>
        /// <param name="value">The value to filter to check with.</param>
        /// <param name="type">The type of filter to perform.</param>
        /// <returns>FilterEquatable.</returns>
        private FilterEquatableItem<T> Add(T value, FilterEquatableTypeEnum type)
        {
            var item = new FilterEquatableItem<T>(value, type);
            Filters.Add(item);
            return item;
        }

        #endregion
    }
}
