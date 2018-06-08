//using JP.Base.DAL.Model;
//using JP.Base.DAL.UnitOfWork;
//using JP.Base.ViewModel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using JP.Base.Logic.Search;
//using JP.Base.Logic.Contracts;

//namespace JP.Base.Logic
//{
//    public class myLogic : BaseLogic<BaseModel<int>, BaseViewModel<int>, IBaseUnitOfWork, int>
//    //,ICRUDLogic<BaseViewModel<int>, BaseViewModel<int>, BaseViewModel<int>, BaseViewModel<int>>
//    {
//        public myLogic(IUoWFactory<IBaseUnitOfWork> factory) : base(factory)
//        {
//        }

//        protected override string DefaultSortField
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//        }

//        protected override string EntityName
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//        }


//        public Tresult Execute<Tresult>(Func<IUoWFactory<IBaseUnitOfWork>, Tresult> funcy)
//        {

//            return funcy.Invoke(factory);
//        }

//        public Tresult Execute<Tresult,TParam>(Func<IUoWFactory<IBaseUnitOfWork>, Tresult> funcy, TParam param)
//        {

//            return funcy.Invoke(factory);
//        }

//        public void Execute(Action<IUoWFactory<IBaseUnitOfWork>> action)
//        {

//            action.Invoke(factory);
//        }

//        public void Execute(Action<IUoWFactory<IBaseUnitOfWork>, BaseModel<int>> action, BaseModel<int> param)
//        {

//            action.Invoke(factory, param);
//        }


//        protected override void ExecuteCreateMethod(BaseModel<int> model, IBaseUnitOfWork unitOfWork)
//        {
//            throw new NotImplementedException();
//        }

//        protected override void ExecuteDeleteMethod(BaseModel<int> model, IBaseUnitOfWork unitOfWork)
//        {
//            throw new NotImplementedException();
//        }

//        protected override BaseViewModel<int> ExecuteGetById(int id, IBaseUnitOfWork unitOfWork)
//        {
//            throw new NotImplementedException();
//        }

//        protected override SearchResults<BaseViewModel<int>> ExecuteGetList(SortAndFilterData sortAndFilter, IBaseUnitOfWork unitOfWork)
//        {
//            throw new NotImplementedException();
//        }

//        protected override BaseViewModel<int> ExecuteUpdateMethod(BaseModel<int> model, IBaseUnitOfWork unitOfWork)
//        {
//            throw new NotImplementedException();
//        }

//        protected override void PerformSpecificValidations(BaseViewModel<int> viewModel)
//        {
//            throw new NotImplementedException();
//        }

//        protected override BaseModel<int> ToModel(BaseViewModel<int> viewModel)
//        {
//            throw new NotImplementedException();
//        }

//        protected override IEnumerable<BaseViewModel<int>> ToViewModel(IEnumerable<BaseModel<int>> entities)
//        {
//            throw new NotImplementedException();
//        }

//        protected override BaseViewModel<int> ToViewModel(BaseModel<int> model)
//        {
//            throw new NotImplementedException();
//        }

//        protected override void ValidateId(int id)
//        {
//            throw new NotImplementedException();
//        }

//        protected override void ValidateModel(object model)
//        {
//            throw new NotImplementedException();
//        }



//    }
//    public static class Extensy
//    {


//        public static SearchResults<BaseModel<int>> GetFiltered(this myLogic logic, SearchParams param) {
            
//            Func<IUoWFactory<IBaseUnitOfWork>, SearchResults<BaseModel<int>>> funcy = (fac) =>
//            {

//                SearchEngine<BaseModel<int>, SearchResults<BaseModel<int>>> engine;


//                var result = engine.GetSearchQuery();

//                return result;
                

//                //return new SearchResults<BaseModel<int>>();
//            };

//            return logic.Execute<SearchResults<BaseModel<int>>,SearchParams>(funcy,param);

//        }

//        public static void Deletey(this myLogic aLogic, BaseModel<int> m)
//        {

//            aLogic.Execute((fac) =>
//            {
//                using (var u = fac.CreateUoW())
//                {
//                    u.Execute(() =>
//                    {
//                        // delete  shit
//                    }, true);
//                }

//            });

//        }

//        public static void Insert(this myLogic aLogic, BaseModel<int> m)
//        {



//        }


//        public static void Getty(this myLogic aLogic)
//        {

//        }


//    }
//}
