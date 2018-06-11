using JP.Base.Logic.Search;
using JP.Base.MVP;

namespace JP.Base.Implementations.MVP.Contracts.Views.Base
{
    /// <summary>
    /// interface to base all views that lists items
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IBaseListView<TModel, TIView> : IBaseModelView<TModel>, IBaseView<TIView>
        where TModel : class
        where TIView : IView<TIView>
    {
        int ItemsCount { get; }

        event NotifyLoadItemsHandler<TModel> NotifyLoadItems;

        /// <summary>
        /// signals that the view should load and display all the available items that match criteria
        /// if no parameter is set then all items are returned
        /// </summary>
        /// <param name="filter">the value that will be searched for</param>
        SearchResults<TModel> LoadAllItems(SortAndFilterData searchData = null);
    }
}