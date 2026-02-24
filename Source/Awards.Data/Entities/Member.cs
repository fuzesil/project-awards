namespace Awards.Data
{
    using System;

    /// <summary>
    /// Entity class representing the Members table.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Gets or Sets the primary key for the <see cref="Member"/> entity.
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// Gets or Sets the name field for the <see cref="Member"/> entity.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the website field for the <see cref="Member"/> entity.
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Gets or Sets the Editor-in-Chief field for the <see cref="Member"/> entity.
        /// </summary>
        public string ChiefEditor { get; set; }

        /// <summary>
        /// Gets or Sets the phone number field for the <see cref="Member"/> entity.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or Sets the office location field for the <see cref="Member"/> entity.
        /// </summary>
        public string OfficeLocation { get; set; }

        /// <summary>
        /// Gets or Sets the publisher field for the <see cref="Member"/> entity.
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// Gets or Sets the foreign key pointing to the <see cref="Data.ExpertGroup"/> entity.
        /// </summary>
        public int ExpertGroupID { get; set; }

        /// <summary>
        /// Gets or Sets the foreign key pointing to the <see cref="Data.Country"/> entity.
        /// </summary>
        public int CountryID { get; set; }

        /// <summary>
        /// Gets the navigation property to the <see cref="Data.ExpertGroup"/> entity.
        /// </summary>
        public virtual ExpertGroup ExpertGroup { get; }

        /// <summary>
        /// Gets the navigation property to the <see cref="Data.Country"/> entity.
        /// </summary>
        public virtual Country Country { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is Member other)
            {
                return this.MemberID == other.MemberID
                    && this.CountryID == other.CountryID
                    && this.ExpertGroupID == other.ExpertGroupID
                    && this.PhoneNumber == other.PhoneNumber
                    && this.Name == other.Name
                    && this.ChiefEditor == other.ChiefEditor
                    && this.Publisher == other.Publisher
                    && this.Website == other.Website;
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.MemberID;
        }

        /// <summary>
        /// Returns a custom string of the properties of the current <see cref="Member"/> object.
        /// </summary>
        /// <returns>The custom string that represents the current object.</returns>
        public override string ToString()
        {
            return "{ "
                + nameof(this.MemberID) + " = " + this.MemberID + " | "
                + nameof(this.ExpertGroup) + " = "
                + (string.IsNullOrWhiteSpace(this.ExpertGroup.Name) ? "NO EXPERTGROUP" : this.ExpertGroup.Name) + " | "
                + nameof(this.Name) + " = " + this.Name + " | "
                + nameof(this.Publisher) + " = " +
                (string.IsNullOrWhiteSpace(this.Publisher) ? "NO DATA" : this.Publisher) + " | "
                + $"{nameof(this.PhoneNumber)} = +{this.Country?.CallingCode} "
                + $"{(string.IsNullOrWhiteSpace(this.PhoneNumber) ? "NO PHONE NUM." : this.PhoneNumber)}, | "
                + nameof(this.OfficeLocation) + " =  "
                + $" {(string.IsNullOrWhiteSpace(this.OfficeLocation) ? "NO OFFICE LOC." : this.OfficeLocation)},"
                + $" {(string.IsNullOrWhiteSpace(this.Country.Name) ? "NO COUNTRY" : this.Country.Name)}"
                + " }";
        }
    }
}
