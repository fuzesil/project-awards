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
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryRepository"/> class.
        /// </summary>
        /// <param name="db">The <see cref="DbContext"/> parametre.</param>
        public CountryRepository(DbContext db)
            : base(db)
        {
        }

        /// <inheritdoc/>
        public void ChangeName(int id, string newName)
        {
            Country thisCountry = this.GetOne(id);
            thisCountry.Name = newName;
            this.Update(thisCountry);
        }

        /// <inheritdoc/>
        public void ChangeCapitalCity(int id, string newCapitalCity)
        {
            Country thisCountry = this.GetOne(id);
            thisCountry.CapitalCity = newCapitalCity;
            this.Update(thisCountry);
        }

        /// <inheritdoc/>
        public void ChangePPP(int id, int newPPP)
        {
            Country thisCountry = this.GetOne(id);
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
            return this.GetAll().First(country => country.CountryID == id);
        }

        /// <inheritdoc/>
        public override Country GetOne(string name)
        {
            return this.GetAll().First(country => country.Name.Contains(name));
        }

        /// <inheritdoc/>
        public override void Remove(int id)
        {
            this.Remove(this.GetOne(id));
        }
    }
}