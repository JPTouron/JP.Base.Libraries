using JP.Base.DAL.Model;
using JP.Base.Logic.Implementations;
using JP.Base.Logic.Search.ADO;

namespace JP.Base.Logic.ADO
{
    public interface ISearchEngineFactory
    {
        AdoSearchEngine<TModel> CreateSearchEngine<TModel, TIdentity>(SearchParams param) where TModel : BaseModel<TIdentity>;
    }
}