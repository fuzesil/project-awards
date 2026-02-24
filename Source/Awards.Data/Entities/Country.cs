namespace Awards.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// The entity class representing the Country table.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Country"/> class.
        /// </summary>
        public Country()
        {
            this.Brands = new HashSet<Brand>();
            this.Members = new HashSet<Member>();
        }

        /// <summary>
        /// Gets or Sets the primary key for the Country table.
        /// </summary>
        public int CountryID { get; set; }

        /// <summary>
        /// Gets or Sets the Name field for the Country table.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the capital city field for the Country table.
        /// </summary>
        public string CapitalCity { get; set; }

        /// <summary>
        /// Gets or Sets the calling code of a country.
        /// </summary>
        public int CallingCode { get; set; }

        /// <summary>
        /// Gets or Sets the PPP per person for the country.
        /// </summary>
        public int PPPperCapita { get; set; }

        /// <summary>
        /// Gets the generic collection type navigational property for the Manufacturer entity.
        /// </summary>
        public virtual ICollection<Brand> Brands { get; }

        /// <summary>
        /// Gets the generic collection type navigational property for the Member entity.
        /// </summary>
        public virtual ICollection<Member> Members { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is Country other)
            {
                return this.CountryID == other.CountryID
                    && this.CallingCode == other.CallingCode
                    && this.PPPperCapita == other.PPPperCapita
                    && this.Name == other.Name
                    && this.CapitalCity == other.CapitalCity;
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.CountryID;
        }

        /// <summary>
        /// Returns a custom string of the properties of the current <see cref="Country"/> object.
        /// </summary>
        /// <returns>The custom string that represents the current object.</returns>
        public override string ToString()
        {
            return "{ "
                + nameof(this.CountryID) + " = " + this.CountryID + " | "
                + nameof(this.Name) + " = " + this.Name + " | "
                + nameof(this.CapitalCity) + " = " + this.CapitalCity + " | "
                + nameof(this.CallingCode) + " = " + this.CallingCode + " | "
                + nameof(this.PPPperCapita) + " = "
                + this.PPPperCapita.ToString("C", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                + " }";
        }
    }
}
