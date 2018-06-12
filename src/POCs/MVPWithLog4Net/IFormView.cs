using JP.Base.Implementations.MVP.Contracts.Views.Base;

namespace MVPWithLog4Net
{
    public interface IFormView : IBaseModelView<Model>, IBaseModelValidationView, IBaseView<IFormView>
    {
    }
}