using JP.Base.Implementations.MVP.Contracts.Views.Base;
using JP.Base.MVP;

namespace JP.Base.Implementations.MVP.Support.Factories
{
    public interface IViewsFactory
    {
        TIView CreateItemView<TModel, TIView>()
            where TIView : IBaseItemView<TModel, TIView>
            where TModel : class;

        TIView CreateListView<TModel, TIView>()
            where TIView : IBaseListView<TModel, TIView>
            where TModel : class;

        TIView CreateView<TIView>() where TIView : IView<TIView>;
    }

    public static class ViewProvider
    {
        public static IViewsFactory ViewFactory { get; set; }
    }
}