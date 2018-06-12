using JP.Base.DAL.UnitOfWork;
using JP.Base.Logic.Search;

namespace JP.Base.Logic.Crud
{
    public class SearchParams : BaseSearchParams
    {
        public IBaseUnitOfWork UnitOfWork { get; set; }
    }
}