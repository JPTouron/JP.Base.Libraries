using JP.Base.DAL.ADO.UnitOfWork.Contracts;
using JP.Base.DAL.UnitOfWork;
using JP.Base.Logic.ADO;
using JP.Base.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.POC.Logic.ADO
{
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

        protected override void PerformSpecificValidations(ClientVM viewModel)
        {
            //meh...
        }


        public IEnumerable<ClientVM> GetList()
        {
            using (var uow = ((UoWFac)factory).CreateUoW())
            {
                var data = uow.Execute(() =>
                {
                    var res = uow.GetGenericRepo<Client>().Get<Client,object>();                    
                    return ToViewModel(res.AsQueryable()).ToList();
                });

                return data;
            }
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
            throw new NotImplementedException();
        }

        protected override ClientVM ToViewModel(Client model)
        {

            return new ClientVM
            {

                Code = model.Code,
                Id = model.Id,
                Name = model.Name,
                Version = model.Version

            };

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
}
