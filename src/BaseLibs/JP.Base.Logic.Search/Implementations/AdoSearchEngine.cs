using System.Collections.Generic;

namespace JP.Base.Logic.Search.Implementations
{
    public abstract class AdoSearchEngine<T> : SearchEngine<T, IEnumerable<T>> where T : class

    {
        public AdoSearchEngine(BaseSearchParams parameters) : base(parameters)
        {
        }

        protected virtual string GetSortingKey()
        {
            var sortOrder = sortAndFilter.SortOrder;
            var sortField = ParseSortingField(sortAndFilter.SortField);

            var sorterKey = string.Format("{0} {1}", sortField, sortOrder);

            return sorterKey;
        }

        protected override IEnumerable<T> InternalSortAndFilterEntities(string searchString, int skip, int take)
        {
        }
    }
}