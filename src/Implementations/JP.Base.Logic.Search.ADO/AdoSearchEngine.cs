using JP.Base.DAL.ADO.Commands;

namespace JP.Base.Logic.Search.ADO
{
    public abstract class AdoSearchEngine<EntityType> : SearchEngine<EntityType, CommandData> where EntityType : class

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