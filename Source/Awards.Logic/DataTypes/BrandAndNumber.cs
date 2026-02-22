namespace Awards.Logic
{
    /// <summary>
    /// A custom type that holds a <see cref="Data.Brand"/> and an associated <see cref="int"/> value, e.g. the result of an aggregate function.
    /// </summary>
    public class BrandAndNumber
    {
        /// <summary>
        /// Gets or Sets the <see cref="Data.Brand"/> object.
        /// </summary>
        public Data.Brand Brand { get; set; }

        /// <summary>
        /// Gets or Sets the numberic <see cref="int"/> value associated with the <see cref="Data.Brand"/> object.
        /// </summary>
        public int Number { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is BrandAndNumber other)
            {
                return this.Number == other.Number
                    && this.Brand.Equals(other.Brand);
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Brand.GetHashCode() + this.Number;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $" -- {nameof(this.Number)} = {this.Number} \n{nameof(this.Brand)} = {this.Brand}";
        }
    }
}
