namespace JP.Base.MVP
{
    /// <summary>
    /// The IViewManager interface helps support the Model-View-Presenter pattern by isolating the
    /// view-coordination management aspects of the presenter types.
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
    /// <typeparam name="TPresenterContract">Presenter contract type</typeparam>
    public interface IViewManager<TViewContract, TPresenterContract>
        where TViewContract : IView<TViewContract>
        where TPresenterContract : IPresenter<TViewContract>
    {
        /// <summary>
        /// Event that fires after the view has been registered.
        /// </summary>
        event PresenterEvent<TViewContract, TPresenterContract> AfterViewConnect;

        /// <summary>
        /// Event that fires after a view has unregistered.
        /// </summary>
        event PresenterEvent<TViewContract, TPresenterContract> AfterViewDisconnect;

        /// <summary>
        /// Event that fires prior to each view being registered.
        /// </summary>
        event PresenterEvent<TViewContract, TPresenterContract> BeforeViewConnect;

        /// <summary>
        /// Event that fires prior to a view un-registering itself
        /// </summary>
        event PresenterEvent<TViewContract, TPresenterContract> BeforeViewDisconnect;
    }
}