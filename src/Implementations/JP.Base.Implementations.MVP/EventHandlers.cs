using JP.Base.Logic.Search;

namespace JP.Base.Implementations.MVP
{
    public delegate void NotifyDisplayConfirmDeletionMessageHandler(string title, string message, ref bool confirmDeletion);

    public delegate void NotifyDisplayErrorMessageHandler(string message, bool shouldDisableView);

    public delegate SearchResults<TModel> NotifyLoadItemsHandler<TModel>(SortAndFilterData searchData) where TModel : class;

    public delegate void NotifySearchRequestHandler(string search);

    public delegate void NotifyUpdatedItemHandler(bool delete);

    public delegate void ViewOperationalStatusChangedHandler(object sender, bool canOperate);
}