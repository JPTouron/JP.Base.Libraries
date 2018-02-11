using JP.Base.DAL.EF6.UnitOfWork;
using JP.Base.Logic.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JP.Base.Logic.Implementations
{
    public class SearchParams : BaseSearchParams
    {
        public IBaseUnitOfWork UnitOfWork { get; set; }
    }
}
