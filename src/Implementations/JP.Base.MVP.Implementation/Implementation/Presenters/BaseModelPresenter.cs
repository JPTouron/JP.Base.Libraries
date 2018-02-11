using System;
using JP.Base.MVP.Implementation.Contracts.Views.Base;

namespace JP.Base.MVP.Implementation.Presenters.Base
{
    public abstract class BaseModelPresenter<TIPresenter, TIView, TModel> : BasePresenter<TIPresenter, TIView>
        where TIView : IBaseView<TIView>
        where TIPresenter : IPresenter<TIView>
        where TModel : class
    {
        public BaseModelPresenter(TIView view)
            : base(view)
        {
        }

        public virtual TModel Model
        {
            set
            {
                ((IBaseModelView<TModel>)currentView).Model = value;
            }

            get
            {
                return ((IBaseModelView<TModel>)currentView).Model;
            }
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

    }
}