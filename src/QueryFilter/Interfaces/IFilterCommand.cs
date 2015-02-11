#region << Usings >>

using System;
using System.Collections.Generic;
using QueryFilter.Enums;

#endregion

namespace QueryFilter.Interfaces
{
    /// <summary>
    /// This interface is implemented by command objects that a filter would directly contain.
    /// </summary>
    public interface IFilterCommand
    {
        /// <summary>
        /// Gets the type of command.
        /// </summary>
        FilterCommandTypeEnum CommandType { get; }

        /// <summary>
        /// Gets the total number of Filter Items contained.
        /// </summary>
        int TotalItems { get; }

        /// <summary>
        /// Gets a list of the Filter Items contained. (This will require a .Cast to implement.)
        /// </summary>
        IList<IFilterItem> FilterItems { get; }
    }
}
