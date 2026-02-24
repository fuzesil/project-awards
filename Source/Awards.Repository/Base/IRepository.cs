namespace Awards.Repository
{
    /// <summary>
    /// Declares basic Count and Delete operations to be implemented.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Deletes the record with the given ID.
        /// </summary>
        /// <param name="id">The ID of the record to be deleted.</param>
        void Remove(int id);
    }
}
