using System.Collections.Generic;

namespace JP.Base.Logic.Search
{
    public class SearchResults<T> where T : class
    {
        public int Count { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}