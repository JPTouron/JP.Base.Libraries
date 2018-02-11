using System;
using System.Windows.Forms;
using JP.Base.MVP.Implementation.Winforms.Enums;
using JP.Base.MVP.Implementation.Winforms.Pagination.EventHandlers;

namespace JP.Base.MVP.Implementation.Winforms.Pagination
{
    public partial class Paginator : UserControl
    {
        private PageSizes pageSize;

        public Paginator()
        {
            InitializeComponent();

            cmbPageSizes.Items.Add((int)PageSizes.Five);
            cmbPageSizes.Items.Add((int)PageSizes.Ten);
            cmbPageSizes.Items.Add((int)PageSizes.Thirty);
            cmbPageSizes.Items.Add((int)PageSizes.Fifty);
            cmbPageSizes.Items.Add((int)PageSizes.Hundred);

            cmbPageSizes.SelectedIndex = 2;

            CurrentPage = 1;
            TotalPages = 0;
        }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public event NotifyFirstPageRequestedHandler NotifyFirstPageRequested;

        public event NotifyLastPageRequestedHandler NotifyLastPageRequested;

        public event NotifyNextPageRequestedHandler NotifyNextPageRequested;

        public event NotifyPageSizeChangedHandler NotifyPageSizeChanged;

        public event NotifyPreviousPageRequestedHandler NotifyPreviousPageRequested;

        public event NotifySpecificPageRequestedHandler NotifySpecificPageRequested;

        public PageSizes GetPageSize()
        {
            return (PageSizes)cmbPageSizes.SelectedItem;
        }

        public void Paginate(int currentPage, int totalItems)
        {
            var pageSize = GetPageSize();
            var pages = (double)((decimal)totalItems / Convert.ToDecimal(pageSize));
            int pageCount = (int)Math.Ceiling(pages);

            TotalPages = pageCount;//get all the pages!!!
            CurrentPage = currentPage;

            lblPages.Text = string.Format("{0} / {1}", currentPage, TotalPages);
        }

        public void SetPageSize(PageSizes pageSize)
        {
            var idx = 0;
            switch (pageSize)
            {
                case PageSizes.Five:
                    idx = 0;
                    break;

                case PageSizes.Ten:
                    idx = 1;
                    break;

                case PageSizes.Thirty:
                    idx = 2;
                    break;

                case PageSizes.Fifty:
                    idx = 3;
                    break;

                case PageSizes.Hundred:
                    idx = 4;
                    break;

                default:
                    idx = 1;
                    break;
            }
            cmbPageSizes.SelectedIndex = idx;
        }

        private void btnFirst_Click(object sender, System.EventArgs e)
        {
            goFirst();
        }

        private void btnLast_Click(object sender, System.EventArgs e)
        {
            goLast();
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            goNext();
        }

        private void btnPrevious_Click(object sender, System.EventArgs e)
        {
            goPrevious();
        }

        private void cmbPageSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var oldPageSize = pageSize;
            pageSize = GetPageSize();

            var evt = NotifyPageSizeChanged;

            bool cancel = false;
            if (evt != null)
                evt((int)GetPageSize(), ref cancel);

            if (cancel)
                pageSize = oldPageSize;
        }

        private void goFirst()
        {
            var newPage = 1;
            var evt = NotifyFirstPageRequested;
            if (evt != null)
                evt(newPage);
        }

        private void goLast()
        {
            var newPage = TotalPages;
            var evt = NotifyLastPageRequested;
            if (evt != null)
                evt(newPage);
        }

        private void goNext()
        {
            var newPage = CurrentPage == TotalPages || TotalPages == 1 ? TotalPages : CurrentPage + 1;
            var evt = NotifyNextPageRequested;
            if (evt != null)
                evt(newPage);
        }

        private void goPrevious()
        {
            var newPage = CurrentPage == 1 || TotalPages == 1 ? CurrentPage : CurrentPage - 1;
            var evt = NotifyPreviousPageRequested;
            if (evt != null)
                evt(newPage);
        }
    }
}