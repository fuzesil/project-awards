namespace Awards.Repository
{
    using System.Linq;
    using Awards.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The data repository of <see cref="Country"/> entity.
    /// </summary>
    public class CountryRepository : RepositoryClass<Country>, ICountryRepository
    {
        private readonly DbContext db;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryRepository"/> class.
        /// </summary>
        /// <param name="db">The <see cref="DbContext"/> parametre.</param>
        public CountryRepository(DbContext db)
            : base(db)
        {
            this.db = db ?? throw new System.ArgumentNullException(nameof(db));
        }

        /// <inheritdoc/>
        public void ChangeName(int id, string newName)
        {
            Country thisCountry = this.GetCountry(id);
            thisCountry.Name = newName;
            this.Update(thisCountry);
        }

        /// <inheritdoc/>
        public void ChangeCapitalCity(int id, string newCapitalCity)
        {
            Country thisCountry = this.GetCountry(id);
            thisCountry.CapitalCity = newCapitalCity;
            this.Update(thisCountry);
        }

        /// <inheritdoc/>
        public void ChangePPP(int id, int newPPP)
        {
            Country thisCountry = this.GetCountry(id);
            thisCountry.PPPperCapita = newPPP;
            this.Update(thisCountry);
        }

        /// <inheritdoc/>
        public double GetAveragePPP()
        {
            return this.GetAll().Average(country => country.PPPperCapita);
        }

        /// <inheritdoc/>
        public override Country GetOne(int id)
        {
            return this.GetAll().Single(country => country.CountryID == id);
        }

        /// <inheritdoc/>
        public override Country GetOne(string name)
        {
            return this.GetAll().Where(country => country.Name.Contains(name)).First();
        }

        /// <inheritdoc/>
        public override void Remove(int id)
        {
            this.Remove(this.GetCountry(id));
        }

        /// <summary>
        /// Returns 1 record or throws exception.
        /// </summary>
        /// <param name="id">The ID of the record to be returned.</param>
        /// <returns>The one record with the matching ID.</returns>
        private Country GetCountry(int id)
        {
            Country country;
            try
            {
                country = this.GetAll().Single(country => country.CountryID == id);
            }
            catch (System.InvalidOperationException ex)
            {
                throw new System.ApplicationException($"No record with the given ID [{id}] found by {nameof(this.GetCountry)}.", ex);
            }

            return country;
        }
    }
}