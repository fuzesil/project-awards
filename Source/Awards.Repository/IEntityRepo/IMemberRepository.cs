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
        /// Updates the <see cref="Member.Website"/> field (i.e. a new website can be specified).
        /// </summary>
        /// <param name="id">The ID [<see cref="Member.MemberID"/>] of the <see cref="Member"/> record to be updated.</param>
        /// <param name="newWebsite">The new value of <see cref="Member.Website"/> (i.e. new website for the chosen record).</param>
        void ChangeWebsite(int id, string newWebsite);

        /// <summary>
        /// Updates the <see cref="Member.ChiefEditor"/> field (i.e. a new Chief Editor can be specified).
        /// </summary>
        /// <param name="id">The ID [<see cref="Member.MemberID"/>] of the <see cref="Member"/> record to be updated.</param>
        /// <param name="newChiefEditor">The new value of <see cref="Member.ChiefEditor"/> (i.e. new Chief Editor for the chosen record).</param>
        void ChangeChiefEditor(int id, string newChiefEditor);

        /// <summary>
        /// Updates the <see cref="Member.PhoneNumber"/> field (i.e. a new phone number can be specified).
        /// </summary>
        /// <param name="id">The ID [<see cref="Member.MemberID"/>] of the <see cref="Member"/> record to be updated.</param>
        /// <param name="newPhoneNumber">The new value of <see cref="Member.PhoneNumber"/> (i.e. new phone number for the chosen record).</param>
        void ChangePhoneNumber(int id, string newPhoneNumber);

        /// <summary>
        /// Updates the <see cref="Member.Publisher"/> field (i.e. a new publisher can be specified).
        /// </summary>
        /// <param name="id">The ID [<see cref="Member.MemberID"/>] of the <see cref="Member"/> record to be updated.</param>
        /// <param name="newPublisher">The new value of <see cref="Member.Publisher"/> (i.e. new publisher for the chosen record).</param>
        void ChangePublisher(int id, string newPublisher);

        /// <summary>
        /// Updates the <see cref="Member.OfficeLocation"/> field (i.e. a new office location (address) can be specified).
        /// The country of the office location can also be updated by specifying the new country ID.
        /// </summary>
        /// <param name="id">The ID [<see cref="Member.MemberID"/>] of the <see cref="Member"/> record to be updated.</param>
        /// <param name="newOfficeLocation">The new value of <see cref="Member.OfficeLocation"/> (i.e. new office location for the chosen record).</param>
        /// <param name="newCountryId">The ID of the country [<see cref="Country.CountryID"/>] for the chosen record.
        /// If &lt;1, it will be ignored.</param>
        void ChangeOfficeLocation(int id, string newOfficeLocation, int newCountryId = 0);
    }
}