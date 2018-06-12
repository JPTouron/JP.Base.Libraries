//using JP.Base.DAL.ADO.Repositories;
//using JP.Base.DAL.Model;
//using JP.Base.Logic.Crud;
//using JP.Base.Logic.Search;
//using JP.Base.Implementations.Logic.Search.ADO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Logic.POC.As.Composable
//{
// ** NOTE **
//    // problem with using this approach is that we'll need to have a logic object handy to invoke this things, therefore also haveing knowledge of the Execute<T> methods
//    // and then all purpose for these methods is gone... also we would break sOlid principle

//    public static class Extensy
//    {
//        public static IEnumerable<Operator> GetFiltered(this OperatorLogic logic, SearchParams param)
//        {
//            return logic.Execute<IEnumerable<Operator>, SearchParams>((fac)=> {
//                var s = new SearchEngine(param);

//                var result = s.GetSearchQuery();

//                using (var u = fac.CreateUoW())
//                {
//                    var r = u.GetGenericRepo<Operator>();
//                    return r.Get(result.Filter, result.OrderBy, result.Sort, result.Page, result.PageSize);

//                }

//            }, param);

//        }

//        //public static void Deletey(this myLogic aLogic, BaseModel<int> m)
//        //{
//        //    aLogic.Execute((fac) =>
//        //    {
//        //        using (var u = fac.CreateUoW())
//        //        {
//        //            u.Execute(() =>
//        //            {
//        //                    // delete  shit
//        //                }, true);
//        //        }

//        //    });

//        //}

//        //public static void Insert(this myLogic aLogic, BaseModel<int> m)
//        //{
//        //}

//        //public static void Getty(this myLogic aLogic)
//        //{
//        //}

//    }
//}