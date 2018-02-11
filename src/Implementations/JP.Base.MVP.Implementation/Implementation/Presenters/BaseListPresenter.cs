using System;
using JP.Base.Logic.Search;
using JP.Base.MVP.Implementation.Contracts.Views.Base;

namespace JP.Base.MVP.Implementation.Presenters.Base
{
    public abstract class BaseListPresenter<TIPresenter, TIView, TIViewModel> : BasePresenter<TIPresenter, TIView>
        where TIView : IBaseView<TIView>
        where TIPresenter : IPresenter<TIView>
        where TIViewModel : class
    {
        public BaseListPresenter(TIView view)
            : base(view)
        {
        }

        protected virtual SearchResults<TIViewModel> GetAllItems(SortAndFilterData data)
        {
            SearchResults<TIViewModel> elements = null;

            try
            {
                SetViewBusyState();

                elements = GetElementsList(data);

                SetViewNormalState();
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }

            return elements;
        }

        protected abstract SearchResults<TIViewModel> GetElementsList(SortAndFilterData data);

        protected SearchResults<TIViewModel> OnViewNotifyLoadItems(SortAndFilterData searchData)
        {
            return GetAllItems(searchData);
        }
    }
}