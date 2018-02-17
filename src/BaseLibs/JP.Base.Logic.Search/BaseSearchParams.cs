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

        /// <summary>
        /// the default property name of the model that will be used for sorting, this is required
        /// </summary>
        public string DefaultSortingField { get; set; }


        public SortAndFilterData SortAndFilter { get; set; }
    }
}