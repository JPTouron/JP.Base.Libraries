using System.Collections.Generic;
using System.Linq;

namespace ImplementingUserControls
{
    public class Backend
    {
        private IEnumerable<Model> itemsList;

        public Backend(IListView view)
        {
            view.NotifyLoadItems += View_NotifyLoadItems;

            itemsList = new List<Model> {
                new Model {  id=1 , name="juan" , value="298"},
                new Model {  id=2 , name="pedro" , value="100"},
                new Model {  id=3 , name="coco" , value="8"},
                new Model {  id=4 , name="nico" , value="34234242"},
                new Model {  id=5 , name="emi" , value="644"},
                new Model {  id=6 , name="gera" , value="4342"}
            };
        }

        public IEnumerable<Model> GetModel()
        {
            return itemsList;
        }

        private JP.Base.Logic.Search.SearchResults<Model> View_NotifyLoadItems(JP.Base.Logic.Search.SortAndFilterData searchData)
        {
            var items = GetModel();

            items = items.Where(x => x.name.Contains(searchData.SearchString));

            return new JP.Base.Logic.Search.SearchResults<Model>
            {
                Count = items.Count(),
                Results = items
            };
        }
    }
}