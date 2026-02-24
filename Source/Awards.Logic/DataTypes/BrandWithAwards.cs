namespace Awards.Logic
{
    using System.Linq;

    /// <summary>
    /// A custom type that stores a <see cref="Data.Brand"/> entity type, <see cref="int"/> Count of awards,
    /// and <see cref="System.Collections.Generic.IEnumerable{T}"/> (T = <see cref="string"/>) Awarded product list.
    /// </summary>
    public class BrandWithAwards
    {
        /// <summary>
        /// Gets or Sets the name of a groupped Brand.
        /// </summary>
        public Data.Brand Brand { get; set; }

        /// <summary>
        /// Gets or Sets the Count of awards won by a groupped Brand.
        /// </summary>
        public int AwardCount { get; set; }

        /// <summary>
        /// Gets or Sets the sequence of awarded Products from the groupped Brand.
        /// </summary>
        public System.Collections.Generic.IEnumerable<Data.Product> WinningProducts { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is BrandWithAwards other))
            {
                return false;
            }

            bool isProductCollectionEqual = false;
            var thisProdList = this.WinningProducts?.ToList() ?? new System.Collections.Generic.List<Data.Product>();
            var otherProdList = other.WinningProducts?.ToList() ?? new System.Collections.Generic.List<Data.Product>();

            if (thisProdList.Count == otherProdList.Count)
            {
                for (int i = 0; i < thisProdList.Count; i++)
                {
                    if (!(isProductCollectionEqual = thisProdList[i].Equals(otherProdList[i])))
                    {
                        break;
                    }
                }
            }

            return isProductCollectionEqual
                && this.AwardCount == other.AwardCount
                && this.Brand.Equals(other.Brand);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.AwardCount + this.Brand.GetHashCode();
        }

        /// <summary>
        /// Returns a custom <see cref="string"/> that represents the current item.
        /// </summary>
        /// <returns>The <see cref="string"/> representation of the current item.</returns>
        public override string ToString()
        {
            string output = $" -- {this.AwardCount} awards won by \n{this.Brand}\n - with products:";
            foreach (var product in this.WinningProducts)
            {
                output += $"\n{product}";
            }

            return output + "\n";
        }
    }
}
