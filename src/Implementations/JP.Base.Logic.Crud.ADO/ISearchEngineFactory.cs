using JP.Base.DAL.Model;
using JP.Base.Logic.Crud;
using JP.Base.Logic.Search.ADO;

namespace JP.Base.Logic.Crud.ADO
{
    public interface ISearchEngineFactory
    {
        AdoSearchEngine<TModel> CreateSearchEngine<TModel, TIdentity>(SearchParams param) where TModel : BaseModel<TIdentity>;
    }
}