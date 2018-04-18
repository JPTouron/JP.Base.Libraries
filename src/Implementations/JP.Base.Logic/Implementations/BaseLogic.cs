using JP.Base.Common.Extensions;
using JP.Base.DAL.EF6.Model;
using JP.Base.DAL.EF6.UnitOfWork;
using JP.Base.Logic.Contracts;
using JP.Base.Logic.Search;
using JP.Base.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace JP.Base.Logic.Implementations
{
    public enum LogicAction
    {
        [Description("creation")]
        Create,

        [Description("updating")]
        Update,

        [Description("deleting")]
        Delete,

        [Description("reading")]
        Get
    }

    /// <summary>
    /// Represents the base for building Business Logic layer classes.
    /// <para>This class handles Database actions such as CRUD and searching, and should be inherited from to define your specific Base Logic class</para>
    /// </summary>
    /// <typeparam name="TModel">The model that the business logic should handle</typeparam>
    /// <typeparam name="TViewModel">The view model that's related to the UI and relate to the <see cref="TModel"/> object</typeparam>
    public abstract class BaseLogic<TModel, TViewModel>
        where TModel : BaseModel<int>
        where TViewModel : BaseViewModel<int>
    {
        protected LogicAction currentAction;
        protected IUoWFactory factory;
        protected ISearchEngineFactory searchFac;

        public BaseLogic(IUoWFactory factory, ISearchEngineFactory searchFac)
        {
            this.factory = factory;
            this.searchFac = searchFac;
        }

        /// <summary>
        /// defines a generic part of the Database Concurrency exception message
        /// </summary>
        protected virtual string DbConcurrencyErrorMessage
        {
            get { return "updated or deleted"; }
        }

        /// <summary>
        /// Defines the default sorting field for searching purposes, can be null
        /// </summary>
        protected abstract string DefaultSortField { get; }

        /// <summary>
        /// The name of the entity that this logic will handle
        /// </summary>
        protected abstract string EntityName { get; }

        /// <summary>
        /// Executes logic to insert an entity into a db
        /// </summary>
        /// <param name="viewModel">The view model that will be persisted into the db</param>
        /// <returns>the id of the inserted entity</returns>
        protected int Create(TViewModel viewModel)
        {
            currentAction = LogicAction.Create;

            ValidateModel(viewModel);

            PerformSpecificValidations(viewModel);

            var model = ToModel(viewModel);

            ExecuteCrudMethod(() =>
            {
                using (var unitOfWork = factory.CreateUoW())
                {
                    unitOfWork.Execute(() =>
                    {
                        ExecuteCreateMethod(model, unitOfWork);
                    }, true);
                }
            }, viewModel);

            return model.Id;
        }

        /// <summary>
        /// Executes logic to delete an entity into a db
        /// </summary>
        /// <param name="viewModel">the view model that will be deleted</param>
        protected void Delete(TViewModel viewModel)
        {
            currentAction = LogicAction.Delete;

            ValidateModel(viewModel);

            PerformSpecificValidations(viewModel);

            var model = ToModel(viewModel);

            ExecuteCrudMethod(() =>
            {
                using (var unitOfWork = factory.CreateUoW())
                {
                    unitOfWork.Execute(() =>
                    {
                        ExecuteDeleteMethod(model, unitOfWork);
                    }, true);
                }
            }, viewModel);
        }

        /// <summary>
        /// Executes the actual insert into the database
        /// <para>override this to implement your own way db of insertion</para>
        /// </summary>
        /// <param name="model">the <see cref="TViewModel"/> entity already converted into a <see cref="TModel"/> entity to be persisted into db </param>
        /// <param name="unitOfWork">the <seealso cref="IBaseUnitOfWork"/> object that performs the actual query against the database</param>
        protected virtual void ExecuteCreateMethod(TModel model, IBaseUnitOfWork unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();
            repo.Insert(model);
        }

        /// <summary>
        /// executes the method defined within <see cref="Create(TViewModel)"/> / <see cref="Update(TViewModel)"/> / <see cref="Delete(TViewModel)"/>  / <see cref="GetEntityById(int)"/>,
        /// which involves executing one of the <seealso cref="IBaseUnitOfWork"/> Execute(...) methods
        /// <para>Override this to implement your try...catch logic for this method if required</para>
        /// </summary>
        /// <param name="method">the method to invoke</param>
        /// <param name="entity">the view model or entity involved in the execution and that was passed into the calling method</param>
        protected virtual void ExecuteCrudMethod(Action method, object entity)
        {
            method.Invoke();
        }

        /// <summary>
        /// Executes the actual delete into the database
        /// <para>override this to implement your own way db of deletion</para>
        /// </summary>
        /// <param name="model">the <see cref="TViewModel"/> entity already converted into a <see cref="TModel"/> entity to be deleted from db </param>
        /// <param name="unitOfWork">the <seealso cref="IBaseUnitOfWork"/> object that performs the actual query against the database</param>
        protected virtual void ExecuteDeleteMethod(TModel model, IBaseUnitOfWork unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            repo.AttachEntity(model);

            repo.Delete(model);
        }

        /// <summary>
        /// Executes the actual get by id against the db
        /// <para>override this to implement your own way db of geting data</para>
        /// </summary>
        /// <param name="id">the id used to search for the data</param>
        /// <param name="unitOfWork">the <seealso cref="IBaseUnitOfWork"/> object that performs the actual query against the database</param>
        protected virtual TViewModel ExecuteGetById(int id, IBaseUnitOfWork unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            var model = repo.GetById(id);

            return ToViewModel(model);
        }

        /// <summary>
        /// Performs the actual search query onto the database
        /// </summary>
        /// <param name="getCount">indicates whether we get the count of elements returned by the search query.
        /// <para>
        /// NOTE: be aware that setting this parameter as true may incurr into some overhead
        /// </para>
        /// </param>
        /// <param name="unitOfWork">the <seealso  cref="IBaseUnitOfWork"/> that perofms the actual query against the db</param>
        /// <param name="searchQuery">the query that will be executed against the database</param>
        /// <param name="totalCount">when <paramref name="getCount"/> is true then this parameter returns the obtained count for the records that matched searching criteria</param>
        /// <returns></returns>
        protected virtual IEnumerable<TViewModel> ExecuteSearchMethod(bool getCount, IBaseUnitOfWork unitOfWork, IQueryable<TModel> searchQuery, ref int totalCount)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            if (getCount)
                totalCount = repo.Get().Count();

            var models = ToViewModel(searchQuery).ToList();

            return models;
        }

        /// <summary>
        /// Executes the actual update into the database
        /// <para>override this to implement your own way db of updating</para>
        /// </summary>
        /// <param name="model">the <see cref="TViewModel"/> entity already converted into a <see cref="TModel"/> entity to be updated into the db </param>
        /// <param name="unitOfWork">the <seealso cref="IBaseUnitOfWork"/> object that performs the actual query against the database</param>
        protected virtual TViewModel ExecuteUpdateMethod(TModel model, IBaseUnitOfWork unitOfWork)
        {
            var repo = unitOfWork.GetGenericRepo<TModel>();

            repo.AttachEntity(model);

            repo.Update(model);

            return ToViewModel(model);
        }

        /// <summary>
        /// Returns an entity based on its Id
        /// </summary>
        /// <param name="id">the Id that matches the entity</param>
        /// <returns>the view model that represents the actual entity</returns>
        protected TViewModel GetEntityById(int id)
        {
            currentAction = LogicAction.Get;

            ValidateId(id);

            TViewModel viewModel = null;
            ExecuteCrudMethod(() =>
            {
                using (var unitOfWork = factory.CreateUoW())
                {
                    unitOfWork.Execute(() =>
                    {
                        viewModel = ExecuteGetById(id, unitOfWork);
                    }, true);
                }
            }, viewModel);

            return viewModel;
        }

        /// <summary>
        /// Define an application general error message, maybe based on logic action, and return it here
        /// </summary>
        protected virtual string GetGeneralError(LogicAction action)
        {
            return $"An error has occurred while {action.ToName()}.\r\nPlease try again.";
        }

        /// <summary>
        /// Executes a search on the database, based on criteria set on <seealso cref="SortAndFilterData"/> entity returning a list of elements that matches that criteria
        /// </summary>
        /// <param name="sortAndFilter">the entity containing sort and search criteria</param>
        /// <param name="getCount">determines whether we should return the amount of elements returned</param>
        /// <returns>a <seealso cref="SearchResults{T}"/> object containing the view models that matched the criteria and a count property if <paramref name="getCount"/> is true</returns>
        protected SearchResults<TViewModel> GetList(SortAndFilterData sortAndFilter, bool getCount)
        {
            using (var unitOfWork = factory.CreateUoW())
            {
                var param = GetSearchParams(sortAndFilter, unitOfWork);
                var search = GetSearchEngine(param);

                var searchQuery = search.GetSearchQuery();
                var totalCount = 0;

                var res = unitOfWork.Execute(() =>
                {
                    return ExecuteSearchMethod(getCount, unitOfWork, searchQuery, ref totalCount);
                });

                return new SearchResults<TViewModel> { Results = res, Count = totalCount };
            }
        }

        /// <summary>
        /// Returns a <seealso cref="SearchEngine{EntityType}"/> by calling <seealso cref="ISearchEngineFactory.CreateSearchEngine{TModel, TIdentity}(SearchParams)"/>
        /// </summary>
        /// <param name="param">the searching params</param>
        protected virtual SearchEngine<TModel> GetSearchEngine(SearchParams param)
        {
            return searchFac.CreateSearchEngine<TModel, int>(param);
        }

        /// <summary>
        /// Returns the <see cref="SearchParams"/> object that contains basic sort and filtering data
        /// </summary>
        /// <param name="sortAndFilter">determines the sort and filtering criteria for the search</param>
        /// <param name="unitOfWork">the unit of work required to perform the search</param>
        /// <returns></returns>
        protected virtual SearchParams GetSearchParams(SortAndFilterData sortAndFilter, IBaseUnitOfWork unitOfWork)
        {
            var param = new SearchParams
            {
                UnitOfWork = unitOfWork,
                DefaultSortingField = DefaultSortField,
                SortAndFilter = sortAndFilter
            };

            return param;
        }

        /// <summary>
        /// when overridden allows to perform specific validations on the viewModel that arrived on any of the CRUD methods
        /// </summary>
        /// <param name="viewModel">the viewmodel to be validated</param>
        protected abstract void PerformSpecificValidations(TViewModel viewModel);

        /// <summary>
        /// when overridden transforms the <see cref="TViewModel"/> viewmodel into a type of <see cref="TModel"/> by way of a mapping logic
        /// </summary>
        /// <param name="viewModel">the viewmodel to be transformed</param>
        protected abstract TModel ToModel(TViewModel viewModel);

        /// <summary>
        /// when overridden transforms the <see cref="TModel"/> model into a type of <see cref="TViewModel"/> by way of a mapping logic
        /// </summary>
        /// <param name="model">the model to be transformed</param>
        protected abstract TViewModel ToViewModel(TModel model);

        /// <summary>
        /// when overridden transforms the <see cref="IQueryable{TModel}"/> model into a type of <see cref="IEnumerable{TViewModel}"/> by way of a mapping logic
        /// </summary>
        /// <param name="query">the query to be executed</param>
        protected abstract IEnumerable<TViewModel> ToViewModel(IQueryable<TModel> query);

        /// <summary>
        /// Executes logic to Update an entity into a db
        /// </summary>
        /// <param name="viewModel">the view model taht will be updated</param>
        /// <returns></returns>
        protected TViewModel Update(TViewModel viewModel)
        {
            currentAction = LogicAction.Update;

            ValidateModel(viewModel);
            ValidateId(viewModel.Id);

            PerformSpecificValidations(viewModel);

            var model = ToModel(viewModel);
            ExecuteCrudMethod(() =>
            {
                using (var unitOfWork = factory.CreateUoW())
                {
                    unitOfWork.Execute(() =>
                    {
                        viewModel = ExecuteUpdateMethod(model, unitOfWork);
                    }, true);
                }
            }, viewModel);

            return viewModel;
        }

        /// <summary>
        /// Validates the Id specified by the caller
        /// </summary>
        protected abstract void ValidateId(int id);

        /// <summary>
        /// Validates the model or viewmodel specified by the caller
        /// </summary>
        protected abstract void ValidateModel(object model);
    }
}