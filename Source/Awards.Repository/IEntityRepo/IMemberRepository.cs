namespace Awards.Repository
{
    using Awards.Data;

    /// <summary>
    /// Declares operations applicable only to the Members table.
    /// </summary>
    public interface IMemberRepository : IRepository<Member>
    {
        /// <summary>
        /// Updates the name of the only Member record with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Member record to be updated.</param>
        /// <param name="newName">The new name for the chosen Member record.</param>
        void ChangeName(int id, string newName);

        /// <summary>
        /// Updates the country and the office location data of the only Member record with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Member record to be updated.</param>
        /// <param name="newCountryID">The ID of the new country for the chosen Member record.
        /// If this is less than 1, it will be ignored.</param>
        /// <param name="newLocation">The new office location for the chosen Member record.</param>
        void Move(int id, int newCountryID, string newLocation);
    }
}