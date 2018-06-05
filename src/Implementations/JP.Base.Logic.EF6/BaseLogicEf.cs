using JP.Base.DAL.EF6.Model;
using JP.Base.DAL.EF6.UnitOfWork;
using JP.Base.DAL.UnitOfWork;
using JP.Base.Logic.Contracts;
using JP.Base.Logic.Implementations;
using JP.Base.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System;
using JP.Base.Logic.Search;

namespace JP.Base.Logic.EF6
{
    public abstract class BaseLogicEf<TModel, TViewModel, TIdentity> : BaseLogic<TModel, TViewModel, IBaseUnitOfWorkEf, TIdentity>
        where TModel : BaseModelEf<TIdentity>
        where TViewModel : BaseViewModel<TIdentity>
    {
        public BaseLogicEf(IUoWFactory<IBaseUnitOfWorkEf> factory, ISearchEngineFactory searchFac) : base(factory, searchFac)
        {
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


        protected override SearchResults<TViewModel> GetList(SortAndFilterData sortAndFilter)
        {
            using (var unitOfWork = factory.CreateUoW())
            {
                var param = GetSearchParams(sortAndFilter, unitOfWork);
                var search = GetSearchEngine(param);

                var searchQuery = search.GetSearchQuery();
                var totalCount = 0;

                var res = unitOfWork.Execute(() =>
                {
                    return ExecuteSearchMethod(sortAndFilter.GetCount, unitOfWork, searchQuery, ref totalCount);
                });

                return new SearchResults<TViewModel> { Results = res, Count = totalCount };
            }
        }

        protected override IEnumerable<TViewModel> ExecuteSearchMethod(bool getCount, IBaseUnitOfWorkEf unitOfWork, IQueryable<TModel> searchQuery, ref int totalCount)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            if (getCount)
                totalCount = repo.Get().Count();

            var models = ToViewModel(searchQuery).ToList();

            return models;
        }

        protected override TViewModel ExecuteUpdateMethod(TModel model, IBaseUnitOfWorkEf unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            repo.AttachEntity((TModel)model);

            repo.Update((TModel)model);

            return ToViewModel(model);
        }
    }
}