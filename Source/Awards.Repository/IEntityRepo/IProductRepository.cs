namespace Awards.Repository
{
    using Awards.Data;

    /// <summary>
    /// Declares operations applicable only to the Products table.
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Updates the price of the only Product record with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Product to be updated.</param>
        /// <param name="newprice">The new price for the chosen Product record.</param>
        void ChangePrice(int id, int newprice);
    }
}