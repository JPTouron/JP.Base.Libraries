using JP.Base.MVP;

namespace JP.Base.MVP.Implementation.Contracts.Presenters.Base
{
    /// <summary>
    /// interface to base type-specific presenters that handle views that display model information
    /// </summary>
    public interface IBaseItemPresenter<TModel, TIView> : IPresenter<TIView>
        where TModel : class
        where TIView : IView<TIView>
    {
        /// <summary>
        /// sets and gets the model currently being handled by the presenter
        /// </summary>
        TModel Model { get; set; }

        /// <summary>
        /// commands the presenter to persist a new item based on the model being currently handled
        /// into the database represented by the Model property
        /// </summary>
        void CreateNewItem();

        /// <summary>
        /// commands the presenter to perform and update on the item based on the model being
        /// currently handled into the database represented by the Model property
        /// </summary>
        /// <param name="deleteItem">
        /// is true it indicates that this should be a delete and not an update call
        /// </param>
        void UpdateItem(bool deleteItem);
    }
}