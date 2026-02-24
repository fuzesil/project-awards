namespace Awards.Logic
{
    using System.Linq;
    using Awards.Repository;

    /// <summary>
    /// Extension class containing the necessary methods to list all tables.
    /// </summary>
    public static class OverviewLogic
    {
        /// <summary>
        /// Gets the entire table using a Repository instance.
        /// </summary>
        /// <param name="irepo">The table (repository) to be read.</param>
        /// <returns>The table of interest as a list.</returns>
        public static System.Collections.Generic.IEnumerable<string> TableToList(this IRepository irepo)
        {
            if (irepo == null)
            {
                throw new System.ArgumentNullException(nameof(irepo), " cannot be null.");
            }

            return irepo switch
            {
                BrandRepository brands => brands.GetAll().Select(item => item.ToString()).ToList(),
                CountryRepository countries => countries.GetAll().Select(item => item.ToString()).ToList(),
                ExpertGroupRepository expertgroups => expertgroups.GetAll().Select(item => item.ToString()).ToList(),
                MemberRepository members => members.GetAll().Select(item => item.ToString()).ToList(),
                ProductRepository products => products.GetAll().Select(item => item.ToString()).ToList(),
                _ => throw new System.ArgumentException(message: "Passed argument was invalid, no repository matches with ", nameof(irepo)),
            };
        }
    }
}
