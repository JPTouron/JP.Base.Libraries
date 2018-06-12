using JP.Base.DAL.Model;
using JP.Base.Implementations.Logic.Search.EF6;
using JP.Base.Logic.Crud;

namespace JP.Base.Implementations.Logic.Crud.EF6
{
    public interface ISearchEngineFactory
    {
        EfSearchEngine<TModel> CreateSearchEngine<TModel, TIdentity>(SearchParams param) where TModel : BaseModel<TIdentity>;
    }
}