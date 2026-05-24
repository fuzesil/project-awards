namespace Awards.Repository
{
    using System.Linq;
    using Awards.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Repository class for the <see cref="ExpertGroup"/> entity.
    /// </summary>
    public class ExpertGroupRepository : RepositoryClass<ExpertGroup>, IExpertGroupRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpertGroupRepository"/> class.
        /// </summary>
        /// <param name="db">The <see cref="DbContext"/> parametre.</param>
        public ExpertGroupRepository(DbContext db)
            : base(db)
        {
        }

        /// <inheritdoc/>
        public void ChangeName(int id, string newName)
        {
            ExpertGroup expertGroup = this.GetExpertGroup(id);
            expertGroup.Name = newName;
            this.Update(expertGroup);
        }

        /// <inheritdoc/>
        public override ExpertGroup GetOne(int id)
        {
            return this.GetAll().Single(expertgroup => expertgroup.ExpertGroupID == id);
        }

        /// <inheritdoc/>
        public override ExpertGroup GetOne(string name)
        {
            return this.GetAll().Where(eg => eg.Name.Contains(name)).First();
        }

        /// <inheritdoc/>
        public override void Remove(int id)
        {
            this.Remove(this.GetOne(id));
        }

        /// <summary>
        /// Returns 1 record or throws exception.
        /// </summary>
        /// <param name="id">The ID of the record to be returned.</param>
        /// <returns>The one record with the matching ID.</returns>
        private ExpertGroup GetExpertGroup(int id)
        {
            ExpertGroup thisEG;
            try
            {
                thisEG = this.GetAll().Single(eg => eg.ExpertGroupID == id);
            }
            catch (System.InvalidOperationException ex)
            {
                throw new System.ApplicationException($"No record with the given ID [{id}] found by {nameof(this.GetExpertGroup)}.", ex);
            }

            return thisEG;
        }
    }
}
