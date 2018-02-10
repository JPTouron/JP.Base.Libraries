namespace JP.Base.MVP
{
    /// <summary>
    /// The IPresenter interface defines an active presenter in the Model-View-Presenter pattern. The
    /// presenter maintains and controls multiple views, which are introduced to it by an
    /// orchestration mechanism. As each view is registered the presenter will attach to appropriate
    /// events on it and also push changed state into it. Whenever another view of the presenter
    /// raises an event, it is the duty of the preseter to effectively multicast these events to
    /// other listening views.
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
    /// <typeparam name="TViewContract">View contract type</typeparam>
    public interface IPresenter<TViewContract>
        where TViewContract : IView<TViewContract>
    {
        /// <summary>
        /// Connect a view to this presenter.
        /// </summary>
        /// <param name="viewInstance">View instance</param>
        /// <param name="requiresState">Requires initial state to be pushed into view.</param>
        void ConnectView(TViewContract viewInstance, bool requiresState);

        /// <summary>
        /// Disconnect a view from this presenter.
        /// </summary>
        /// <param name="viewInstance">View instance</param>
        void DisconnectView(TViewContract viewInstance);
    }
}