namespace Awards.Repository
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Implements those basic  operations that are general (do not depend on data types),
    /// leaves the rest to the descendants.
    /// </summary>
    /// <typeparam name="T">A generic type (limited to classes).</typeparam>
    public abstract class RepositoryClass<T> : IRepository<T>
        where T : class
    {
        private readonly DbContext db;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryClass{T}"/> class.
        /// </summary>
        /// <param name="db">A <see cref="DbContext"/> type instance.</param>
        protected RepositoryClass(DbContext db)
        {
            this.db = db ?? throw new System.ArgumentNullException(nameof(db));
        }

        /// <inheritdoc/>
        public IQueryable<T> GetAll()
        {
            return this.db.Set<T>();
        }

        /// <inheritdoc/>
        public abstract T GetOne(int id);

        /// <inheritdoc/>
        public abstract T GetOne(string name);

        /// <inheritdoc/>
        public void Insert(T entity)
        {
            this.db.Set<T>().Add(entity);
            this.db.SaveChanges();
        }

        /// <inheritdoc/>
        public void Remove(T entity)
        {
            this.db.Set<T>().Remove(entity);
            this.db.SaveChanges();
        }

        /// <summary>
        /// Deletes the <typeparamref name="T"/> record with the given ID.
        /// </summary>
        /// <param name="id">The ID of the <typeparamref name="T"/> record to be deleted.</param>
        public abstract void Remove(int id);

        /// <inheritdoc/>
        public void Update(T entity)
        {
            this.db.Set<T>().Update(entity);
            this.db.SaveChanges();
        }
    }
}
