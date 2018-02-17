using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace JP.Base.Logic.Search
{
    public abstract class SearchEngine<EntityType> where EntityType : class
    {
        protected string defaultSortingField;
        protected BaseSearchParams parameters;
        private const string AscendingOrder = "asc";
        private const string DescendingOrder = "desc";
        private SortAndFilterData sortAndFilter;

        public SearchEngine(BaseSearchParams parameters)
        {
            this.parameters = parameters;
            sortAndFilter = parameters.SortAndFilter;
            defaultSortingField = parameters.DefaultSortingField;
        }

        /// <summary>
        /// Main method, you start your filtering/sorting with this
        /// </summary>
        public IQueryable<EntityType> GetSearchQuery()
        {
            var results = VerifySearchDataAssembleSearchQuery(sortAndFilter);

            return results;
        }

        protected virtual IQueryable<EntityType> ApplyOrderBy(IQueryable<EntityType> entities, string orderByKey)
        {
            entities = entities.OrderBy(orderByKey);
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

        /// <summary>
        /// provides a way to modify and parse the sortField used
        /// </summary>
        protected abstract string ParseSortingField(string sortField);

        /// <summary>
        /// viejo executesortandfilter
        /// </summary>
        /// <param name="sortAndFilter"></param>
        /// <returns></returns>
        protected virtual IQueryable<EntityType> SortAndFilterEntities(SortAndFilterData sortAndFilter)
        {
            var searchString = sortAndFilter.SearchString;
            var pageNumber = parameters.SortAndFilter.Page.Value;
            var pageSize = parameters.SortAndFilter.PageSize.Value;

            var skip = pageSize * (pageNumber - 1);
            var take = pageSize;

            var entities = GetMainQuery();

            var searchExp = GetSearchExpression(searchString);

            var filtered = FilterEntities(entities, searchExp);

            if (!string.IsNullOrEmpty(sortAndFilter.SortField))
            {
                var orderByKey = GetSortingKey();
                filtered = ApplyOrderBy(filtered, orderByKey);
            }

            IQueryable locator = BuildRowLocatorQuery(filtered, skip, take);

            IQueryable<EntityType> elements = BuildSortAndFilterQuery(filtered, locator);

            return elements;
        }

        /// <summary>
        /// initiates the search, and refreshes the values to the parameter sortAndFilter
        /// </summary>
        protected IQueryable<EntityType> VerifySearchDataAssembleSearchQuery(SortAndFilterData sortAndfilter)
        {
            SetSearchValues(sortAndfilter);

            var elements = SortAndFilterEntities(sortAndfilter);

            return elements;
        }

        /// <summary>
        /// refreshes and sets required values for search to work (for example when to change the sorting)
        /// </summary>
        private void SetSearchValues(SortAndFilterData sortAndfilter)
        {
            var sortOrder = sortAndfilter.SortOrder;
            var sortField = sortAndfilter.SortField;
            var searchString = sortAndfilter.SearchString;
            var page = sortAndfilter.Page;

            sortAndfilter.SortField = string.IsNullOrEmpty(sortField) ? defaultSortingField : sortField;
            sortAndfilter.SortOrder = string.IsNullOrEmpty(sortOrder) ? AscendingOrder : sortOrder;
            sortAndfilter.PageSize = !sortAndfilter.PageSize.HasValue ? 10 : sortAndfilter.PageSize;

            if (sortAndfilter.SortOrder.ToLower() == DescendingOrder && !string.IsNullOrEmpty(sortOrder))
                sortAndfilter.SortOrder = DescendingOrder;
            else
                sortAndfilter.SortOrder = AscendingOrder;

            if ((searchString != null && page == null) || page == null)
            {
                page = 1;
                sortAndfilter.Page = page;
            }
        }
    }
}