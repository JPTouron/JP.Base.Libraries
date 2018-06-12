using JP.Base.Implementations.MVP.Contracts.Views.Base;
using JP.Base.Implementations.MVP.Support.Factories;
using JP.Base.Implementations.MVP.Winforms.Contracts.Base;
using JP.Base.Implementations.MVP.Winforms.Implementation.Messages;
using JP.Base.Implementations.MVP.Winforms.Implementation.Searching;
using JP.Base.Implementations.MVP.Winforms.Pagination;
using JP.Base.Logic.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace JP.Base.Implementations.MVP.Winforms.Implementation
{
    /// <summary>
    /// <para>this class enables the creation of a simple user control which already has sufficient logic implemented
    /// to support a view within an mvp architecture</para>
    /// <para>this control is designed to handle a model list type and be backed by a view class that would repreent that part on an mvp
    /// architecture model</para>
    /// <para>in order to enable visual control edition based on forms designer, create a user control
    /// and make it inherit from this class (we call this a slice control), then create another user control the actual one you are going to use, and
    /// make it inherit from the slice control</para>
    /// </summary>
    /// <typeparam name="TModel">represents the type of the model this control will handle</typeparam>
    /// <typeparam name="TIView">represents the type of the view that will handle this user control</typeparam>
    public partial class BaseListControl<TModel, TIView> : UserControl, IBaseMVPListItemsControl
        where TIView : IBaseListView<TModel, TIView>
        where TModel : class
    {
        private Paginator paginator;
        private Form parentForm;
        private Search searchControl;
        //private string j;

        public BaseListControl()
        {
            InitializeComponent();

            DefaultPageSize = CurrentPageSize = 10;
            DefaultSortDirection = CurrentSortDirection = "asc";
            CurrentPage = 1;

            if (ViewProvider.ViewFactory != null)
                CreateAndBindView();

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            ListControl = null;

            //j = Guid.NewGuid().ToString().Substring(0, 5);
            //Debug.WriteLine(">> CREATED BaseListControl<"+ typeof(TModel).ToString() +"> instance: " + j.ToString());
        }

        public int CurrentPage { get; set; }

        public int CurrentPageSize { get; set; }

        public int ItemsCount
        {
            get;
            protected set;
        }

        /// <summary>
        /// determines which is the list control where the items will be displayed into
        /// </summary>
        public Control ListControl { get; set; }

        public object SelectedItem
        {
            get { return View.Model; }
        }

        public int TotalItemsFound { get; protected set; }

        protected BindingSource BindingSource { get; set; }

        protected string CurrentSortDirection { get; set; }

        protected string CurrentSortField { get; set; }

        protected int DefaultPageSize { get; set; }

        protected string DefaultSortDirection { get; set; }

        protected Paginator PaginatorControl
        {
            get { return paginator; }
            set
            {
                if (paginator == null)
                {
                    paginator = value;
                    BindPaginationEvents();
                }
            }
        }

        protected Search SearchControl
        {
            get
            {
                return searchControl;
            }
            set
            {
                if (searchControl == null)
                {
                    searchControl = value;
                    BindSearchEvents();
                }
            }
        }

        protected IBaseListView<TModel, TIView> View { get; set; }

        public event EventHandler FinishedLoadingItems;

        public event EventHandler ItemHasBeenSelected;

        public event EventHandler LoadingItems;

        public event EventHandler NoItemsFound;

        public event ViewOperationalStatusChangedHandler ViewOperationalStatusChanged;

        public void CancelItemsLoad()
        {
            if (worker.IsBusy)
                worker.CancelAsync();
        }

        public void LoadAllItems(SortAndFilterData searchData = null)
        {
            if (!worker.IsBusy)
            {
                var loading = LoadingItems;
                if (loading != null)
                    loading(this, null);

                if (ListControl != null)
                    ListControl.Enabled = false;

                if (paginator != null)
                    paginator.Enabled = false;

                searchData = PrepareSearchData(searchData);

                worker.RunWorkerAsync(searchData);
            }
        }

        protected void BindPaginationEvents()
        {
            PaginatorControl.NotifySpecificPageRequested += OnPaginatorCurrentPageChanged;
            PaginatorControl.NotifyFirstPageRequested += OnPaginatorFirstPage;
            PaginatorControl.NotifyLastPageRequested += OnPaginatorLastPage;
            PaginatorControl.NotifyNextPageRequested += OnPaginatorNextPage;
            PaginatorControl.NotifyPreviousPageRequested += OnPaginatorPreviousPage;
            PaginatorControl.NotifyPageSizeChanged += OnPaginatorPageSizeChanged;
        }

        protected void BindSearchEvents()
        {
            SearchControl.NotifySearchRequest += OnSearchControlNotifySearchRequest;
        }

        protected void OnGridColumnClick(string columnName)
        {
            if (!worker.IsBusy)
            {
                if (columnName == CurrentSortField)
                    if (CurrentSortDirection == "desc")
                        CurrentSortDirection = "asc";
                    else
                        CurrentSortDirection = "desc";

                CurrentSortField = columnName;
                var searchData = new SortAndFilterData { SortField = columnName, SortOrder = CurrentSortDirection, Page = CurrentPage, PageSize = CurrentPageSize };
                LoadAllItems(searchData);
            }
        }

        /// <summary>
        /// gets called whenever the background thread that loads the items has something to report, by default this does nothing
        /// </summary>
        /// <param name="e">the object with percentage and user property values</param>
        protected virtual void OnLoadingProgressChanged(ProgressChangedEventArgs e)
        {
            // let's do nothing
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (parentForm != null)
            {
                parentForm.Closing -= OnParentFormClosing;
            }
            parentForm = FindForm();

            if (parentForm != null)
                parentForm.Closing += OnParentFormClosing;
        }

        protected virtual void OnParentFormClosing(object sender, CancelEventArgs e)
        {
            parentForm.Closing -= OnParentFormClosing;
            parentForm = null;

            UnBindView();
            View.TerminateView();
        }

        protected virtual void OnViewNotifyDisplayErrorMessage(string message, bool shouldDisableView)
        {
            var renderer = RendererProvider.RendererFactory.CreateRenderer();
            this.Enabled = !shouldDisableView;

            if (ViewOperationalStatusChanged != null)
                ViewOperationalStatusChanged(this, !shouldDisableView);

            renderer.ShowErrorMessage(message);
        }

        /// <summary>
        /// perform actual loading of items into the <see cref="View"/>, using a background thread, by calling the <see cref="IBaseListView.LoadAllItems(SortAndFilterData)"/> method
        /// <para>if <see cref="SortAndFilterData"/> contains empty data then it also blanks <see cref="CurrentSortDirection"/> and <see cref="CurrentSortField"/> properties</para>
        /// <para>NOTE: be careful when overriding this method as it is being triggered by a background thread and so the UI cannot be updated from such a thread.
        /// If you need to perform such an update then try this code <code>_syncContext.Post((object t) =>{method.Invoke();}, null);</code>
        /// where method is an <seealso cref="System.Action{T}"/>  or delegate to be invoked
        /// </para>
        /// </summary>
        /// <param name="e">a <see cref="DoWorkEventArgs"/> type parameter that will contain information about the search data and on-going process </param>
        protected virtual void PerformBackThreadItemLoading(DoWorkEventArgs e)
        {
            if (WasWorkerCancelationRequested(e))
                return;

            var searchData = (SortAndFilterData)e.Argument;

            var missingSorting = string.IsNullOrEmpty(searchData.SortOrder) && string.IsNullOrEmpty(searchData.SortField);

            if (WasWorkerCancelationRequested(e))
                return;

            var searchResults = View.LoadAllItems(searchData);

            if (WasWorkerCancelationRequested(e))
                return;

            if (missingSorting)
            {
                CurrentSortField = searchData.SortField;
                CurrentSortDirection = searchData.SortOrder;
            }

            e.Result = searchResults;
        }

        /// <summary>
        /// updates or creates a <see cref="SortAndFilterData"/> object that contains the filtering and searching patterns
        /// for the items being loaded into the grid
        /// <para>override to alter sort and filter behaviour</para>
        /// </summary>
        /// <param name="searchData">the previously existing filter that may be passed into this method (default is null)</param>
        /// <returns>the <see cref="SortAndFilterData"/> object</returns>
        protected virtual SortAndFilterData PrepareSearchData(SortAndFilterData searchData = null)
        {
            if (searchData == null)
                searchData = new SortAndFilterData
                {
                    Page = CurrentPage,
                    PageSize = CurrentPageSize,
                    SortOrder = CurrentSortDirection,
                    SortField = CurrentSortField
                };

            var filter = searchData.SearchString;

            filter = string.IsNullOrEmpty(filter) && SearchControl != null && !string.IsNullOrEmpty(SearchControl.InputText) ? SearchControl.InputText : filter;
            searchData.SearchString = filter;

            BindingSource.DataSource = null;

            return searchData;
        }

        protected void RaiseItemHasBeenSelectedEvent()
        {
            var evt = ItemHasBeenSelected;
            if (evt != null)
                evt(this, null);
        }

        protected void RaiseNoItemsFoundEvent()
        {
            var evt = NoItemsFound;
            if (evt != null)
                NoItemsFound(this, null);
        }

        /// <summary>
        /// sets the value to <see cref="IBaseModelView{TModel}.Model"/> represented on the <see cref="View"/> property based on the
        /// index number of the parameter
        /// <para>then it also invokes <see cref="RaiseItemHasBeenSelectedEvent"/> </para>
        /// <para>this method can be overriden</para>
        /// </summary>
        /// <param name="rowIndex">the index of the selected row, zero by default</param>
        protected virtual void SetSelectedItem(int rowIndex = 0)
        {
            if (rowIndex < 0)
                rowIndex = 0;

            var items = ((IEnumerable<TModel>)BindingSource.DataSource);

            if (items != null)
                View.Model = items.ElementAt(rowIndex);

            RaiseItemHasBeenSelectedEvent();
        }

        private void CreateAndBindView()
        {
            View = ViewProvider.ViewFactory.CreateListView<TModel, TIView>();
            View.NotifyUIDisplayErrorMessage += OnViewNotifyDisplayErrorMessage;
        }

        private void OnPaginatorCurrentPageChanged(int page)
        {
            if (!worker.IsBusy)
            {
                CurrentPage = page;
                LoadAllItems();
            }
        }

        private void OnPaginatorFirstPage(int page)
        {
            if (!worker.IsBusy)
            {
                CurrentPage = page;
                LoadAllItems();
            }
        }

        private void OnPaginatorLastPage(int page)
        {
            if (!worker.IsBusy)
            {
                CurrentPage = page;
                LoadAllItems();
            }
        }

        private void OnPaginatorNextPage(int page)
        {
            if (!worker.IsBusy)
            {
                CurrentPage = page;
                LoadAllItems();
            }
        }

        private void OnPaginatorPageSizeChanged(int pageSize, ref bool cancel)
        {
            cancel = worker.IsBusy;

            if (!worker.IsBusy)
            {
                CurrentPage = 1;
                CurrentPageSize = pageSize;
                LoadAllItems();
            }
        }

        private void OnPaginatorPreviousPage(int page)
        {
            if (!worker.IsBusy)
            {
                CurrentPage = page;
                LoadAllItems();
            }
        }

        private void OnSearchControlNotifySearchRequest(string search)
        {
            int searchInt;
            bool isNumeric = int.TryParse(search, out searchInt);

            if (!worker.IsBusy && (search.Length >= 3 || search.Length == 0 || isNumeric))
            {
                CurrentPage = 1;
                var searchData = new SortAndFilterData
                {
                    SearchString = search,
                    SortOrder = CurrentSortDirection,
                    Page = CurrentPage,
                    PageSize = CurrentPageSize,
                    SortField = CurrentSortField
                };
                LoadAllItems(searchData);
            }
        }

        private void OnWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            PerformBackThreadItemLoading(e);
        }

        private void OnWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnLoadingProgressChanged(e);
        }

        private void OnWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ListControl != null)
                ListControl.Enabled = true;

            if (paginator != null)
                paginator.Enabled = true;

            if (!e.Cancelled && e.Error == null)
            {
                var searchResults = (SearchResults<TModel>)e.Result;

                IEnumerable<TModel> elements = searchResults.Results;

                BindingSource.DataSource = searchResults.Results;

                ItemsCount = View.ItemsCount;
                TotalItemsFound = searchResults.Count;

                if (ItemsCount == 0)
                    RaiseNoItemsFoundEvent();
                else
                    SetSelectedItem();

                var finishedLoading = FinishedLoadingItems;
                if (finishedLoading != null)
                    finishedLoading(this, null);

                RefreshPagination();
            }
        }

        private void RefreshPagination()
        {
            if (paginator != null)
            {
                paginator.Paginate(CurrentPage, TotalItemsFound);
                paginator.Enabled = TotalItemsFound > 0;
            }
        }

        private void UnBindView()
        {
            View.NotifyUIDisplayErrorMessage -= OnViewNotifyDisplayErrorMessage;
        }

        private bool WasWorkerCancelationRequested(DoWorkEventArgs e)
        {
            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }

            return e.Cancel;
        }
    }
}