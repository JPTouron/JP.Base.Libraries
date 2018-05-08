﻿using JP.Base.DAL.Model;
using JP.Base.Logic.Implementations;
using JP.Base.Logic.Search;

namespace JP.Base.Logic.Contracts
{
    public interface ISearchEngineFactory
    {
        SearchEngine<TModel> CreateSearchEngine<TModel, TIdentity>(SearchParams param) where TModel : BaseModel<TIdentity>;
    }
}