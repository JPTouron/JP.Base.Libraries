using JP.Base.DAL.EF6.UnitOfWork;
using JP.Base.DAL.UnitOfWork;
using JP.Base.Logic.Contracts;
using JP.Base.Logic.EF6;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<EmployeeVM> GetList()
        {
            using (var uow = ((UoWFac)factory).CreateUoW())
            {
                var data = uow.Execute(() =>
                {
                    var res = uow.GetGenericRepo<Employee>().Get(orderBy: x => x.OrderBy(y => y.Name));
                    return ToViewModel(res).ToList();
                });

                return data;
            }
        }

        public void CreateEmployee(EmployeeVM employee)
        {
            Create(employee);
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
                EmployerId = viewModel.EmployerId,
                Name = viewModel.Name,
                Function = viewModel.Function,
            };
        }

        protected override IEnumerable<EmployeeVM> ToViewModel(IEnumerable<Employee> query)
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

        public void Create(EmployerVM employer)
        {
            base.Create(employer);
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
            return new Employer
            {
                Area = viewModel.Area,
                Id = viewModel.Id,

                Name = viewModel.Name,
                Version = viewModel.Version
            };
        }

        protected override IEnumerable<EmployerVM> ToViewModel(IEnumerable<Employer> query)
        {
            var data = query.Select(x => new EmployerVM
            {
                Id = x.Id,
                Name = x.Name,
                Version = x.Version,
                Area = x.Area,
                Employees = x.Employees.Select(y => new EmployeeVM
                {
                    Id = y.Id,
                    Age = y.Age,
                    EmployerArea = y.Employer.Area,
                    EmployerId = y.EmployerId,
                    EmployerName = y.Employer.Name,
                    Name = y.Name,
                    Function = y.Function,
                    Version = y.Version
                })
            });

            return data;
        }

        protected override EmployerVM ToViewModel(Employer model)
        {
            return new EmployerVM
            {
                Area = model.Area,
                Id = model.Id,
                Name = model.Name,
                Version = model.Version,
                Employees = model.Employees == null ? null : ToEmployeesList(model.Employees)
            };
        }

        public IEnumerable<EmployerVM> GetList()
        {
            using (var uow = ((UoWFac)factory).CreateUoW())
            {
                var data = uow.Execute(() =>
               {
                   var res = uow.GetGenericRepo<Employer>().Get(orderBy: x => x.OrderBy(y => y.Name));
                   return ToViewModel(res).ToList();
               });

                return data;
            }
        }

        protected override void ValidateId(int id)
        {
            // some validation
        }

        protected override void ValidateModel(object model)
        {
            // some validation
        }

        private IEnumerable<EmployeeVM> ToEmployeesList(ICollection<Employee> emps)
        {
            return emps.Select(x => new EmployeeVM
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
    }
}