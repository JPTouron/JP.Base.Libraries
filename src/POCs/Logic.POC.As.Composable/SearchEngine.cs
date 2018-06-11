using JP.Base.Implementations.Logic.Search.ADO;
using JP.Base.Logic.Search;
using System;

namespace Logic.POC.As.Composable
{
    internal class SearchEngine : AdoSearchEngine<Operator>
    {
        public SearchEngine(BaseSearchParams parameters) : base(parameters)
        {
        }

        protected override SearchEngineResults InternalSortAndFilterEntities(string searchString, int skip, int take)
        {
            throw new NotImplementedException();
        }

        protected override string ParseSortingField(string sortField)
        {
            throw new NotImplementedException();
        }
    }
}