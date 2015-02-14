#region << Usings >>

using System;

#endregion

namespace QueryFilter.Interfaces
{
    /// <summary>
    /// This interface is used for Filter Items to prove the framework code is working with a FilterItem.
    /// </summary>
    public interface IFilterItem
    {
        /// <summary>
        /// Gets a key to this command in order to maintain the FilterGroup relationships after deserializing JSON.
        /// </summary>
        string Id { get; }
    }
}
