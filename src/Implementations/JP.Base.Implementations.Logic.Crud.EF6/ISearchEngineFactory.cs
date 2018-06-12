using JP.Base.Implementations.DAL.Model;
using JP.Base.Logic.Crud;
using JP.Base.Implementations.Logic.Search.EF6;

namespace JP.Base.Implementations.Logic.Crud.EF6
{
    public interface ISearchEngineFactory
    {
        EfSearchEngine<TModel> CreateSearchEngine<TModel, TIdentity>(SearchParams param) where TModel : BaseModel<TIdentity>;
    }
}