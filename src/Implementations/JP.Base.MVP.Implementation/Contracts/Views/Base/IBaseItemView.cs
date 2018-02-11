using System;

namespace JP.Base.MVP.Implementation.Contracts.Views.Base
{
    /// <summary>
    /// interface to base all views that display model information
    /// </summary>
    public interface IBaseItemView<TModel, TIView> : IBaseModelView<TModel>, IBaseModelValidationView, IBaseView<TIView>
        where TModel : class
        where TIView : IView<TIView>
    {
        event EventHandler NotifyPerformCreateItem;

        event EventHandler<bool> NotifyPerformUpdateItem;

        event NotifyDisplayConfirmDeletionMessageHandler NotifyUIDisplayConfirmDeletionMessage;

        /// <summary>
        /// enables the view to display a delete confirmation message
        /// </summary>
        bool DisplayConfirmDeletionMessage(string message);

        /// <summary>
        /// signals to create as new the item being currently displayed by the view and represented
        /// in Model
        /// </summary>
        bool PerformCreateItem(TModel model);

        /// <summary>
        /// signals to delete the item being currently displayed by the view and represented in Model
        /// </summary>
        bool PerformDeleteItem(TModel model);

        /// <summary>
        /// signals to update the item being currently displayed by the view and represented in Model
        /// </summary>
        bool PerformUpdateItem(TModel model);
    }
}