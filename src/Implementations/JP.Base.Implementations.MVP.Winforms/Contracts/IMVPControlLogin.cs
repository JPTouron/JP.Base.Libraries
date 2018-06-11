using System;

namespace JP.Base.Implementations.MVP.Winforms.Contracts
{
    public interface IMVPControlLogin
    {
        event EventHandler Close;

        void ClearForm();

        /// <summary>
        /// sets the view as "busy"
        /// </summary>
        void SetBusyState();

        /// <summary>
        /// sets the view as normal and ready status
        /// </summary>
        void SetNormalState();
    }
}