using JP.Base.Logic.Search;

namespace JP.Base.Logic.Contracts
{
    /// <summary>
    /// defines a generic interface for all the CRUD operations logic
    /// </summary>
    public interface ICRUDLogic<CreateType, EditType, DeleteType, ListType>
        where CreateType : class
        where EditType : class
        where DeleteType : class
        where ListType : class
    {
        /// <summary>
        /// returns the model id created
        /// </summary>
        int Create(CreateType model);

        /// <summary>
        /// Deletes the model
        /// </summary>
        void DeleteConfirmed(DeleteType model);

        /// <summary>
        /// Updates the model supplied in the parameter
        /// </summary>
        EditType Edit(EditType model);

        /// <summary>
        /// Gets a model based on an Id
        /// </summary>
        EditType GetById(int id);

        /// <summary>
        /// Gets a list of entities, the list can be filtered or sorted
        /// </summary>
        SearchResults<ListType> GetList(SortAndFilterData sortAndFilter, bool getCount = true);
    }
}