namespace JP.Base.Logic.Search
{
    /// <summary>
    /// defines the base data required to perform a search
    /// </summary>
    public class BaseSearchParams
    {
        public BaseSearchParams()
        {
        }

        public string DefaultSortingField { get; set; }
        public SortAndFilterData SortAndFilter { get; set; }
    }
}