namespace Awards.Data
{
    using System;

    /// <summary>
    /// Entity class representing the Products table.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or Sets the primary key for the <see cref="Product"/> entity.
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or Sets the name field for the <see cref="Product"/> entity.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the award category field for the <see cref="Product"/> entity.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or Sets the price field for the <see cref="Product"/> entity.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or Sets the date of launch field for the <see cref="Product"/> entity.
        /// </summary>
        public DateTime LaunchDate { get; set; }

        /// <summary>
        /// Gets or Sets the estimated lifetime field for the <see cref="Product"/> entity.
        /// </summary>
        public int EstimatedLifetime { get; set; }

        /// <summary>
        /// Gets or Sets the foreign key pointing to the <see cref="Data.Country"/> entity.
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// Gets or Sets the foreign key pointing to the <see cref="Data.ExpertGroup"/> entity.
        /// </summary>
        public int ExpertGroupID { get; set; }

        /// <summary>
        /// Gets the navigation property to the <see cref="Data.Country"/> entity.
        /// </summary>
        public virtual Brand Brand { get; }

        /// <summary>
        /// Gets the navigation property to the <see cref="Data.ExpertGroup"/> entity.
        /// </summary>
        public virtual ExpertGroup ExpertGroup { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is Product other)
            {
                return this.ProductID == other.ProductID
                    && this.BrandId == other.BrandId
                    && this.ExpertGroupID == other.ExpertGroupID
                    && this.Price == other.Price
                    && this.EstimatedLifetime == other.EstimatedLifetime
                    && this.LaunchDate == other.LaunchDate
                    && this.Name == this.Name;
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.ProductID;
        }

        /// <summary>
        /// Returns a custom string of the properties of the current <see cref="Product"/> object.
        /// </summary>
        /// <returns>The custom string that represents the current object.</returns>
        public override string ToString()
        {
            return "{ "
                + nameof(this.ProductID) + " = " + this.ProductID + " | "
                + nameof(this.ExpertGroup) + " = " + this.ExpertGroup?.Name + " | "
                + nameof(this.Category) + " = " + this.Category + " | "
                + nameof(this.Name) + " = " + $"{this.Brand?.Name} {this.Name}" + " | "
                + nameof(this.Price) + " = " +
                this.Price.ToString("C", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + " | "
                + nameof(this.EstimatedLifetime) + " = " + this.EstimatedLifetime
                + " }";
        }
    }
}
