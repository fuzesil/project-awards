namespace Awards.Logic
{
    /// <summary>
    /// A custom type that holds one <see cref="Data.Brand"/> and one <see cref="Data.Member"/> object.
    /// </summary>
    public class MemberBrand
    {
        /// <summary>
        /// Gets or Sets the <see cref="Data.Brand"/> member.
        /// </summary>
        public Data.Brand Brand { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="Data.Brand"/> member.
        /// </summary>
        public Data.Member Member { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is MemberBrand other)
            {
                return this.Brand.Equals(other.Brand)
                    && this.Member.Equals(other.Member);
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Brand.GetHashCode() + this.Member.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $" >> {nameof(this.Brand)} = {this.Brand} \n{nameof(this.Member)} = {this.Member}\n";
        }
    }
}
