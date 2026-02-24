namespace Awards.Logic
{
    /// <summary>
    /// A custom type that holds one <see cref="Data.Member"/> and one <see cref="Data.Country"/> object.
    /// </summary>
    public class MemberCountry
    {
        /// <summary>
        /// Gets or Sets the <see cref="Data.Member"/> type member.
        /// </summary>
        public Data.Member Member { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="Data.Country"/> type member.
        /// </summary>
        public Data.Country Country { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is MemberCountry other)
            {
                return this.Country.Equals(other.Country)
                    && this.Member.Equals(other.Member);
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Country.GetHashCode() + this.Member.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $" -- {nameof(this.Member)} = {this.Member}"
                + $"\n{nameof(this.Country)} = {this.Country}" +
                "\n";
        }
    }
}
