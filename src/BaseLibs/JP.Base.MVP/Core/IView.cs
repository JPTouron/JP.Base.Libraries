using System;

namespace JP.Base.MVP
{
    /// <summary>
    /// The IView interface defines a passive view in the Model-View-Presenter pattern. The view
    /// registers itself with a presenter (as controlled by an orchestration mechanism) and requests
    /// it's state. The view should expose events that are consumed by the presenter for observation
    /// and change tracking in response to interactions.
    /// </summary>
    /// <remarks>
    /// <para>
    /// removed the strong coupling between the view and the presenters now the view does not know
    /// anything about the presenter
    /// </para>
    /// </remarks>
    /// <typeparam name="TViewContract">View contract type</typeparam>
    public interface IView<TViewContract>
        where TViewContract : IView<TViewContract>
    {
        /// <summary>
        /// thie event signals the presenter that the view is ready to be disposed of fire this event
        /// on clos, exit or whatever disposing/terminating methods you define
        /// </summary>
        event EventHandler NotifyTerminating;
    }
}