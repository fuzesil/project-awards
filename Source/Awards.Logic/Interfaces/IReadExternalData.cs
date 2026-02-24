namespace Awards.Logic
{
    /// <summary>
    /// Declares the 'Read All' and 'Read One' operations on the <see cref="Data.Brand"/> and <see cref="Data.Product"/> tables.
    /// </summary>
    public interface IReadExternalData
    {
        /// <summary>
        /// Returns the list of all records in the table.
        /// </summary>
        /// <param name="count">Number of records in the table.</param>
        /// <returns>List of all records in the table.</returns>
        System.Collections.Generic.IEnumerable<Data.Brand> ListAllBrands(out int count);

        /// <summary>Returns the one <see cref="Data.Brand"/> record from the table with the given ID or Name.
        /// <list type="bullet">
        /// <item>
        /// <term>By ID</term>
        /// <description> If <paramref name="id"/> is greater than 0, then the record with that ID is returned.</description>
        /// </item>
        /// <item>
        /// <term>By Name</term>
        /// <description> If <paramref name="id"/> is NOT greater than 0 -AND- <paramref name="name"/> is given, then the record having the input parametre text in the Name of the record is returned.</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="id">ID of the record to retrieve.</param>
        /// <param name="name">Name of the record to retrieve.</param>
        /// <returns>One record matching by <see cref="int"/> ID or <see cref="string"/> Name.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when no record could be returned using either parametre.</exception>
        Data.Brand GetOneBrand(int id, string name = "");

        /// <summary>
        /// Returns the list of all records in the table.
        /// </summary>
        /// <param name="count">Number of records in the table.</param>
        /// <returns>List of all records in the table.</returns>
        System.Collections.Generic.IEnumerable<Data.Product> ListAllProducts(out int count);

        /// <summary>Returns the one <see cref="Data.Product"/> record from the table with the given ID or Name.
        /// <list type="bullet">
        /// <item>
        /// <term>By ID</term>
        /// <description> If <paramref name="id"/> is greater than 0, then the record with that ID is returned.</description>
        /// </item>
        /// <item>
        /// <term>By Name</term>
        /// <description> If <paramref name="id"/> is NOT greater than 0 -AND- <paramref name="name"/> is given, then the record having the input parametre text in the Name of the record is returned.</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="id">ID of the record to retrieve.</param>
        /// <param name="name">Name of the record to retrieve.</param>
        /// <returns>One record matching by <see cref="int"/> ID or <see cref="string"/> Name.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when no record could be returned using either parametre.</exception>
        Data.Product GetOneProduct(int id, string name = "");
    }
}
