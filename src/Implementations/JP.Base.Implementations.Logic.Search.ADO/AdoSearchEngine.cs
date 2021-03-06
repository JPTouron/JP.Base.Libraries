﻿using JP.Base.Logic.Search;

namespace JP.Base.Implementations.Logic.Search.ADO
{
    public abstract class AdoSearchEngine<EntityType> : SearchEngine<EntityType, SearchEngineResults> where EntityType : class

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
    }
}