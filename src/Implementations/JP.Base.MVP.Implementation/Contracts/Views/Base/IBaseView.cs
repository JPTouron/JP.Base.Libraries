using System;

namespace JP.Base.MVP.Implementation.Contracts.Views.Base
{
    /// <summary>
    /// interface to base all views
    /// </summary>
    public interface IBaseView<TIView> : IView<TIView>
        where TIView : IView<TIView>
    {
        event NotifyDisplayErrorMessageHandler NotifyUIDisplayErrorMessage;

        event EventHandler NotifyUISetBusyState;

        event EventHandler NotifyUISetNormalState;

        void DisplayErrorMessage(string message, bool shouldDisableView);

        /// <summary>
        /// sets the view as "busy"
        /// </summary>
        void SetBusyState();

        /// <summary>
        /// sets the view as normal and ready status
        /// </summary>
        void SetNormalState();

        void TerminateView();
    }
}