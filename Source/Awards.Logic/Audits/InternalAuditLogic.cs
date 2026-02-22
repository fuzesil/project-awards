namespace Awards.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Awards.Repository;

    /// <summary>
    /// Contains additional functions that work with tables: Members, Countries, ExpertGroups.
    /// </summary>
    public class InternalAuditLogic : InternalLogic
    {
        /* private static InternalAuditLogic singletonIAL; */

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalAuditLogic"/> class.
        /// </summary>
        /// <param name="topN">The value of 'N' in "the top N" queries.</param>
        /// <param name="members">The Memebrs tabel.</param>
        /// <param name="countries">The Countries table.</param>
        /// <param name="expertgroups">The ExpertGroups table.</param>
        public InternalAuditLogic(int topN, IMemberRepository members, ICountryRepository countries, IExpertGroupRepository expertgroups)
            : base(topN, members, countries, expertgroups)
        {
        }

        /*
        /// <summary>
        /// Sets the properties for <see cref="InternalAuditLogic"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Repository arguments mustn't be null.</exception>
        /// <exception cref="ArgumentException">Argument mustn't be less than 1.</exception>
        /// <param name="limiter">The value of 'N' in "the top N" queries.</param>
        /// <param name="memberRepository">The Memebrs tabel.</param>
        /// <param name="countryRepository">The Countries table.</param>
        /// <param name="expertGroupRepository">The ExpertGroups table.</param>
        /// <returns>An instance of this class.</returns>
        public static InternalAuditLogic Initialize(
            int limiter = 1,
            IMemberRepository memberRepository = null,
            ICountryRepository countryRepository = null,
            IExpertGroupRepository expertGroupRepository = null)
        {
            if (limiter < 1)
            {
                throw new ArgumentException("This cannot be less than 1", nameof(limiter));
            }

            if (singletonIAL == null)
            {
                singletonIAL = new InternalAuditLogic(limiter, memberRepository, countryRepository, expertGroupRepository);
            }

            return singletonIAL;
        }
        */

        /// <inheritdoc/>
        public override IEnumerable<Data.Country> ListCountriesAboveAveragePPP(out int count)
        {
            IQueryable<Data.Country> output = this.Countries.GetAll()
                .Where(country => country.PPPperCapita > this.GetAveragePPP())
                .OrderBy(country => country.PPPperCapita);
            count = output.Count();
            return output.ToList();
        }

        /// <inheritdoc/>
        public override IEnumerable<MemberCountry> ListMembersInCapitalCity(bool isContained)
        {
            if (isContained)
            {
                return this.Members.GetAll()
                   .Join(this.Countries.GetAll(), member => member.CountryID, country => country.CountryID, (member, country) => new { member, country })
                   .Where(jointype => jointype.member.OfficeLocation.Contains(jointype.country.CapitalCity))
                   .Select(jointype => new MemberCountry { Country = jointype.country, Member = jointype.member });
            }

            return this.Members.GetAll()
                   .Join(this.Countries.GetAll(), member => member.CountryID, country => country.CountryID, (member, country) => new { member, country })
                   .Where(jointype => !jointype.member.OfficeLocation.Contains(jointype.country.CapitalCity))
                   .Select(jointype => new MemberCountry { Country = jointype.country, Member = jointype.member })
                   .ToList();

            // .Where(member => member.OfficeLocation.Contains(member.Country.CapitalCity))
        }

        /// <inheritdoc/>
        public override Task<IEnumerable<MemberCountry>> ListMembersInCapitalCityAsync(bool isContained)
        {
            return Task.Run(() => this.ListMembersInCapitalCity(isContained));
        }

        /// <inheritdoc/>
        public override Task<IEnumerable<ExpertgroupMemberCountry>> GetRichestMemberInExpertGroupAsync()
        {
            return Task.Run(this.GetRichestMemberInExpertGroup);
        }

        /// <inheritdoc/>
        public override IEnumerable<Data.Member> CountMembersInCountry(int id, out int count)
        {
            var output = this.Members.GetAll().Where(member => member.CountryID == id);
            count = output.Count();
            return output;
        }

        /// <inheritdoc/>
        public override IEnumerable<ExpertgroupMemberCountry> GetRichestMemberInExpertGroup()
        {
            return this.Countries.GetAll()
                     .Join(this.Members.GetAll(), country => country.CountryID, member => member.CountryID, (country, member) => new { country, member })
                     .GroupBy(join => join.member.ExpertGroupID)
                     .Select(group => new { EGid = group.Key, MaxPPP = group.Max(item => item.country.PPPperCapita) })
                     /* [countries + members] we have the EGid and the richest country's PPP/C by now */
                     .Join(this.Countries.GetAll(), egID_maxPPP => egID_maxPPP.MaxPPP, country => country.PPPperCapita, (egID_maxPPP, country) => new { egID_maxPPP, country })
                     .Where(countryjoin => countryjoin.country.PPPperCapita == countryjoin.egID_maxPPP.MaxPPP)
                     /* [(egID + maxPPP) + countries] we have countries having the same PPP/C as the selected greatest from earlier, plus egID */
                     .Join(this.Members.GetAll(), countryjoin => countryjoin.country.CountryID, member => member.CountryID, (group, member) => new { group, member })
                     .Where(membergroup => membergroup.member.ExpertGroupID == membergroup.group.egID_maxPPP.EGid && membergroup.member.CountryID == membergroup.group.country.CountryID)
                     /* && membergroup.member.Country.PPPperCapita == membergroup.group.MaxPPP)*/
                     /* now we joined with members based on country (ID) and EG membership (ID) */
                     .Join(this.Expertgroups.GetAll(), memgroup => memgroup.member.ExpertGroupID, eg => eg.ExpertGroupID, (memgroup, eg) => new ExpertgroupMemberCountry { Expertgroup = eg, Country = memgroup.group.country, Member = memgroup.member })
                     .ToList();
            /*
             * IEnumerable<Data.Member> chosenMembers = this.Countries.GetAll()
                     .Join(this.Members.GetAll(), country => country.CountryID, member => member.CountryID, (country, member) => new { country, member })
                     .GroupBy(join => join.member.ExpertGroupID)
                     .Select(group => new { EGid = group.Key, MaxPPP = group.Max(item => item.country.PPPperCapita) })
                     .Join(this.Members.GetAll(), group => group.EGid, member => member.ExpertGroupID, (group, member) => new { group, member })
                     .Where(trijoin => trijoin.member.ExpertGroupID == trijoin.group.EGid && trijoin.member.Country.PPPperCapita == trijoin.group.MaxPPP)
                     .Select(trijoin => trijoin.member);

            return this.Countries.GetAll()
                 .Join(this.Members.GetAll(), country => country.CountryID, member => member.CountryID, (country, member) => new { country, member })
                 .Join(this.Expertgroups.GetAll(), join => join.member.ExpertGroupID, eg => eg.ExpertGroupID, (join, eg) => new { join, eg })
                 .Where(result => chosenMembers.Contains(result.join.member))
                 .Select(outtype => new ExpertgroupMemberCountry { Expertgroup = outtype.eg, Member = outtype.join.member, Country = outtype.join.country })
                 .ToList();
             */
        }

        /*
        public int CountMembersInCapitalCity(bool isContained)
        {
            return this.Members.GetAll()
                .Where(member => isContained
                ? member.OfficeLocation.Contains(member.Country.CapitalCity)
                : !member.OfficeLocation.Contains(member.Country.CapitalCity))
                .Count();

            // members.GetAll().Count() - GetMembersInCapitalCity().Count();
        }
        */

        /*
                /// <summary>
                /// Returns the list of IDs and Counts of the top 'N' brands.
                /// </summary>
                /// <param name="count">The number of items in the returned ist.</param>
                /// <returns>List of IDs and Counts of select brands.</returns>
                public IEnumerable<NameAndValue> ListTopMemberRegions(out int count)
                {
                    IEnumerable<int> countTopMemberRegion = this.Members.GetAll()
                        .GroupBy(member => member.CountryID)
                        .Select(group => group.Count())
                        .Distinct()
                        .OrderByDescending(grpCnt => grpCnt)
                        .Take(this.TopN)
                        .ToList();

                    IEnumerable<NameAndValue> output = this.Members.GetAll()
                        .GroupBy(member => member.CountryID)
                        .Select(group => new { brandID = group.Key, Count = group.Count() })
                        .Where(group => countTopMemberRegion.Contains(group.Count))
                        .Join(this.Countries.GetAll(), group => group.brandID, country => country.CountryID, (group, country) => new { country.Name, group.Count })
                        .Select(inp => new NameAndValue { Name = inp.Name, Value = inp.Count })
                        .OrderByDescending(inp => inp.Value)
                        .ToList();

                    count = output.Count();
                    return output;
                }
        */
    }
}
