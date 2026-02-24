namespace Awards.Repository
{
    /// <summary>
    /// Declares operations applicable only to the Brands table.
    /// </summary>
    public interface IBrandRepository : IRepository<Data.Brand>
    {
        /// <summary>
        /// Updates the web home page of the only Brand record with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Brand record to be updated.</param>
        /// <param name="newHP">The new web home page for the chosen Brand record.</param>
        void ChangeHomePage(int id, string newHP);

        /// <summary>
        /// Updates the name of the only Brand record with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Brand record to be updated.</param>
        /// <param name="newName">The new name for the chosen Brand record.</param>
        void ChangeName(int id, string newName);

        /// <summary>
        /// Updates the country and address data of the only Brand record with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Brand record to be updated.</param>
        /// <param name="newCountryID">The ID of the new country >= 1.
        /// No change to the country will be made otherwise.</param>
        /// <param name="newAddress">The new address to replace the current.</param>
        void Move(int id, int newCountryID, string newAddress);
    }
}