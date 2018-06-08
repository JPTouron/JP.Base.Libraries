using JP.Base.DAL.ADO.Repositories;
using JP.Base.DAL.Model;
using JP.Base.Logic.Crud;
using JP.Base.Logic.Search;
using JP.Base.Logic.Search.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.POC.As.Composable
{
    public static class Extensy
    {


        public static IEnumerable<Model> GetFiltered(this myLogic logic, SearchParams param)
        {


            return logic.Execute<IEnumerable<Model>, SearchParams>((fac)=> {



                var s = new SearchEngine(param);

                var result = s.GetSearchQuery();

                using (var u = fac.CreateUoW())
                {

                    var r = u.GetGenericRepo<Model>();
                    return r.Get(result.Filter, result.OrderBy, result.Sort, result.Page, result.PageSize);

                }


            }, param);

        }

        //public static void Deletey(this myLogic aLogic, BaseModel<int> m)
        //{

        //    aLogic.Execute((fac) =>
        //    {
        //        using (var u = fac.CreateUoW())
        //        {
        //            u.Execute(() =>
        //            {
        //                    // delete  shit
        //                }, true);
        //        }

        //    });

        //}

        //public static void Insert(this myLogic aLogic, BaseModel<int> m)
        //{



        //}


        //public static void Getty(this myLogic aLogic)
        //{

        //}


    }
}
