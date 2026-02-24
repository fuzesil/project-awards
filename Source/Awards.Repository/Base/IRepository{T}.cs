namespace Awards.Repository
{
    using System.Linq;

    /// <summary>
    /// Declares additional basic but (generic) typed operations.
    /// </summary>
    /// <typeparam name="T">The generic type (which must be a class).</typeparam>
    public interface IRepository<T> : IRepository
        where T : class
    {
        /// <summary>
        /// Returns one record with the given ID.
        /// </summary>
        /// <param name="id">The ID of the record to be returned.</param>
        /// <returns>The record which has the given ID.</returns>
        T GetOne(int id);

        /// <summary>
        /// Returns one record with the given name.
        /// </summary>
        /// <param name="name">The name of the record to be returned.</param>
        /// <returns>The record which has the given name.</returns>
        T GetOne(string name);

        /// <summary>
        /// Returns all of the records in a table.
        /// </summary>
        /// <returns>All records in a table.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Adds one record to the table.
        /// </summary>
        /// <param name="entity">The record to be added.</param>
        void Insert(T entity);

        /// <summary>
        /// Deletes one record from the table.
        /// </summary>
        /// <param name="entity">The record to be deleted.</param>
        void Remove(T entity);

        /// <summary>
        /// Updates one record in the table.
        /// </summary>
        /// <param name="entity">The record to be updated.</param>
        void Update(T entity);
    }
}
