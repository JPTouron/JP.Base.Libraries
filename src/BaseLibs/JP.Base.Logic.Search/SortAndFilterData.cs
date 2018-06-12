namespace JP.Base.Logic.Search
{
    public class SortAndFilterData
    {
        private int page = 1;
        private int pageSize = 10;

        public bool GetCount { get; set; }
        public int? Page { get { return page; } set { page = value.HasValue ? value.Value : page; } }
        public int? PageSize { get { return pageSize; } set { pageSize = value.HasValue ? value.Value : pageSize; } }

        public string SearchString { get; set; }
        public string SortField { get; set; }
        public string SortOrder { get; set; }
    }
}