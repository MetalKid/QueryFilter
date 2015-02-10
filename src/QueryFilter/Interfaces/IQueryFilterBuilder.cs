#region << Usings >>

using System;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace QueryFilter.Interfaces
{
    /// <summary>
    /// This interface defines all public methods on the QueryFilterBuilder object.  This is mainly used for IoC
    /// injection.
    /// </summary>
    public interface IQueryFilterBuilder<TType, TFilter>
        where TType : class 
        where TFilter : class
    {

        /// <summary>
        /// Adds a custom mapping expression in case the default MapToProperty attribute couldn't handle a special
        /// situation.  (i.e. You need to filter on a parent or child entity from the current level.)
        /// </summary>
        /// <typeparam name="TProperty">The type of the property on the entity.</typeparam>
        /// <param name="property">A lamda expression used to return the property of the entity.</param>
        /// <param name="filter">The filter being used against this property.</param>
        /// <returns>QueryFilterBuilder. (For chaining.)</returns>
        QueryFilterBuilder<TType, TFilter> AddCustomMap<TProperty>(
            Expression<Func<TType, TProperty>> property,
            IFilterCommand filter);

        /// <summary>
        /// Returns the query with all filter values applied to the where clause.
        /// </summary>
        /// <param name="query">The query object to add all the Where clause(s) to.</param>
        /// <param name="filter">The filter object holding the values to filter with.</param>
        /// <returns>IQueryable.</returns>
        IQueryable<TType> Build(IQueryable<TType> query, TFilter filter);

        /// <summary>
        /// Clears all expressions from the cache.
        /// </summary>
        void Clear();

    }
}
