namespace JP.Base.MVP.Implementation.Winforms.Pagination.EventHandlers
{
    public delegate void NotifyFirstPageRequestedHandler(int page);

    public delegate void NotifyLastPageRequestedHandler(int page);

    public delegate void NotifyNextPageRequestedHandler(int page);

    public delegate void NotifyPageSizeChangedHandler(int pageSize, ref bool cancel);

    public delegate void NotifyPreviousPageRequestedHandler(int page);

    public delegate void NotifySpecificPageRequestedHandler(int page);
}