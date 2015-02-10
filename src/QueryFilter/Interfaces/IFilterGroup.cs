#region << Usings >>

using System.Collections.Generic;

#endregion

namespace QueryFilter.Interfaces
{
    /// <summary>
    /// This interface is used by a Filter to support Filter Groups.
    /// </summary>
    public interface IFilterGroup
    {

        /// <summary>
        /// Gets or sets top level filter groupings.
        /// </summary>
        IList<FilterGroup> FilterGroups { get; set; }

    }
}
