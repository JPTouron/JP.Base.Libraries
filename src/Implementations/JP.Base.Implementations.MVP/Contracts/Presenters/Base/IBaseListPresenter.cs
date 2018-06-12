using JP.Base.MVP;

namespace JP.Base.Implementations.MVP.Contracts.Presenters.Base
{
    /// <summary>
    /// interface to base type-specific presenters that handle views that lists items
    /// </summary>
    public interface IBaseListPresenter<TModel, TIView> : IPresenter<TIView>
        where TModel : class
        where TIView : IView<TIView>
    {
        /// <summary>
        /// gets the item that was selected in this presenter
        /// </summary>
        TModel SelectedItem { get; set; }
    }
}