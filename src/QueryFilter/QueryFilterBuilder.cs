﻿#region << Usings >>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using QueryFilter.Attributes;
using QueryFilter.Enums;
using QueryFilter.Interfaces;

#endregion

namespace QueryFilter
{
    /// <summary>
    /// This class is used to process the filter expressions and build the query via the Where clause on
    /// IQueryable.
    /// </summary>
    /// <typeparam name="TType">The type of the IQueryable object being filtered on.</typeparam>
    /// <typeparam name="TFilter">The type of the filter.</typeparam>
    public class QueryFilterBuilder<TType, TFilter> : IQueryFilterBuilder<TType, TFilter>
        where TType : class
        where TFilter : class
    {

        #region << Static Methods >>

        /// <summary>
        /// Returns a new instance of QueryFilterBuilder for convienence only.
        /// </summary>
        /// <param name="isCaseSensitive">
        /// True if the result needs to worry about case; false otherwise.
        /// Default - true
        /// </param>
        /// <returns>QueryFilterBuilder.</returns>
        /// <remarks>If this call is for EF and the database doesn't care about case, then this can be false.</remarks>
        public static QueryFilterBuilder<TType, TFilter> New(bool isCaseSensitive = true)
        {
            return new QueryFilterBuilder<TType, TFilter>(isCaseSensitive);
        }

        #endregion

        #region << Variables >>

        private readonly IDictionary<IFilterItem, Expression<Func<TType, bool>>> _expressions =
            new Dictionary<IFilterItem, Expression<Func<TType, bool>>>();
        private readonly IList<IFilterItem> _expressionsProcessed = new List<IFilterItem>();
        private readonly bool _isCaseSensitive;

        #endregion

        #region << Public Methods >>

        /// <summary>
        /// Adds a custom mapping expression in case the default MapToProperty attribute couldn't handle a special
        /// situation.  (i.e. You need to filter on a parent or child entity from the current level.)
        /// </summary>
        /// <typeparam name="TProperty">The type of the property on the entity.</typeparam>
        /// <param name="property">A lamda expression used to return the property of the entity.</param>
        /// <param name="filter">The filter being used against this property.</param>
        /// <returns>QueryFilterBuilder. (For chaining.)</returns>
        public QueryFilterBuilder<TType, TFilter> AddCustomMap<TProperty>(
            Expression<Func<TType, TProperty>> property,
            IFilterCommand filter)
        {
            var entityPropertyName = property.Body.ToString();
            // Get rid of lambda reference
            entityPropertyName = entityPropertyName.Substring(entityPropertyName.IndexOf('.') + 1);
            BuildExpressionsForEntityProperty(entityPropertyName, filter);
            return this;
        }
        
        /// <summary>
        /// Returns the query with all filter values applied to the where clause.
        /// </summary>
        /// <param name="query">The query object to add all the Where clause(s) to.</param>
        /// <param name="filter">The filter object holding the values to filter with.</param>
        /// <returns>IQueryable.</returns>
        public IQueryable<TType> Build(IQueryable<TType> query, TFilter filter)
        {
            BuildExpressionsFromFilter(filter);
            query = ApplyGroupFilterExpressions(query, filter);
            query = ApplyFilterExpressions(query);

            _expressionsProcessed.Clear();
            return query;
        }

        /// <summary>
        /// Clears all expressions from the cache.
        /// </summary>
        public void Clear()
        {
            _expressions.Clear();
        }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Constructor that initializes values.
        /// </summary>
        /// <param name="isCaseSensitive">
        /// True if the result needs to worry about case; false otherwise. 
        /// Default - true
        /// </param>
        /// <remarks>If this call is for EF and the database doesn't care about case, then this can be false.</remarks>
        public QueryFilterBuilder(bool isCaseSensitive = true)
        {
            _isCaseSensitive = isCaseSensitive;
        }

        #endregion

        #region << Expression Methods >>

        /// <summary>
        /// Builds all the expressions for the filter and caches the result.
        /// </summary>
        /// <param name="filter">The filter storing the values to filter on.</param>
        private void BuildExpressionsFromFilter(TFilter filter)
        {
            if (filter == null)
            {
                return;
            }
            var props = filter.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                var attr =
                    (MapToPropertyAttribute)
                        prop.GetCustomAttributes(typeof (MapToPropertyAttribute), false).FirstOrDefault();
                if (attr == null)
                {
                    continue;
                }
                var value = prop.GetValue(filter, null) as IFilterCommand;

                string entityPropertyName = attr.PropertyName ?? prop.Name;
                BuildExpressionsForEntityProperty(entityPropertyName, value);
            }
        }

        /// <summary>
        /// Builds an expression for a specific command and all of its Filter Items and caches the result.
        /// </summary>
        /// <param name="entityPropertyName">The property name of the entity the filters are going against.</param>
        /// <param name="command">The instructions on which expression to build.</param>
        private void BuildExpressionsForEntityProperty(string entityPropertyName, IFilterCommand command)
        {
            if (entityPropertyName == null)
            {
                throw new ArgumentNullException("entityPropertyName");
            }
            if (command == null || command.TotalItems == 0)
            {
                return;
            }
            foreach (var item in command.FilterItems)
            {
                ParameterExpression entityParam = Expression.Parameter(typeof (TType), "a");

                Expression body = entityParam;
                foreach (var member in entityPropertyName.Split('.'))
                {
                    body = Expression.PropertyOrField(body, member);
                }
                if (body.Type == typeof(string) && !(command is FilterString))
                {
                    body = Expression.Property(body, "Length");
                }

                var leftExpression = body;

                switch (command.CommandType)
                {
                    case FilterCommandTypeEnum.Group:
                        break;
                    case FilterCommandTypeEnum.Equatable:
                        GetType()
                            .GetMethod(
                                "CreateFilterEquatableExpression",
                                BindingFlags.NonPublic | BindingFlags.Instance)
                            .MakeGenericMethod(item.GetType().GetGenericArguments()[0])
                            .Invoke(this, new object[] {entityParam, leftExpression, item});
                        break;
                    case FilterCommandTypeEnum.Range:
                        var property = typeof (TType).GetProperty(entityPropertyName);
                        if (property.PropertyType == typeof(string))
                        {
                            property = property.PropertyType.GetProperty("Length");
                        }

                        GetType()
                           .GetMethod(
                               "CreateFilterRangeExpression",
                               BindingFlags.NonPublic | BindingFlags.Instance)
                           .MakeGenericMethod(item.GetType().GetGenericArguments()[0], property.PropertyType)
                           .Invoke(this, new object[] { entityParam, leftExpression, item });
                        break;
                    case FilterCommandTypeEnum.String:
                        CreateFilterStringExpression(entityParam, leftExpression, item as FilterStringItem);
                        break;
                    default:
                        throw new NotImplementedException(
                            "FilterCommandType '" + command.CommandType + " not implemented.");
                }
            }
        }

        /// <summary>
        /// Creates a new expression relating to FilterEquatable types.
        /// </summary>
        /// <typeparam name="T">The type the value the filter is for.</typeparam>
        /// <param name="entityParam">An expression that represents the entity's parameter.</param>
        /// <param name="leftExpression">The left side of the expression equation.</param>
        /// <param name="item">The object to build the full expression from.</param>
        /// <remarks>Have to call this via reflection since we can't call it generically from a PropertyInfo.</remarks>
        private void CreateFilterEquatableExpression<T>(
            ParameterExpression entityParam,
            Expression leftExpression,
            FilterEquatableItem<T> item)
        {
            Expression rightExpression = Expression.Constant(item.Value, typeof(T));

            Expression checkExp = item.Operator == FilterEquatableTypeEnum.EqualTo
                ? Expression.Equal(leftExpression, rightExpression)
                : Expression.NotEqual(leftExpression, rightExpression);

            _expressions[item] = Expression.Lambda<Func<TType, bool>>(checkExp, entityParam);
        }

        /// <summary>
        /// Creates a new expression relating to FilterRange types.
        /// </summary>
        /// <typeparam name="T">The type the value the filter is for.</typeparam>
        /// <typeparam name="TProperty">The type of the entity's property.</typeparam>
        /// <param name="entityParam">An expression that represents the entity's parameter.</param>
        /// <param name="leftExpression">The left side of the expression equation.</param>
        /// <param name="item">The object to build the full expression from.</param>
        /// <remarks>Have to call this via reflection since we can't call it generically from a PropertyInfo.</remarks>
        private void CreateFilterRangeExpression<T, TProperty>(
            ParameterExpression entityParam,
            Expression leftExpression,
            FilterRangeItem<T> item)
            where T: struct
        {
            Expression rightExpression;
            bool isPropertyNullable =
                typeof (TProperty).IsAssignableFrom(typeof (Nullable<>).MakeGenericType(typeof (T)));

            rightExpression = Expression.Constant(
                isPropertyNullable ? item.Value : item.NonNullableValue,
                typeof (TProperty));
            
            Expression checkExp;
            switch (item.Operator)
            {
                case FilterRangeTypeEnum.EqualTo:
                    checkExp = Expression.Equal(leftExpression, rightExpression);
                    break;
                case FilterRangeTypeEnum.NotEqualTo:
                    checkExp = Expression.NotEqual(leftExpression, rightExpression);
                    break;
                case FilterRangeTypeEnum.GreaterThan:
                    checkExp = Expression.GreaterThan(leftExpression, rightExpression);
                    break;
                case FilterRangeTypeEnum.GreaterThanOrEqualTo:
                    checkExp = Expression.GreaterThanOrEqual(leftExpression, rightExpression);
                    break;
                case FilterRangeTypeEnum.LessThan:
                    checkExp = Expression.LessThan(leftExpression, rightExpression);
                    break;
                case FilterRangeTypeEnum.LessThanOrEqualTo:
                    checkExp = Expression.LessThanOrEqual(leftExpression, rightExpression);
                    break;
                default:
                    throw new NotImplementedException(
                        "FilterRangeTypeEnum '" + item.Operator + " not implemented.");
            }

            _expressions.Add(item, Expression.Lambda<Func<TType, bool>>(checkExp, entityParam));
        }

        /// <summary>
        /// Creates a new expression relating to FilterString types.
        /// </summary>
        /// <param name="entityParam">An expression that represents the entity's parameter.</param>
        /// <param name="leftExpression">The left side of the expression equation.</param>
        /// <param name="item">The object to build the full expression from.</param>
        private void CreateFilterStringExpression(
            ParameterExpression entityParam,
            Expression leftExpression,
            FilterStringItem item)
        {
            Expression rightExpression;
            if (_isCaseSensitive && item.IgnoreCase)
            {
                leftExpression = Expression.Call(
                    leftExpression,
                    "ToLower",
                    new Type[0]); // NOTE: Cannot use CultureInfo here or Entity Framework will blow up.
                rightExpression = Expression.Constant(item.Value == null ? null : item.Value.ToLower(), typeof(string));
            }
            else
            {
                rightExpression = Expression.Constant(item.Value, typeof(string));
            }

            Expression checkExp;
            switch (item.Operator)
            {
                case FilterStringEnum.EqualTo:
                    checkExp = Expression.Equal(leftExpression, rightExpression);
                    break;
                case FilterStringEnum.NotEqualTo:
                    checkExp = Expression.NotEqual(leftExpression, rightExpression);
                    break;
                case FilterStringEnum.Contains:
                {
                    var method = rightExpression.Type.GetMethod("Contains", new[] {typeof (string)});
                    checkExp = Expression.Call(leftExpression, method, new[] {rightExpression});
                    break;
                }
                case FilterStringEnum.NotContains:
                {
                    var method = rightExpression.Type.GetMethod("Contains", new[] {typeof (string)});
                    checkExp = Expression.Not(Expression.Call(leftExpression, method, new[] {rightExpression}));
                    break;
                }
                case FilterStringEnum.StartsWith:
                {
                    var method = rightExpression.Type.GetMethod("StartsWith", new[] {typeof (string)});
                    checkExp = Expression.Call(leftExpression, method, new[] {rightExpression});
                    break;
                }
                case FilterStringEnum.NotStartsWith:
                {
                    var method = rightExpression.Type.GetMethod("StartsWith", new[] {typeof (string)});
                    checkExp = Expression.Not(Expression.Call(leftExpression, method, new[] {rightExpression}));
                    break;
                }
                case FilterStringEnum.EndsWith:
                {
                    var method = rightExpression.Type.GetMethod("EndsWith", new[] {typeof (string)});
                    checkExp = Expression.Call(leftExpression, method, new[] {rightExpression});
                    break;
                }
                case FilterStringEnum.NotEndsWith:
                {
                    var method = rightExpression.Type.GetMethod("EndsWith", new[] {typeof (string)});
                    checkExp = Expression.Not(Expression.Call(leftExpression, method, new[] {rightExpression}));
                    break;
                }
                default:
                    throw new NotImplementedException(
                        "FilterStringEnum '" + item.Operator + " not implemented.");
            }

            _expressions[item] = Expression.Lambda<Func<TType, bool>>(checkExp, entityParam);
        }

        #endregion

        #region << Where Methods >>

        /// <summary>
        /// Applies all the expressions that need to be grouped together, recursively.
        /// </summary>
        /// <param name="query">The query object to add all the Where clause(s) to.</param>
        /// <param name="filter">The filter object holding the values to filter with.</param>
        /// <returns>IQueryable.</returns>
        private IQueryable<TType> ApplyGroupFilterExpressions(IQueryable<TType> query, TFilter filter)
        {
            IFilterGroup filterGroup = filter as IFilterGroup;
            if (filterGroup == null || filterGroup.FilterGroups == null || filterGroup.FilterGroups.Count == 0)
            {
                return query;
            }

            Expression result = GetExpression(filterGroup.FilterGroups[0]);
            for (int i = 1; i < filterGroup.FilterGroups.Count; i++)
            {
                var group = filterGroup.FilterGroups[i];
                switch (group.GroupType)
                {
                    case FilterGroupTypeEnum.Or:
                        result = Expression.OrElse(result, GetExpression(group));
                        break;
                    case FilterGroupTypeEnum.And:
                        result = Expression.AndAlso(result, GetExpression(group));
                        break;
                    default:
                        throw new NotImplementedException(
                            "GroupType '" + group.GroupType.ToString() + "' not implemented.");
                }
            }
            ParameterExpression param = Expression.Parameter(typeof(TType), "a");
            ParameterReplacer replacer = new ParameterReplacer(param);
            result = replacer.Visit(result);
            return query.Where(Expression.Lambda<Func<TType, bool>>(result, param));
        }

        /// <summary>
        /// Returns the expression to include in the WhereClause given the current group.
        /// </summary>
        /// <param name="group">The Group to build the expression from.</param>
        /// <returns>Expression to add to a Where clause.</returns>
        private Expression GetExpression(FilterGroup group)
        {
            var items = group.FilterItems;
            Expression result;
            var fgCheck = items[0] as FilterGroup;
            if (fgCheck != null)
            {
                result = GetExpression(fgCheck);
            }
            else
            {
                Expression<Func<TType, bool>> exp;
                if (!_expressions.TryGetValue(items[0], out exp))
                {
                    exp = _expressions.Single(a => a.Key.Id == items[0].Id).Value;
                }
                _expressionsProcessed.Add(items[0]);
                result = exp.Body;
            }

            for (int i = 1; i < items.Count; i++)
            {
                result = GetGroupExpession(group.GroupType, result, items[i]);
            }

            return result;
        }

        /// <summary>
        /// Builds the resulting expression for one specific Filter Item.
        /// </summary>
        /// <param name="groupType">The type of expression to create.</param>
        /// <param name="result">The current query so far.</param>
        /// <param name="item">The current Filter Item being added to the query.</param>
        /// <returns>Expression.</returns>
        private Expression GetGroupExpession(FilterGroupTypeEnum groupType, Expression result, IFilterItem item)
        {
            var fg = item as FilterGroup;
            Expression rightExpression;
            if (fg != null)
            {
                rightExpression = GetExpression(fg);
            }
            else
            {
                Expression<Func<TType, bool>> exp;
                if (!_expressions.TryGetValue(item, out exp))
                {
                    exp = _expressions.Single(a => a.Key.Id == item.Id).Value;
                }
                rightExpression = exp.Body;
            }

            switch (groupType)
            {
                case FilterGroupTypeEnum.And:
                    result = Expression.AndAlso(result, rightExpression);
                    break;
                case FilterGroupTypeEnum.Or:
                    result = Expression.OrElse(result, rightExpression);
                    break;
                default:
                    throw new NotImplementedException(
                        "FilterGroupTypeEnum '" + groupType + "' not implemented.");
            }
            _expressionsProcessed.Add(item);
            return result;
        }

        /// <summary>
        /// Applies any expressions that were not part of any group. These are "and"ed together at the end.
        /// </summary>
        /// <param name="query">The query object to add all the Where clause(s) to.</param>
        /// <returns>IQueryable.</returns>
        private IQueryable<TType> ApplyFilterExpressions(IQueryable<TType> query)
        {
            foreach (var item in _expressions)
            {
                if (_expressionsProcessed.Contains(item.Key))
                {
                    continue;
                }
                query = query.Where(item.Value);
            }
            return query;
        }

        #endregion

        #region << Private Classes >>

        /// <summary>
        /// This class is used to convert the entity parameter's reference for each individual where clause
        /// to use the same reference since we only pass an entity into the clause once for FilterGroups.
        /// </summary>
        private class ParameterReplacer : ExpressionVisitor
        {
            private readonly ParameterExpression _newParameter;

            internal ParameterReplacer(ParameterExpression parameter)
            {
                _newParameter = parameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return _newParameter;
            }
        }

        #endregion

    }
}
