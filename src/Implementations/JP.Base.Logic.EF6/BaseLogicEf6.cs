using JP.Base.DAL.EF6.Model;
using JP.Base.DAL.EF6.UnitOfWork;
using JP.Base.DAL.UnitOfWork;
using JP.Base.Logic.Contracts;
using JP.Base.Logic.Implementations;
using JP.Base.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JP.Base.Logic.EF6
{
    public class BaseLogicEf6 : BaseLogic<BaseModelEf<int>, BaseViewModel<int>, IBaseUnitOfWorkEf>
    {
        public BaseLogicEf6(IUoWFactory factory, ISearchEngineFactory searchFac) : base(factory, searchFac)
        {
        }

        protected override string DefaultSortField
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected override string EntityName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected override void ExecuteCreateMethod(BaseModelEf<int> model, IBaseUnitOfWorkEf unitOfWork)
        {
            throw new NotImplementedException();
        }

        protected override void ExecuteDeleteMethod(BaseModelEf<int> model, IBaseUnitOfWorkEf unitOfWork)
        {
            throw new NotImplementedException();
        }

        protected override BaseViewModel<int> ExecuteGetById(int id, IBaseUnitOfWorkEf unitOfWork)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<BaseViewModel<int>> ExecuteSearchMethod(bool getCount, IBaseUnitOfWorkEf unitOfWork, IQueryable<BaseModelEf<int>> searchQuery, ref int totalCount)
        {
            throw new NotImplementedException();
        }

        protected override BaseViewModel<int> ExecuteUpdateMethod(BaseModelEf<int> model, IBaseUnitOfWorkEf unitOfWork)
        {
            throw new NotImplementedException();
        }

        protected override void PerformSpecificValidations(BaseViewModel<int> viewModel)
        {
            throw new NotImplementedException();
        }

        protected override BaseModelEf<int> ToModel(BaseViewModel<int> viewModel)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<BaseViewModel<int>> ToViewModel(IQueryable<BaseModelEf<int>> query)
        {
            throw new NotImplementedException();
        }

        protected override BaseViewModel<int> ToViewModel(BaseModelEf<int> model)
        {
            throw new NotImplementedException();
        }

        protected override void ValidateId(int id)
        {
            throw new NotImplementedException();
        }

        protected override void ValidateModel(object model)
        {
            throw new NotImplementedException();
        }
    }
}