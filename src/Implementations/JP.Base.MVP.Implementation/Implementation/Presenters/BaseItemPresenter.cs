using System;
using JP.Base.MVP.Implementation.Contracts.Views.Base;

namespace JP.Base.MVP.Implementation.Presenters.Base
{
    public abstract class BaseItemPresenter<TIPresenter, TIView, TModel> : BaseModelPresenter<TIPresenter, TIView, TModel>
        where TIView : IBaseItemView<TModel, TIView>
        where TIPresenter : IPresenter<TIView>
        where TModel : class
    {
        public BaseItemPresenter(TIView view)
            : base(view)
        {
        }

        protected override TIView currentView
        {
            get
            {
                return base.currentView;
            }
            set
            {
                base.currentView = value;
            }
        }

        public abstract void CreateNewItem();

        public abstract void UpdateItem(bool deleteItem);

        protected override void BindToViewEvents()
        {
            currentView.NotifyPerformUpdateItem += OnViewNotifyPerformUpdateItem;
            currentView.NotifyPerformCreateItem += OnViewNotifyPerformCreateItem;
            base.BindToViewEvents();
        }

        protected override void UnbindViewEvents()
        {
            currentView.NotifyPerformUpdateItem -= OnViewNotifyPerformUpdateItem;
            currentView.NotifyPerformCreateItem -= OnViewNotifyPerformCreateItem;
            base.UnbindViewEvents();
        }

        private void OnViewNotifyPerformCreateItem(object sender, EventArgs e)
        {
            CreateNewItem();
        }

        private void OnViewNotifyPerformUpdateItem(object sender, bool e)
        {
            UpdateItem(e);
        }
    }
}