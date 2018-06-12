using JP.Base.DAL.Model;

namespace Logic.POC.As.Composable.Components
{
    public interface IUpdateComponent<TModel>
        where TModel : BaseModel<int>

    {
        void Execute(TModel model);
    }

    public class UpdateComponent : IUpdateComponent<Operator>
    {
        private ComposableOperatorLogic logic;

        public UpdateComponent(ComposableOperatorLogic logic)
        {
            this.logic = logic;
        }

        public void Execute(Operator model)
        {
            logic.Execute((fac) =>
            {
                using (var u = fac.CreateUoW())
                {
                    u.Execute(() =>
                    {
                        var repo = u.GetSpecificRepo<OperatorRepo>();

                        repo.Update(model);
                    }, true);
                }
            });
        }
    }
}