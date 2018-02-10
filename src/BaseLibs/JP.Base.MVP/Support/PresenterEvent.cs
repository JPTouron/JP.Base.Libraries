namespace JP.Base.MVP
{
    /// <summary>
    /// The PresenterEvent delegate defines a connection/view management related event on a presenter
    /// class. For example, BeforeViewRegister, AfterViewRegister and other events fire that pass
    /// these delegates around to indicate state.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <list>
    /// <listheader>Version History</listheader>
    /// <item>7 October, 2009 - Steve Gray - Initial Draft</item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <typeparam name="TViewContract">View Contract</typeparam>
    /// <typeparam name="TPresenterContract">Presenter contract</typeparam>
    /// <param name="sender">Presenter that is experiencing the event</param>
    /// <param name="view">View that is the subject of the event.</param>
    public delegate void PresenterEvent<TViewContract, TPresenterContract>(TPresenterContract sender, TViewContract view);
}