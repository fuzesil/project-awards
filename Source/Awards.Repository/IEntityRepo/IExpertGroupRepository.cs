namespace Awards.Repository
{
    /// <summary>
    /// Declares operations applicable only to the ExpertGroups table.
    /// </summary>
    public interface IExpertGroupRepository : IRepository<Data.ExpertGroup>
    {
        /// <summary>
        /// Updates the name of the only ExpertGroup record with the given ID.
        /// </summary>
        /// <param name="id">The ID of the ExpertGroup to be updated.</param>
        /// <param name="newName">The new name for the ExpertGroup record.</param>
        void ChangeName(int id, string newName);
    }
}