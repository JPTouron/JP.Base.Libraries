using JP.Base.DAL.Model.Concurrent;
using JP.Base.DAL.UnitOfWork;
using JP.Base.Implementations.DAL.EF6.UnitOfWork;
using JP.Base.Implementations.Logic.Search.EF6;
using JP.Base.Logic.Crud;
using JP.Base.Logic.Search;
using JP.Base.Logic.ViewModel;
using System.Linq;

namespace JP.Base.Implementations.Logic.Crud.EF6
{
    public abstract class BaseCrudLogicEf<TModel, TViewModel, TIdentity> : BaseCrudLogic<TModel, TViewModel, IBaseUnitOfWorkEf, TIdentity>
        where TModel : BaseModelConcurrent<TIdentity>
        where TViewModel : BaseViewModel<TIdentity>
    {
        private ISearchEngineFactory searchFac;

        public BaseCrudLogicEf(IUoWFactory<IBaseUnitOfWorkEf> factory, ISearchEngineFactory searchFac) : base(factory)
        {
            this.searchFac = searchFac;
        }

        protected override void ExecuteCreateMethod(TModel model, IBaseUnitOfWorkEf unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();
            repo.Insert(model);
        }

        protected override void ExecuteDeleteMethod(TModel model, IBaseUnitOfWorkEf unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            repo.AttachEntity(model);

            repo.Delete((TModel)model);
        }

        protected override TViewModel ExecuteGetById(TIdentity id, IBaseUnitOfWorkEf unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            var model = repo.GetById(id);

            return ToViewModel(model);
        }

        protected override SearchResults<TViewModel> ExecuteGetList(SortAndFilterData sortAndFilter, IBaseUnitOfWorkEf unitOfWork)
        {
            var param = GetSearchParams(sortAndFilter, unitOfWork);
            var search = GetSearchEngine(param);

            var searchQuery = search.GetSearchQuery();
            var totalCount = 0;

            var repo = unitOfWork.GetGenericRepo<TModel>();

            if (sortAndFilter.GetCount)
                totalCount = repo.Get().Count();

            var models = ToViewModel(searchQuery).ToList();

            return new SearchResults<TViewModel> { Results = models, Count = totalCount };
        }

        protected override TViewModel ExecuteUpdateMethod(TModel model, IBaseUnitOfWorkEf unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            repo.AttachEntity((TModel)model);

            repo.Update((TModel)model);

            return ToViewModel(model);
        }

        /// <summary>
        /// Returns a <seealso cref="SearchEngine{EntityType,TReturnType}"/> by calling <seealso cref="ISearchEngineFactory.CreateSearchEngine{TModel, TIdentity}(SearchParams)"/>
        /// </summary>
        /// <param name="param">the searching params</param>
        protected virtual EfSearchEngine<TModel> GetSearchEngine(SearchParams param)
        {
            return searchFac.CreateSearchEngine<TModel, TIdentity>(param);
        }
    }
}