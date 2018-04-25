using System;
using JP.Base.MVP.Implementation.Contracts.Views.Base;

namespace JP.Base.MVP.Implementation.Presenters.Base
{
    public abstract class BasePresenter<TIPresenter, TIView> : PresenterBase<TIPresenter, TIView>
        where TIView : IBaseView<TIView>
        where TIPresenter : IPresenter<TIView>
    {
        public BasePresenter(TIView view)
        {
            currentView = view;
            this.BeforeViewConnect += OnBasePresenterBeforeViewConnect;
            BindToViewEvents();
        }

        protected virtual TIView currentView { get; set; }

        protected virtual void BindToViewEvents()
        {
            currentView.NotifyTerminating += OnCurrentViewNotifyTerminating;
        }

        protected virtual void DisplayExceptionOnView(Exception ex, bool shouldDisableView)
        {
            currentView.DisplayErrorMessage($"An error has occurred while processing your request\r\n{ex.Message}", shouldDisableView);
        }

        protected abstract override TIPresenter GetPresenterEndpoint();

        protected void HandleError(Exception ex, bool shouldDisableView = false, bool setNormalState = true)
        {
            if (setNormalState)
                SetViewNormalState();

            LogException(ex);

            DisplayExceptionOnView(ex, shouldDisableView);
        }

        protected abstract void LogException(Exception ex);    

        /// <summary>
        /// dispose of anything here as the view is terminating
        /// </summary>
        protected virtual void OnViewTerminating()
        {
            UnbindViewEvents();
        }

        protected override abstract void RefreshView(TIView viewInstance);

        protected void SetViewBusyState()
        {
            currentView.SetBusyState();
        }

        protected void SetViewNormalState()
        {
            currentView.SetNormalState();
        }

        protected virtual void UnbindViewEvents()
        {
            currentView.NotifyTerminating -= OnCurrentViewNotifyTerminating;
        }

        private void OnBasePresenterBeforeViewConnect(TIPresenter sender, TIView view)
        {
            currentView = view;
        }

        private void OnCurrentViewNotifyTerminating(object sender, EventArgs e)
        {
            OnViewTerminating();
        }
    }
}