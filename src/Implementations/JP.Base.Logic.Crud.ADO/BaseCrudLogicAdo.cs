using JP.Base.DAL.ADO.UnitOfWork.Contracts;
using JP.Base.DAL.Model;
using JP.Base.DAL.UnitOfWork;
using JP.Base.Logic.Crud;
using JP.Base.Logic.Search;
using JP.Base.Logic.Search.ADO;
using JP.Base.ViewModel;
using System.Linq;

namespace JP.Base.Logic.Crud.ADO
{
    public abstract class BaseCrudLogicAdo<TModel, TViewModel, TIdentity> : BaseCrudLogic<TModel, TViewModel, IBaseUnitOfWorkAdo, TIdentity>
           where TModel : BaseModel<TIdentity>
           where TViewModel : BaseViewModel<TIdentity>

    {
        private ISearchEngineFactory searchFac;

        public BaseCrudLogicAdo(IUoWFactory<IBaseUnitOfWorkAdo> factory, ISearchEngineFactory searchFac) : base(factory)
        {
            this.searchFac = searchFac;
        }

        protected override void ExecuteCreateMethod(TModel model, IBaseUnitOfWorkAdo unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();
            repo.Insert(model);
        }

        protected override void ExecuteDeleteMethod(TModel model, IBaseUnitOfWorkAdo unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            repo.Delete((TModel)model);
        }

        protected override TViewModel ExecuteGetById(TIdentity id, IBaseUnitOfWorkAdo unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            var model = repo.GetById(id);

            return ToViewModel(model);
        }

        protected override SearchResults<TViewModel> ExecuteGetList(SortAndFilterData sortAndFilter, IBaseUnitOfWorkAdo unitOfWork)
        {
            var param = GetSearchParams(sortAndFilter, unitOfWork);
            var engine = GetSearchEngine(param);

            var repo = unitOfWork.GetGenericRepo<TModel>();

            var searchresults = engine.GetSearchQuery();

            var totalCount = 0;

            if (sortAndFilter.GetCount)
                totalCount = repo.Get().Count();

            var entities = repo.Get(searchresults.Filter, searchresults.OrderBy, searchresults.Sort, searchresults.Page, searchresults.PageSize);

            var models = ToViewModel(entities).ToList();

            return new SearchResults<TViewModel> { Results = models, Count = totalCount };
        }

        protected override TViewModel ExecuteUpdateMethod(TModel model, IBaseUnitOfWorkAdo unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            repo.Update((TModel)model);

            return ToViewModel(model);
        }

        /// <summary>
        /// Returns a <seealso cref="SearchEngine{EntityType,TReturnType}"/> by calling <seealso cref="ISearchEngineFactory.CreateSearchEngine{TModel, TIdentity}(SearchParams)"/>
        /// </summary>
        /// <param name="param">the searching params</param>
        protected virtual AdoSearchEngine<TModel> GetSearchEngine(SearchParams param)
        {
            return searchFac.CreateSearchEngine<TModel, TIdentity>(param);
        }
    }
}