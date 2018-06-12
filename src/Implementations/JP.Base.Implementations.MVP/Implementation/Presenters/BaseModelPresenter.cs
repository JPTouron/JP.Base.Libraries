using System;
using JP.Base.Implementations.MVP.Contracts.Views.Base;
using JP.Base.MVP;

namespace JP.Base.Implementations.MVP.Presenters.Base
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