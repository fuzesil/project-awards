namespace Awards.Logic
{
    /// <summary>
    /// Custom type that holds one <see cref="Data.ExpertGroup"/> and one <see cref="Data.Product"/> member.
    /// </summary>
    public class ExpertgroupProduct
    {
        /// <summary>
        /// Gets or Sets the <see cref="Data.ExpertGroup"/> type member of this complex type.
        /// </summary>
        public Data.ExpertGroup ExpertGroup { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="Data.Product"/> type member of this complex type.
        /// </summary>
        public Data.Product Product { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is ExpertgroupProduct other)
            {
                return this.ExpertGroup.Equals(other.ExpertGroup)
                    && this.Product.Equals(other.Product);
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.ExpertGroup.GetHashCode() + this.Product.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $" -- {nameof(this.ExpertGroup)} = {this.ExpertGroup}"
                + $"\n {nameof(this.Product)} = {this.Product}"
                + "\n";
        }
    }
}
