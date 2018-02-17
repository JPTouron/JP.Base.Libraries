using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JP.Base.Logic.Search.Tests.Implementation
{
    public class Model
    {
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }

    public class SearchEngineImpl : SearchEngine<Model>
    {
        private IList<Model> models;

        public SearchEngineImpl(BaseSearchParams parameters) : base(parameters)
        {
            models = new List<Model>
            {
                new Model { Cost=0.3m, Description="test", Id=1 }
            };
        }

        protected override IQueryable BuildRowLocatorQuery(IQueryable<Model> filteredEntities, int skip, int take)
        {
            return filteredEntities;
        }

        protected override IQueryable<Model> BuildSortAndFilterQuery(IQueryable<Model> filtered, IQueryable locator)
        {
            return filtered;
        }

        protected override IQueryable<Model> GetMainQuery()
        {
            return models.AsQueryable();
        }

        protected override Expression<Func<Model, bool>> GetSearchExpression(string searchString)
        {
            return null;
        }

        protected override string ParseSortingField(string sortField)
        {
            return sortField;
        }
    }
}