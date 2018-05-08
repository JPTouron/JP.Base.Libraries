using JP.Base.DAL.Model;
using JP.Base.DAL.UnitOfWork;
using System.Linq;

namespace JP.Base.Logic.Implementations
{
    public class SearchParameters<TModel,TIdentity,TIUnitOfWork> where TModel : BaseModel<TIdentity>
        where TIUnitOfWork:IBaseUnitOfWork
    {
        public bool getCount { get; set; }
        public IQueryable<TModel> searchQuery { get; set; }
        public int totalCount { get; set; }
        public TIUnitOfWork unitOfWork { get; set; }
    }
}