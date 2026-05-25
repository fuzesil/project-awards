namespace Awards.Repository
{
    using System.Linq;
    using Awards.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Repository class for the <see cref="Member"/> entity.
    /// </summary>
    public class MemberRepository : RepositoryClass<Member>, IMemberRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberRepository"/> class.
        /// </summary>
        /// <param name="db">The <see cref="DbContext"/> parametre.</param>
        public MemberRepository(DbContext db)
            : base(db)
        {
        }

        /// <inheritdoc/>
        public void ChangeChiefEditor(int id, string newChiefEditor)
        {
            Member thisMember = this.GetOne(id);
            thisMember.ChiefEditor = newChiefEditor;
            this.Update(thisMember);
        }

        /// <inheritdoc/>
        public void ChangeName(int id, string newName)
        {
            Member thisMember = this.GetOne(id);
            thisMember.Name = newName;
            this.Update(thisMember);
        }

        /// <inheritdoc/>
        public override Member GetOne(int id)
        {
            return this.GetAll().First(member => member.MemberID == id);
        }

        /// <inheritdoc/>
        public override Member GetOne(string name)
        {
            return this.GetAll().First(member => member.Name.Contains(name));
        }

        /// <inheritdoc/>
        public override void Remove(int id)
        {
            this.Remove(this.GetOne(id));
        }

        /// <inheritdoc/>
        public void ChangeOfficeLocation(int id, string newOfficeLocation, int newCountryId = 0)
        {
            Member thisMember = this.GetOne(id);
            thisMember.OfficeLocation = newOfficeLocation;
            if (newCountryId > 0)
            {
                thisMember.CountryID = newCountryId;
            }

            this.Update(thisMember);
        }

        /// <inheritdoc/>
        public void ChangePhoneNumber(int id, string newPhoneNumber)
        {
            Member thisMember = this.GetOne(id);
            thisMember.PhoneNumber = newPhoneNumber;
            this.Update(thisMember);
        }

        /// <inheritdoc/>
        public void ChangePublisher(int id, string newPublisher)
        {
            Member thisMember = this.GetOne(id);
            thisMember.Publisher = newPublisher;
            this.Update(thisMember);
        }

        /// <inheritdoc/>
        public void ChangeWebsite(int id, string newWebsite)
        {
            Member thisMember = this.GetOne(id);
            thisMember.Website = newWebsite;
            this.Update(thisMember);
        }
    }
}
