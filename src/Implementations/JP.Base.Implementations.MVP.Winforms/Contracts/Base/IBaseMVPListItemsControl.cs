using System;
using JP.Base.Logic.Search;
using JP.Base.Implementations.MVP;

namespace JP.Base.Implementations.MVP.Winforms.Contracts.Base
{
    public interface IBaseMVPListItemsControl
    {
        int ItemsCount
        {
            get;
        }

        int TotalItemsFound
        {
            get;
        }

        event EventHandler FinishedLoadingItems;

        event EventHandler ItemHasBeenSelected;

        event EventHandler LoadingItems;

        event EventHandler NoItemsFound;

        /// <summary>
        /// notifies subscriber that something happened and that may have changed the operational
        /// status of the view
        /// </summary>
        event ViewOperationalStatusChangedHandler ViewOperationalStatusChanged;

        void CancelItemsLoad();

        void LoadAllItems(SortAndFilterData searchData = null);
    }
}