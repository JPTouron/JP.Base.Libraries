using JP.Base.Logic.Search;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace JP.Base.Implementations.Logic.Search.EF6
{
    public abstract class EfSearchEngine<EntityType> : SearchEngine<EntityType, IQueryable<EntityType>> where EntityType : class
    {
        public EfSearchEngine(BaseSearchParams parameters) : base(parameters)
        {
        }

        protected virtual IQueryable<EntityType> ApplyOrderBy(IQueryable<EntityType> entities, string orderByKey)
        {
            entities = entities.OrderBy(orderByKey);// from System.Linq.Dynamic
            return entities;
        }

        protected abstract IQueryable BuildRowLocatorQuery(IQueryable<EntityType> filteredEntities, int skip, int take);

        /// <summary>
        /// builds the final sort and filter query
        /// </summary>
        /// <returns>
        /// returns a queryable expression that represents the final expression to be executed
        /// against the DB
        /// </returns>
        protected abstract IQueryable<EntityType> BuildSortAndFilterQuery(IQueryable<EntityType> filtered, IQueryable locator);

        protected virtual IQueryable<EntityType> FilterEntities(IQueryable<EntityType> entities, Expression<Func<EntityType, bool>> searchExp)
        {
            var filtered = searchExp == null ? entities : entities.Where(searchExp);
            return filtered;
        }

        protected abstract IQueryable<EntityType> GetMainQuery();

        /// <summary>
        /// when implemented it allows to define an expression that will work as the filtering
        /// expression for the query <example>return Expression[Func[User, bool]] lambda = u =&gt; u.Operator.LastName.Contains(searchString);</example>
        /// </summary>
        /// <param name="searchString">the value that is pretended to filter in the query</param>
        protected abstract Expression<Func<EntityType, bool>> GetSearchExpression(string searchString);

        protected virtual string GetSortingKey()
        {
            var sortOrder = sortAndFilter.SortOrder;
            var sortField = ParseSortingField(sortAndFilter.SortField);

            var sorterKey = string.Format("{0} {1}", sortField, sortOrder);

            return sorterKey;
        }

        protected override IQueryable<EntityType> InternalSortAndFilterEntities(string searchString, int skip, int take)
        {
            var entities = GetMainQuery();

            var searchExp = GetSearchExpression(searchString);

            var filtered = FilterEntities(entities, searchExp);

            var orderByKey = GetSortingKey();

            filtered = ApplyOrderBy(filtered, orderByKey);

            IQueryable locator = BuildRowLocatorQuery(filtered, skip, take);

            var elements = BuildSortAndFilterQuery(filtered, locator);

            return elements;
        }
    }
}