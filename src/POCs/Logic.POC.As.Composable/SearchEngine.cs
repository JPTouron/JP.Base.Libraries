using JP.Base.Logic.Search.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JP.Base.Logic.Search;

namespace Logic.POC.As.Composable
{
    class SearchEngine : AdoSearchEngine<Model>
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
