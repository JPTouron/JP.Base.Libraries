using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace JP.Base.MVP
{
    /// <summary>
    /// The PresenterBase class provides common functionality for managing the views associated with
    /// a presenter type. This allows for quicker construction of basic views without the
    /// implementation of large volumes of boilerplate code.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <list>
    /// <listheader>Version History</listheader>
    /// <item>10 October, 2009 - Steve Gray - Initial Draft</item>
    /// </list>
    /// </para>
    /// <para>
    /// This implementation uses self-constrained generics to allow bi-directional strong coupling of
    /// interface types for both presenter and view, allowing mutual strong references.
    /// </para>
    /// </remarks>
    /// <typeparam name="TPresenterContract">Presenter contract type</typeparam>
    /// <typeparam name="TViewContract">View contract type</typeparam>
    public abstract class PresenterBase<TPresenterContract, TViewContract> :
        IPresenter<TViewContract>,
        IViewManager<TViewContract, TPresenterContract>
        where TViewContract : IView<TViewContract>
        where TPresenterContract : IPresenter<TViewContract>
    {
        private IList<TViewContract> _Views;

        /// <summary>
        /// Initialize the new presenter base class.
        /// </summary>
        public PresenterBase()
        {
            // Set up the initial, empty views list
            _Views = new List<TViewContract>();
        }

        /// <summary>
        /// Views that are currently connected to this presenter.
        /// </summary>
        protected IEnumerable<TViewContract> Views
        {
            get
            {
                return _Views;
            }
        }

        /// <summary>
        /// Event that fires after the view has been registered.
        /// </summary>
        public event PresenterEvent<TViewContract, TPresenterContract> AfterViewConnect;

        /// <summary>
        /// Event that fires after a view has unregistered.
        /// </summary>
        public event PresenterEvent<TViewContract, TPresenterContract> AfterViewDisconnect;

        /// <summary>
        /// Event that fires prior to each view being registered.
        /// </summary>
        public event PresenterEvent<TViewContract, TPresenterContract> BeforeViewConnect;

        /// <summary>
        /// Event that fires prior to a view un-registering itself
        /// </summary>
        public event PresenterEvent<TViewContract, TPresenterContract> BeforeViewDisconnect;

        /// <summary>
        /// Connect a view to this presenter.
        /// </summary>
        /// <param name="viewInstance">View instance</param>
        /// <param name="requiresState">Requires initial state to be pushed into view.</param>
        public void ConnectView(TViewContract viewInstance, bool requiresState)
        {
            // LRM.Casiraghi.Forms invariants
            Debug.Assert(Views != null, "The views collection should always be available.");

            // Validate arguments
            if (viewInstance == null)
                throw new ArgumentNullException("viewInstance");

            lock (Views)
            {
                // If already registered, do nothing
                if (Views.Contains(viewInstance))
                    return;

                // Before the view connects, fire the appropriate events.
                if (BeforeViewConnect != null)
                    BeforeViewConnect(GetPresenterEndpoint(), viewInstance);

                // Add to views collection
                this._Views.Add(viewInstance);

                // Set initial state/refresh
                if (requiresState)
                    RefreshView(viewInstance);

                // After the view connects, fire the appropriate events.
                if (AfterViewConnect != null)
                    AfterViewConnect(GetPresenterEndpoint(), viewInstance);
            }
        }

        /// <summary>
        /// Disconnect a view from this presenter.
        /// </summary>
        /// <param name="viewInstance">View instance</param>
        public void DisconnectView(TViewContract viewInstance)
        {
            // LRM.Casiraghi.Forms invariants
            Debug.Assert(Views != null, "The views collection should always be available.");

            // Validate arguments
            if (viewInstance == null)
                throw new ArgumentNullException("viewInstance");

            lock (Views)
            {
                // If not registered, do nothing
                if (!Views.Contains(viewInstance))
                    return;

                // Before the view disconnects, fire the appropriate events.
                if (BeforeViewDisconnect != null)
                    BeforeViewDisconnect(GetPresenterEndpoint(), viewInstance);

                // Remove from views collection
                this._Views.Remove(viewInstance);

                // After the view disconnects, fire the appropriate events.
                if (AfterViewDisconnect != null)
                    AfterViewDisconnect(GetPresenterEndpoint(), viewInstance);
            }
        }

        /// <summary>
        /// Get the presenter contract endpoint represented by this presenter base.
        /// </summary>
        /// <returns>Presenter contract endpoint.</returns>
        protected abstract TPresenterContract GetPresenterEndpoint();

        /// <summary>
        /// Refresh a view / push initial state values into the view.
        /// </summary>
        /// <param name="viewInstance">View instance to refresh</param>
        protected abstract void RefreshView(TViewContract viewInstance);
    }
}