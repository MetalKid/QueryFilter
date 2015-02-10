#region << Usings >>

using System.Collections.Generic;
using System.Linq;
using QueryFilter.Enums;
using QueryFilter.Interfaces;

#endregion

namespace QueryFilter
{
    /// <summary>
    /// This class is used to store data about filtering on strings.
    /// </summary>
    public class FilterString : IFilterCommand
    {

        #region << Properties >>

        /// <summary>
        /// Gets the list of filters to apply, if any.
        /// </summary>
        public IList<FilterStringItem> Filters { get; private set; }

        /// <summary>
        /// Sets a value to filter on with an EqualTo check.
        /// </summary>
        /// <remarks>This is mainly to make it easier to filter on one value.</remarks>
        /// <remarks>Get is here only so JSON can deserialize correctly.</remarks>
        public string Value
        {
            get { return null; }
            set { Filters.Add(new FilterStringItem(value)); }
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
            get { return FilterCommandTypeEnum.String; }
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
        public FilterString()
        {
            Filters = new List<FilterStringItem>();
        }

        /// <summary>
        /// Constuctor that assumes an initial Filter of EqualTo to the given value.
        /// </summary>
        /// <param name="value">The value to filter on with an EqualTo check.</param>
        public FilterString(string value)
            : this()
        {
            Value = value;
        }

        #endregion

        #region << Public Methods >>

        /// <summary>
        /// Adds a new value to filter on with an EqualTo check and returns this object.
        /// </summary>
        /// <param name="value">The value to filter on with an EqualTo check.</param>
        /// <returns>FilterStringItem.</returns>
        public FilterStringItem EqualTo(string value)
        {
            return Add(value, FilterStringEnum.EqualTo);
        }

        /// <summary>
        /// Adds a new value to filter on with an Not EqualTo check and returns this object.
        /// </summary>
        /// <param name="value">The value to filter on with an Not EqualTo check.</param>
        /// <returns>FilterStringItem.</returns>
        public FilterStringItem NotEqualTo(string value)
        {
            return Add(value, FilterStringEnum.NotEqualTo);
        }

        /// <summary>
        /// Adds a new value to filter on with a StartsWith check and returns this object.
        /// </summary>
        /// <param name="value">The value to filter on with a StartsWith check.</param>
        /// <returns>FilterStringItem.</returns>
        public FilterStringItem StartsWith(string value)
        {
            return Add(value, FilterStringEnum.StartsWith);
        }

        /// <summary>
        /// Adds a new value to filter on with a NotStartsWith check and returns this object.
        /// </summary>
        /// <param name="value">The value to filter on with a NotStartsWith check.</param>
        /// <returns>FilterStringItem.</returns>
        public FilterStringItem NotStartsWith(string value)
        {
            return Add(value, FilterStringEnum.NotStartsWith);
        }

        /// <summary>
        /// Adds a new value to filter on with a Contains check and returns this object.
        /// </summary>
        /// <param name="value">The value to filter on with a Contains check.</param>
        /// <returns>FilterStringItem.</returns>
        public FilterStringItem Contains(string value)
        {
            return Add(value, FilterStringEnum.Contains);
        }

        /// <summary>
        /// Adds a new value to filter on with a NotContains check and returns this object.
        /// </summary>
        /// <param name="value">The value to filter on with a Contains check.</param>
        /// <returns>FilterStringItem.</returns>
        public FilterStringItem NotContains(string value)
        {
            return Add(value, FilterStringEnum.NotContains);
        }

        /// <summary>
        /// Adds a new value to filter on with an EndsWith check and returns this object.
        /// </summary>
        /// <param name="value">The value to filter on with an EndsWith check.</param>
        /// <returns>FilterStringItem.</returns>
        public FilterStringItem EndsWith(string value)
        {
            return Add(value, FilterStringEnum.EndsWith);
        }

        /// <summary>
        /// Adds a new value to filter on with a NotEndsWith check and returns this object.
        /// </summary>
        /// <param name="value">The value to filter on with a NotEndsWith check.</param>
        /// <returns>FilterStringItem.</returns>
        public FilterStringItem NotEndsWith(string value)
        {
            return Add(value, FilterStringEnum.NotEndsWith);
        }

        #endregion

        #region << Helper Methods >>

        /// <summary>
        /// Adds a new value to filter on.
        /// </summary>
        /// <param name="value">The value to filter to check with.</param>
        /// <param name="type">The type of filter to perform.</param>
        /// <returns>FilterStringItem.</returns>
        private FilterStringItem Add(string value, FilterStringEnum type)
        {
            var item = new FilterStringItem(value, type);
            Filters.Add(item);
            return item;
        }

        #endregion

    }
}
