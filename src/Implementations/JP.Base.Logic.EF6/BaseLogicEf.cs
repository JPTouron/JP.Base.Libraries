using JP.Base.DAL.EF6.Model;
using JP.Base.DAL.EF6.UnitOfWork;
using JP.Base.DAL.UnitOfWork;
using JP.Base.Logic.Contracts;
using JP.Base.Logic.Implementations;
using JP.Base.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace JP.Base.Logic.EF6
{
    public abstract class BaseLogicEf<TModel, TViewModel, TIdentity> : BaseLogic<BaseModelEf<TIdentity>, BaseViewModel<TIdentity>, IBaseUnitOfWorkEf, TIdentity>
        where TModel : BaseModelEf<TIdentity>
        where TViewModel : BaseViewModel<TIdentity>
    {
        public BaseLogicEf(IUoWFactory factory, ISearchEngineFactory searchFac) : base(factory, searchFac)
        {
        }

        protected override void ExecuteCreateMethod(BaseModelEf<TIdentity> model, IBaseUnitOfWorkEf unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();
            repo.Insert((TModel)model);
        }

        protected override void ExecuteDeleteMethod(BaseModelEf<TIdentity> model, IBaseUnitOfWorkEf unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            repo.AttachEntity((TModel)model);

            repo.Delete((TModel)model);
        }

        protected override BaseViewModel<TIdentity> ExecuteGetById(TIdentity id, IBaseUnitOfWorkEf unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            var model = repo.GetById(id);

            return ToViewModel(model);
        }

        protected override IEnumerable<BaseViewModel<TIdentity>> ExecuteSearchMethod(bool getCount, IBaseUnitOfWorkEf unitOfWork, IQueryable<BaseModelEf<TIdentity>> searchQuery, ref int totalCount)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            if (getCount)
                totalCount = repo.Get().Count();

            var models = ToViewModel(searchQuery).ToList();

            return models;
        }

        protected override BaseViewModel<TIdentity> ExecuteUpdateMethod(BaseModelEf<TIdentity> model, IBaseUnitOfWorkEf unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            repo.AttachEntity((TModel)model);

            repo.Update((TModel)model);

            return ToViewModel(model);
        }
    }
}