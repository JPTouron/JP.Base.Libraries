using System.ComponentModel;

namespace JP.Base.Logic.Search.ADO
{
    public class SearchEngineResults
    {
        public string Filter { get; set; } = null;

        public ListSortDirection Sort { get; set; } = ListSortDirection.Ascending;
        public string OrderBy { get; set; } = null;
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 0;
    }
}