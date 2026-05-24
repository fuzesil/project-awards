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
            ExpertGroup expertGroup = this.GetOne(id);
            expertGroup.Name = newName;
            this.Update(expertGroup);
        }

        /// <inheritdoc/>
        public override ExpertGroup GetOne(int id)
        {
            return this.GetAll().First(expertgroup => expertgroup.ExpertGroupID == id);
        }

        /// <inheritdoc/>
        public override ExpertGroup GetOne(string name)
        {
            return this.GetAll().First(eg => eg.Name.Contains(name));
        }

        /// <inheritdoc/>
        public override void Remove(int id)
        {
            this.Remove(this.GetOne(id));
        }
    }
}
