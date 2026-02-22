namespace Awards.Repository
{
    /// <summary>
    /// Declares operations applicable only to the Country table.
    /// </summary>
    public interface ICountryRepository : IRepository<Data.Country>
    {
        /// <summary>
        /// Updates the PPP/C column for the only Country record with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Country record to be updated.</param>
        /// <param name="newPPP">The new value of the PPP/C column for the chosen Country record.</param>
        void ChangePPP(int id, int newPPP);

        /// <summary>
        /// Updates the name of the only Country record with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Country record to be updated.</param>
        /// <param name="newName">The new name for the chosen Country record.</param>
        void ChangeName(int id, string newName);

        /// <summary>
        /// Updates the capital city of the only Country record with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Country record to be updated.</param>
        /// <param name="newCapitalCity">The new capital city for the chosen Country record.</param>
        void ChangeCapitalCity(int id, string newCapitalCity);

        /// <summary>
        /// Returns the Average value of the PPP/C column of the Country table.
        /// </summary>
        /// <returns>The avergae of the PPP/C column from Countries.</returns>
        double GetAveragePPP();
    }
}