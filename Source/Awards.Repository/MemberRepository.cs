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
        public void ChangeName(int id, string newName)
        {
            Member thisMember = this.GetMember(id);
            thisMember.Name = newName;
            this.Update(thisMember);
        }

        /// <inheritdoc/>
        public void Move(int id, int newCountryID, string newLocation)
        {
            Member thisMember = this.GetMember(id);
            if (id > 0)
            {
                thisMember.CountryID = newCountryID;
            }

            thisMember.OfficeLocation = newLocation;
            this.Update(thisMember);
        }

        /// <inheritdoc/>
        public override Member GetOne(int id)
        {
            return this.GetAll().Single(member => member.MemberID == id);
        }

        /// <inheritdoc/>
        public override Member GetOne(string name)
        {
            return this.GetAll().Where(member => member.Name.Contains(name)).First();
        }

        /// <inheritdoc/>
        public override void Remove(int id)
        {
            this.Remove(this.GetMember(id));
        }

        /// <summary>
        /// Returns 1 record or throws exception.
        /// </summary>
        /// <param name="id">The ID of the record to be returned.</param>
        /// <returns>The one record with the matching ID.</returns>
        private Member GetMember(int id)
        {
            Member thisMember;
            try
            {
                thisMember = this.GetAll().Single(member => member.MemberID == id);
            }
            catch (System.InvalidOperationException ex)
            {
                throw new System.ApplicationException($"No record with the given ID [{id}] found by {nameof(this.GetMember)}.", ex);
            }

            return thisMember;
        }
    }
}
