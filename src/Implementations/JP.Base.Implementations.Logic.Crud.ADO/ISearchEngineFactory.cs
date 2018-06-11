using JP.Base.Implementations.DAL.Model;
using JP.Base.Logic.Crud;
using JP.Base.Implementations.Logic.Search.ADO;

namespace JP.Base.Implementations.Logic.Crud.ADO
{
    public interface ISearchEngineFactory
    {
        AdoSearchEngine<TModel> CreateSearchEngine<TModel, TIdentity>(SearchParams param) where TModel : BaseModel<TIdentity>;
    }
}