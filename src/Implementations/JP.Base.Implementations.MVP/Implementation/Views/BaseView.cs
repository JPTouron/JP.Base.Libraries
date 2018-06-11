using System;
using JP.Base.Implementations.MVP.Contracts.Views.Base;

namespace JP.Base.Implementations.MVP.Views.Base
{
    /// <summary>
    /// base view to group basic view functionality implementations
    /// </summary>
    public class BaseView : IBaseView<BaseView>
    {
        protected bool canOperate;

        public BaseView()
        {
            canOperate = true;
        }

        public event EventHandler NotifyTerminating;

        public event NotifyDisplayErrorMessageHandler NotifyUIDisplayErrorMessage;

        public event EventHandler NotifyUISetBusyState;

        public event EventHandler NotifyUISetNormalState;

        public void DisplayErrorMessage(string message, bool shouldDisableView)
        {
            DoNotifyDisplayErrorMessage(message, shouldDisableView);
        }

        /// <summary>
        /// sets the view as "busy"
        /// </summary>
        public void SetBusyState()
        {
            var evt = NotifyUISetBusyState;
            if (evt != null)
            {
                evt(this, null);
            }
        }

        /// <summary>
        /// sets the view as normal and ready status
        /// </summary>
        public void SetNormalState()
        {
            var evt = NotifyUISetNormalState;
            if (evt != null)
            {
                evt(this, null);
            }
        }

        /// <summary>
        /// this method gets called when the view is being closed/terminated because the form containing it is closing
        /// <para>it fires event <see cref="NotifyTerminating"/></para>
        /// <para>override this method if you need to add some extra logic on that moment but make sure to invoke <see cref="NotifyTerminating"/></para>
        /// </summary>
        public virtual void TerminateView()
        {
            var notify = NotifyTerminating;
            if (notify != null)
                notify(this, null);
        }

        protected virtual void BaseShowErrorMessage(string message, bool shouldDisableView)
        {
            var evt = NotifyUIDisplayErrorMessage;
            if (evt != null)
                evt(message, shouldDisableView);
        }

        protected void DoNotifyDisplayErrorMessage(string message, bool shouldDisableView)
        {
            BaseShowErrorMessage(message, shouldDisableView);
        }
    }
}