using JP.Base.DAL.Model;
using JP.Base.Logic.Crud;
using JP.Base.Logic.Search.EF6;

namespace JP.Base.Logic.Crud.EF6
{
    public interface ISearchEngineFactory
    {
        EfSearchEngine<TModel> CreateSearchEngine<TModel, TIdentity>(SearchParams param) where TModel : BaseModel<TIdentity>;
    }
}