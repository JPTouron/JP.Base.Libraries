namespace JP.Base.Logic.Search
{
    public abstract class SearchEngine<EntityType, ReturnType>
        where EntityType : class
    {
        protected const string AscendingOrder = "asc";
        protected const string DescendingOrder = "desc";
        protected string defaultSortingField;
        protected BaseSearchParams parameters;
        protected SortAndFilterData sortAndFilter;

        public SearchEngine(BaseSearchParams parameters)
        {
            this.parameters = parameters;
            sortAndFilter = parameters.SortAndFilter;
            defaultSortingField = parameters.DefaultSortingField;
        }

        /// <summary>
        ///Main method, you start your filtering/sorting with this
        /// </summary>
        public ReturnType GetSearchQuery()
        {
            var results = VerifySearchDataAssembleSearchQuery(sortAndFilter);

            return results;
        }

        protected abstract ReturnType InternalSortAndFilterEntities(string searchString, int skip, int take);

        /// <summary>
        /// provides a way to modify, verify and parse the sortField used
        /// </summary>
        protected abstract string ParseSortingField(string sortField);

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

        /// <summary>
        /// viejo executesortandfilter
        /// </summary>
        /// <param name="sortAndFilter"></param>
        /// <returns></returns>
        private ReturnType SortAndFilterEntities(SortAndFilterData sortAndFilter)
        {
            var searchString = sortAndFilter.SearchString;
            var pageNumber = parameters.SortAndFilter.Page.Value;
            var pageSize = parameters.SortAndFilter.PageSize.Value;

            var skip = pageSize * (pageNumber - 1);
            var take = pageSize;

            return InternalSortAndFilterEntities(searchString, skip, take);
        }

        /// <summary>
        /// initiates the search, and refreshes the values to the parameter sortAndFilter
        /// </summary>
        private ReturnType VerifySearchDataAssembleSearchQuery(SortAndFilterData sortAndfilter)
        {
            SetSearchValues(sortAndfilter);

            var elements = SortAndFilterEntities(sortAndfilter);

            return elements;
        }
    }
}