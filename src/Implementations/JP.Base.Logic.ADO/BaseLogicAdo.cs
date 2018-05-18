using JP.Base.DAL.ADO.UnitOfWork.Contracts;
using JP.Base.DAL.Model;
using JP.Base.DAL.UnitOfWork;
using JP.Base.Logic.Contracts;
using JP.Base.Logic.Implementations;
using JP.Base.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JP.Base.Logic.ADO
{
    public abstract class BaseLogicAdo<TModel, TViewModel, TIdentity> : BaseLogic<TModel, TViewModel, IBaseUnitOfWorkAdo, TIdentity>
           where TModel : BaseModel<TIdentity>
           where TViewModel : BaseViewModel<TIdentity>
    {
        public BaseLogicAdo(IUoWFactory<IBaseUnitOfWorkAdo> factory, ISearchEngineFactory searchFac) : base(factory, searchFac)
        {
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

        protected override IEnumerable<TViewModel> ExecuteSearchMethod(bool getCount, IBaseUnitOfWorkAdo unitOfWork, IQueryable<TModel> searchQuery, ref int totalCount)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            if (getCount)
                totalCount = repo.Get<TModel,object>().Count();

            var models = ToViewModel(searchQuery).ToList();

            return models;
        }

        protected override TViewModel ExecuteUpdateMethod(TModel model, IBaseUnitOfWorkAdo unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            repo.Update((TModel)model);

            return ToViewModel(model);
        }
    }
}
