#region << Usings >>

using System.Collections.Generic;
using System.Linq;
using QueryFilter.Enums;
using QueryFilter.Interfaces;

#endregion

namespace QueryFilter
{
    public class FilterGroup : IFilterItem, IFilterCommand
    {

        #region << Public Static Methods >>

        /// <summary>
        /// Creates and returns a new FilterGroup for convienence only.
        /// </summary>
        /// <param name="groupType">The type o group.</param>
        /// <param name="items">The filter items associated with this group.</param>
        /// <returns>FilterGroup.</returns>
        public static FilterGroup New(
            FilterGroupTypeEnum groupType = FilterGroupTypeEnum.And,
            params IFilterItem[] items)
        {
            return new FilterGroup(groupType, items);
        }

        #endregion

        #region << Properties >>

        /// <summary>
        /// Gets or sets the type of Group (And, Or).
        /// </summary>
        public FilterGroupTypeEnum GroupType { get; set; }

        /// <summary>
        /// Gets or sets the Groups involved in this Group.
        /// </summary>
        public IList<IFilterItem> FilterItems { get; private set; }

        /// <summary>
        /// Gets the total number of filters added.
        /// </summary>
        public int TotalItems
        {
            get { return FilterItems == null ? 0 : FilterItems.Count; }
        }

        /// <summary>
        /// Gets the type of command this class represents.
        /// </summary>
        public FilterCommandTypeEnum CommandType
        {
            get { return FilterCommandTypeEnum.Group; }
        }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Constructor to default properties
        /// </summary>
        /// <param name="groupType"></param>
        /// <param name="items"></param>
        public FilterGroup(FilterGroupTypeEnum groupType = FilterGroupTypeEnum.And, params IFilterItem[] items)
        {
            GroupType = groupType;
            FilterItems = items == null ? new List<IFilterItem>() : items.ToList();
        }

        #endregion

    }
}
