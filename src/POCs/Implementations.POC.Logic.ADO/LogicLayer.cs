using JP.Base.DAL.ADO.UnitOfWork.Contracts;
using JP.Base.DAL.UnitOfWork;
using JP.Base.Logic.ADO;
using JP.Base.Logic.Contracts;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Implementations.POC.Logic.ADO
{

    public class CustomLogic : BaseLogicAdo<OperatorWithClient, OperatorWithClientVM, int>
    {
        public CustomLogic(IUoWFactory<IBaseUnitOfWorkAdo> factory, ISearchEngineFactory searchFac) : base(factory, searchFac)
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

        public OperatorWithClientVM GetJoinedData(int id)
        {

            return base.GetEntityById(id);
        }

        protected override void PerformSpecificValidations(OperatorWithClientVM viewModel)
        {
            throw new NotImplementedException();
        }

        protected override OperatorWithClient ToModel(OperatorWithClientVM viewModel)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<OperatorWithClientVM> ToViewModel(IQueryable<OperatorWithClient> query)
        {
            throw new NotImplementedException();
        }

        protected override OperatorWithClientVM ToViewModel(OperatorWithClient model)
        {

            return new OperatorWithClientVM
            {
                Client = reuse.ToViewModel(model.Client),
                Document = model.Document,
                EmployeeNbr = model.EmployeeNbr,
                FirstName = model.FirstName,
                Id = model.Id,
                IsActive = model.IsActive,
                LastName = model.LastName



            };

        }

        protected override void ValidateId(int id)
        {
         //mehh
        }

        protected override void ValidateModel(object model)
        {
            //mehh

        }
    }

    public class ClientLogic : BaseLogicAdo<Client, ClientVM, int>
    {
        public ClientLogic(IUoWFactory<IBaseUnitOfWorkAdo> factory, ISearchEngineFactory searchFac) : base(factory, searchFac)
        {
        }

        protected override string DefaultSortField
        {
            get
            {
                return "Code";
            }
        }

        protected override string EntityName
        {
            get
            {
                return "Clientes";
            }
        }

        public void Create(ClientVM client)
        {
            base.Create(client);
        }

        public ClientVM GetById(int id)
        {
            return GetEntityById(id);
        }

        public IEnumerable<ClientVM> GetList()
        {
            using (var uow = ((UoWFac)factory).CreateUoW())
            {
                var data = uow.Execute(() =>
                {
                    var res = uow.GetGenericRepo<Client>().Get<Client, object>(x=>x.Code == "");
                    return ToViewModel(res.AsQueryable()).ToList();
                });

                return data;
            }
        }

        public ClientVM Update(ClientVM client)
        {
            return base.Update(client);
        }

        protected override void PerformSpecificValidations(ClientVM viewModel)
        {
            //meh...
        }

        protected override Client ToModel(ClientVM viewModel)
        {
            return new Client
            {
                Code = viewModel.Code,
                Id = viewModel.Id,
                Name = viewModel.Name,
                Version = viewModel.Version
            };
        }

        protected override IEnumerable<ClientVM> ToViewModel(IQueryable<Client> query)
        {
            return query.Select(x => new ClientVM
            {
                Code = x.Code,
                Id = x.Id,
                Name = x.Name
            });
        }

        protected override ClientVM ToViewModel(Client model)
        {
            return reuse.ToViewModel(model);
        }

        protected override void ValidateId(int id)
        {
            //meh...
        }

        protected override void ValidateModel(object model)
        {
            //meh...
        }
    }

    static class reuse
    {

        public static ClientVM ToViewModel(Client model)
        {
            return new ClientVM
            {
                Code = model.Code,
                Id = model.Id,
                Name = model.Name,
            };
        }
    }
}