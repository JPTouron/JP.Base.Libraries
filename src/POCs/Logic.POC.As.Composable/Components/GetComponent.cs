using JP.Base.DAL.Model;

namespace Logic.POC.As.Composable.Components
{
    public interface IGetComponent<TModel>
         where TModel : BaseModel<int>

    {
        TModel Execute(int id);
    }

    public class GetComponent : IGetComponent<Operator>
    {
        private ComposableOperatorLogic logic;

        public GetComponent(ComposableOperatorLogic logic)
        {
            this.logic = logic;
        }

        public Operator Execute(int id)
        {
            return logic.Execute<Operator>((fac) =>
            {
                using (var u = fac.CreateUoW())
                {
                    return u.Execute<Operator>(() =>
                            {
                                var repo = u.GetSpecificRepo<OperatorRepo>();

                                return repo.GetById(id);
                            });
                }
            });
        }
    }
}