namespace Awards.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// Entity that represents the Expert Groups table.
    /// </summary>
    public class ExpertGroup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpertGroup"/> class.
        /// </summary>
        public ExpertGroup()
        {
            this.Members = new HashSet<Member>();
            this.Products = new HashSet<Product>();
        }

        /// <summary>
        /// Gets or Sets the primary key for the <see cref="ExpertGroup"/> entity.
        /// </summary>
        public int ExpertGroupID { get; set; }

        /// <summary>
        /// Gets or Sets the name field for the <see cref="ExpertGroup"/> entity.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the collection of <see cref="Member"/> instances.
        /// </summary>
        public virtual ICollection<Member> Members { get; }

        /// <summary>
        /// Gets the collection of <see cref="Product"/> instances.
        /// </summary>
        public virtual ICollection<Product> Products { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is ExpertGroup other)
            {
                return this.ExpertGroupID == other.ExpertGroupID
                    && this.Name == other.Name;
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.ExpertGroupID;
        }

        /// <summary>
        /// Returns a custom string of the properties of the current <see cref="ExpertGroup"/> object.
        /// </summary>
        /// <returns>The custom string that represents the current object.</returns>
        public override string ToString()
        {
            return "{ "
                + nameof(this.ExpertGroupID) + " = " + this.ExpertGroupID + " | "
                + nameof(this.Name) + " = " + this.Name
                + " }";
        }
    }
}
