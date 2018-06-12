using JP.Base.Implementations.MVP;
using JP.Base.Implementations.MVP.Contracts.Views.Base;
using JP.Base.Implementations.MVP.Views.Base;
using JP.Base.Logic.Search;
using System.Linq;

namespace ImplementingUserControls
{
    public interface IListView : IBaseListView<Model, IListView>
    {
    }

    public class ListView : BaseView, IListView
    {
        private Backend be;

        public ListView()
        {
            be = new Backend(this);
        }

        public int ItemsCount
        {
            get; private set;
        }

        public Model Model
        {
            get; set;
        }

        public event NotifyLoadItemsHandler<Model> NotifyLoadItems;

        public SearchResults<Model> LoadAllItems(SortAndFilterData searchData = null)
        {
            ItemsCount = 0;

            var evt = NotifyLoadItems;
            if (evt != null)
            {
                var result = evt(searchData);
                ItemsCount = result.Results.Count();
                return result;
            }
            return null;
        }
    }
}