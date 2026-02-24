namespace Awards.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Awards.Repository;

    /// <summary>
    /// Contains functions that work with tables: Members, Countries, ExpertGroups.
    /// </summary>
    public abstract class InternalLogic : IReadInternalData, IReadCommonData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalLogic"/> class.
        /// </summary>
        /// <param name="topN">The value of 'N' in "the top N of ..." queries.</param>
        /// <param name="members">An object for the Member repo.</param>
        /// <param name="countries">An object for the Country repo.</param>
        /// <param name="expertgroups">An object for the ExpertGroup repo.</param>
        protected InternalLogic(int topN, IMemberRepository members, ICountryRepository countries, IExpertGroupRepository expertgroups)
        {
            this.TopN = topN;
            this.Members = members;
            this.Countries = countries;
            this.Expertgroups = expertgroups;
        }

        /// <summary>
        /// Gets or Sets the value of 'N'.
        /// </summary>
        public int TopN { get; set; }

        /// <summary>
        /// Gets the Member repository instance.
        /// </summary>
        public IMemberRepository Members { get; private set; }

        /// <summary>
        /// Gets the Country repository instance.
        /// </summary>
        public ICountryRepository Countries { get; private set; }

        /// <summary>
        /// Gets the ExpertGroup repository instance.
        /// </summary>
        public IExpertGroupRepository Expertgroups { get; private set; }

        /// <summary>
        /// Returns the average value of the PPP/C column in the 'Countries' table.
        /// </summary>
        /// <returns>Average of PPP/C column in 'Countries' table.</returns>
        public double GetAveragePPP()
        {
            return this.Countries.GetAll().Average(item => item.PPPperCapita);
        }

        /// <inheritdoc/>
        public IEnumerable<Data.Country> ListAllCountries(out int count)
        {
            IQueryable<Data.Country> output = this.Countries.GetAll();
            count = output.Count();
            return output.ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<Data.ExpertGroup> ListAllExpertgroups(out int count)
        {
            IQueryable<Data.ExpertGroup> output = this.Expertgroups.GetAll();
            count = output.Count();
            return output.ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<Data.Member> ListAllMembers(out int count)
        {
            IQueryable<Data.Member> output = this.Members.GetAll();
            count = output.Count();
            return output.ToList();
        }

        /// <inheritdoc/>
        public Data.Country GetOneCountry(int id, string name = "")
        {
            if (id > 0)
            {
                return this.Countries.GetOne(id);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                return this.Countries.GetOne(name);
            }

            return null;
        }

        /// <inheritdoc/>
        public Data.ExpertGroup GetOneExpertGroup(int id, string name = "")
        {
            if (id > 0)
            {
                return this.Expertgroups.GetOne(id);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                return this.Expertgroups.GetOne(name);
            }

            return null;
        }

        /// <inheritdoc/>
        public Data.Member GetOneMember(int id, string name = "")
        {
            if (id > 0)
            {
                return this.Members.GetOne(id);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                return this.Members.GetOne(name);
            }

            return null;
        }

        /// <summary>
        /// Returns the list of countries with values in the PPP/C column above the calculated average of that column.
        /// </summary>
        /// <param name="count">Number of elements in the returned list.</param>
        /// <returns>List of countries with PPP/C above average.</returns>
        public abstract IEnumerable<Data.Country> ListCountriesAboveAveragePPP(out int count);

        /// <summary>
        /// Returns a list of countries that are either IN (if <paramref name="isContained"/> = true) or OUT OF the capital city of their country.
        /// </summary>
        /// <param name="isContained">Chooses between the selection method.
        /// 'True' for seeking members IN capital cities. 'False' for the opposite.</param>
        /// <returns>List of members IN / OUT OF capital cities.</returns>
        public abstract IEnumerable<MemberCountry> ListMembersInCapitalCity(bool isContained);

        /// <summary>
        /// Asynchronous version of <see cref="ListMembersInCapitalCity(bool)"/>.
        /// </summary>
        /// <param name="isContained">Chooses between the selection method.
        /// 'True' for seeking members IN capital cities. 'False' for the opposite.</param>
        /// <returns>List of members IN / OUT OF capital cities.</returns>
        public abstract System.Threading.Tasks.Task<IEnumerable<MemberCountry>> ListMembersInCapitalCityAsync(bool isContained);

        /// <summary>
        /// Asynchronous version of <see cref="GetRichestMemberInExpertGroup"/>.
        /// </summary>
        /// <returns>Call to the non-async method.</returns>
        public abstract System.Threading.Tasks.Task<IEnumerable<ExpertgroupMemberCountry>> GetRichestMemberInExpertGroupAsync();

        /// <summary>
        /// Returns the list of members in the specified country.
        /// </summary>
        /// <param name="id">ID of the country of interest.</param>
        /// <param name="count">Number of elements in the returned list.</param>
        /// <returns>The sequence of member records that satisfy the country ID specified.</returns>
        public abstract IEnumerable<Data.Member> CountMembersInCountry(int id, out int count);

        /// <summary>
        /// Returns the sequence of <see cref="ExpertgroupMemberCountry"/> elements
        /// where the member is the richest one (by its nation's PPP) in a particular expert group.
        /// </summary>
        /// <returns>The sequence of ExpertGroup - Member - Country triples where the member is the richest one in the group.</returns>
        public abstract IEnumerable<ExpertgroupMemberCountry> GetRichestMemberInExpertGroup();
    }
}
