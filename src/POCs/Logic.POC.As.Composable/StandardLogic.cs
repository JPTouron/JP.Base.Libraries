using JP.Base.DAL.UnitOfWork;
using JP.Base.Logic;

namespace Logic.POC.As.Composable
{
    //standard more reduced logic, tailored only to get and update methods
    public class StandardOperatorLogic : BaseLogic<Uow>

    {
        public StandardOperatorLogic(IUoWFactory<Uow> factory) : base(factory)
        {
        }

        public Operator GetById(int id)
        {
            using (var u = factory.CreateUoW())
            {
                return u.Execute<Operator>(() =>
                {
                    var repo = u.GetSpecificRepo<OperatorRepo>();

                    return repo.GetById(id);
                });
            }
        }

        public void Update(Operator model)
        {
            using (var u = factory.CreateUoW())
            {
                u.Execute(() =>
                {
                    var repo = u.GetSpecificRepo<OperatorRepo>();

                    repo.Update(model);
                }, true);
            }
        }
    }
}