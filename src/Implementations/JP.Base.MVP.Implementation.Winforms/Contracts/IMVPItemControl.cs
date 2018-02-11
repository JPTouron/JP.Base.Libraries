namespace JP.Base.MVP.Implementation.Winforms
{
    public interface IMVPItemControl
    {
        event NotifyUpdatedItemHandler NotifyUpdatedItem;

        /// <summary>
        /// notifies subscriber that something happened and that may have changed the operational
        /// status of the view
        /// </summary>
        event ViewOperationalStatusChangedHandler ViewOperationalStatusChanged;

        void ClearForm();

        /// <summary>
        /// signals to create as new the item being currently displayed by the view and represented
        /// in Model
        /// </summary>
        bool PerformCreateItem();

        /// <summary>
        /// signals to delete the item being currently displayed by the view and represented in Model
        /// </summary>
        bool PerformDeleteItem();

        /// <summary>
        /// signals to update the item being currently displayed by the view and represented in Model
        /// </summary>
        bool PerformUpdateItem();

        /// <summary>
        /// signals that the view should be prepared to begin the model edition
        /// </summary>
        void PrepareItemEdition();

        /// <summary>
        /// signals that the view should be prepared to begin the model creation
        /// </summary>
        void PrepareNewItemCreation();

        /// <summary>
        /// signals that the view should be re-bound to the model represented in Model
        /// </summary>
        void RefreshViewData();

        /// <summary>
        /// sets the view as "busy"
        /// </summary>
        void SetBusyState();

        void SetModel(object model);

        /// <summary>
        /// sets the view as normal and ready status
        /// </summary>
        void SetNormalState();
    }
}