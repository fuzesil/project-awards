namespace Awards.Logic
{
    /// <summary>
    /// A custom type that holds one <see cref="Data.ExpertGroup"/>, one <see cref="Data.Member"/>, and one <see cref="Data.Country"/> object. More complex operations fetch one of each to store all relevant information.
    /// </summary>
    public class ExpertgroupMemberCountry
    {
        /// <summary>
        /// Gets or Sets the <see cref="Data.Country"/> type member.
        /// </summary>
        public Data.Country Country { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="Data.ExpertGroup"/> type member.
        /// </summary>
        public Data.ExpertGroup Expertgroup { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="Data.Member"/> type member.
        /// </summary>
        public Data.Member Member { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is ExpertgroupMemberCountry other)
            {
                return this.Country.Equals(other.Country)
                    && this.Expertgroup.Equals(other.Expertgroup)
                    && this.Member.Equals(other.Member);
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Country.GetHashCode() + this.Expertgroup.GetHashCode() + this.Member.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $" -- {nameof(this.Expertgroup)} = {this.Expertgroup}"
                + $"\n{nameof(this.Member)} = {this.Member}"
                + $"\n{nameof(this.Country)} = {this.Country}"
                + "\n";
        }
    }
}
