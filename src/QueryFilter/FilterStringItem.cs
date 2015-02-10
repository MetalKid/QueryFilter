#region << Usings >>

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
        /// Gets or sets the value to filter on.
        /// </summary>
        public string Value { get; set; }

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
        public FilterStringItem(
            string value = null, 
            FilterStringEnum rangeType = FilterStringEnum.EqualTo)
        {
            Value = value;
            Operator = rangeType;
        }

        #endregion

    }
}
