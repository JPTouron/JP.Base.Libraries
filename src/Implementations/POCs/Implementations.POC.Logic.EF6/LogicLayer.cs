using JP.Base.DAL.UnitOfWork;
using JP.Base.Logic.Contracts;
using JP.Base.Logic.EF6;
using System;
using System.Collections.Generic;
using System.Linq;
using JP.Base.DAL.EF6.UnitOfWork;

namespace Implementations.POC.Logic.EF6
{
    public class EmployeeLogic : BaseLogicEf<Employee, EmployeeVM, int>
    {
        public EmployeeLogic(IUoWFactory<IBaseUnitOfWorkEf> factory, ISearchEngineFactory searchFac) : base(factory, searchFac)
        {
        }


        protected override string DefaultSortField
        {
            get
            {
                return "Name";
            }
        }

        protected override string EntityName
        {
            get
            {
                return "Empleado";
            }
        }


        public IEnumerable<EmployeeVM> GetList() {

            using (var uow =  ((UoWFac)factory).CreateUoW())
            {
                var res = uow.Execute(() => 
                {

                    return uow.GetGenericRepo<Employee>().Get(orderBy: x => x.OrderBy(y => y.Name));
                });


                return ToViewModel( res);
            }
        }

        protected override void PerformSpecificValidations(EmployeeVM viewModel)
        {
            if (viewModel.Age <= 0)
            {
                throw new Exception("mala edad");
            }
        }

        protected override Employee ToModel(EmployeeVM viewModel)
        {
            return new Employee
            {
                Age = viewModel.Age,
                Employer = new Employer
                {
                    Area = viewModel.EmployerArea,
                    Id = viewModel.EmployerId,
                    Name = viewModel.EmployerName
                }
            };
        }

        protected override IEnumerable<EmployeeVM> ToViewModel(IQueryable<Employee> query)
        {
            return query.Select(x => new EmployeeVM
            {
                Id = x.Id,
                Age = x.Age,
                EmployerArea = x.Employer.Area,
                EmployerId = x.EmployerId,
                EmployerName = x.Employer.Name,
                Name = x.Name,
                Function = x.Function,
                Version = x.Version
            });
        }

        protected override EmployeeVM ToViewModel(Employee model)
        {
            return new EmployeeVM
            {
                Id = model.Id,
                Age = model.Age,
                EmployerArea = model.Employer.Area,
                EmployerId = model.EmployerId,
                EmployerName = model.Employer.Name,
                Name = model.Name,
                Function = model.Function,
                Version = model.Version
            };
        }

        protected override void ValidateId(int id)
        {
            //watev, something like id >0
        }

        protected override void ValidateModel(object model)
        {
            //watev, something like model !=null
        }
    }

    public class EmployerLogic : BaseLogicEf<Employer, EmployerVM, int>
    {
        
        public EmployerLogic(IUoWFactory<IBaseUnitOfWorkEf> factory, ISearchEngineFactory searchFac) : base(factory, searchFac)
        {
        }

        protected override string DefaultSortField
        {
            get
            {
                return "Name";
            }
        }

        protected override string EntityName
        {
            get
            {
                return "Empleador";
            }
        }

        protected override void PerformSpecificValidations(EmployerVM viewModel)
        {
            // some validation
        }

        protected override Employer ToModel(EmployerVM viewModel)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<EmployerVM> ToViewModel(IQueryable<Employer> query)
        {
            throw new NotImplementedException();
        }

        protected override EmployerVM ToViewModel(Employer model)
        {
            return new EmployerVM {
                Area = model.Area,
                Id = model.Id,
                Name = model.Name,
                Version = model.Version,
                Employees = model.Employees == null ? null : ToEmployeesList(model.Employees) 
            };
        }


        private IEnumerable<EmployeeVM> ToEmployeesList(ICollection<Employee> emps) {



            return emps.Select(x => new EmployeeVM {
                Id = x.Id,
                Age = x.Age,
                EmployerArea = x.Employer.Area,
                EmployerId = x.EmployerId,
                EmployerName = x.Employer.Name,
                Name = x.Name,
                Function = x.Function,
                Version = x.Version
            });

        }

        protected override void ValidateId(int id)
        {
            // some validation
        }

        protected override void ValidateModel(object model)
        {
            // some validation
        }
    }
}