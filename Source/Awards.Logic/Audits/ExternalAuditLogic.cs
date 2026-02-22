namespace Awards.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Awards.Repository;

    /// <summary>
    /// Contains additional functions that work with tables: Brands, Products, ExpertGroups.
    /// </summary>
    public class ExternalAuditLogic : ExternalLogic
    {
        /* private static ExternalAuditLogic singletonEAL; */

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalAuditLogic"/> class.
        /// </summary>
        /// <param name="topN">The value of 'N' in "the top N" queries.</param>
        /// <param name="brands">The Brands table.</param>
        /// <param name="products">The Products table.</param>
        /// <param name="expertgroups">The ExpertGroups table.</param>
        public ExternalAuditLogic(int topN, IBrandRepository brands, IProductRepository products, IExpertGroupRepository expertgroups)
            : base(topN, brands, products, expertgroups)
        {
        }

        /*
        /// <summary>
        /// Creates an instance of <see cref="ExternalAuditLogic"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Repository arguments mustn't be null.</exception>
        /// <exception cref="ArgumentException">Argument mustn't be less than 1.</exception>
        /// <param name="limiter">The value of 'N' in "the top N" queries.</param>
        /// <param name="brandRepository">The Brands table.</param>
        /// <param name="productRepository">The Products table.</param>
        /// <param name="expertGroupRepository">The ExpertGroups table.</param>
        /// <returns>An instance of this class.</returns>
        public static ExternalAuditLogic Initialize(
            int limiter,
            IBrandRepository brandRepository,
            IProductRepository productRepository,
            IExpertGroupRepository expertGroupRepository)
        {
            if (limiter < 1)
            {
                throw new ArgumentException("This cannot be less than 1", nameof(limiter));
            }

            if (singletonEAL == null)
            {
                singletonEAL = new ExternalAuditLogic(limiter, brandRepository, productRepository, expertGroupRepository);
            }

            return singletonEAL;
        }
        */

        /// <inheritdoc/>
        public override IEnumerable<BrandAndNumber> ListTopBrands()
        {
            return this.GetBrandsAndAwardCounts()
                .OrderByDescending(brand_count => brand_count.Number)
                .ToList();
        }

        /// <inheritdoc/>
        public override Task<IEnumerable<BrandAndNumber>> ListTopBrandsAsync()
        {
            return Task.Run(this.ListTopBrands);
        }

        /// <inheritdoc/>
        public override IEnumerable<BrandWithAwards> GetProductsByBrandId(out int count)
        {
            List<BrandWithAwards> output = new List<BrandWithAwards>();
            foreach (BrandAndNumber brand_count in this.GetBrandsAndAwardCounts())
            {
                output.Add(new BrandWithAwards
                {
                    AwardCount = brand_count.Number,
                    Brand = brand_count.Brand,
                    WinningProducts = this.Products.GetAll().Where(product => product.BrandId == brand_count.Brand.BrandId),
                });
            }

            count = output.Count;
            return output;
        }

        /// <inheritdoc/>
        public override IEnumerable<ExpertgroupProduct> GetMaxPriceProdInEveryEG()
        {
            return this.Products.GetAll()
                .GroupBy(product => product.ExpertGroupID)
                .Select(grp => new { ExpertGroupID = grp.Key, MaxPrice = grp.Max(product => product.Price) })
                .Join(this.Products.GetAll(), group => group.MaxPrice, prod => prod.Price, (group, prod) => new { group, prod })
                .Join(this.Expertgroups.GetAll(), jointype => jointype.group.ExpertGroupID, eg => eg.ExpertGroupID, (jointype, eg) => new ExpertgroupProduct { ExpertGroup = eg, Product = jointype.prod })
                .ToList();
        }

        /// <inheritdoc/>
        public override Task<IEnumerable<ExpertgroupProduct>> GetMaxPriceProdInEveryEGAsync()
        {
            return Task.Run(this.GetMaxPriceProdInEveryEG);
        }

        /// <summary>
        /// Returns a sequence of <see cref="BrandAndNumber"/> type elements; that is,
        /// a <see cref="Data.Brand"/> object and the corresponding <see cref="int"/> Count of awards won by that brand
        /// given that it is in the top 'N' (defined in <see cref="ExternalLogic.TopN"/>) brands.
        /// </summary>
        /// <returns>The ID (first item) and the Count (second item) of top 'N' brands.</returns>
        private IEnumerable<BrandAndNumber> GetBrandsAndAwardCounts()
        {
            IEnumerable<int> topBrandCounts = this.Products.GetAll()
                .GroupBy(prod => prod.BrandId)
                .Select(group => group.Count())
                .Distinct()
                .OrderByDescending(grpCnt => grpCnt)
                .Take(this.TopN)
                .ToList();
            return this.Products.GetAll()
                .GroupBy(prod => prod.BrandId)
                .Select(group => new { brandID = group.Key, Count = group.Count() })
                .Where(group => topBrandCounts.Contains(group.Count))
                /*.Select(grp => new Tuple<int, int>(grp.brandID, grp.Count))*/
                /*.Select(group => new NumberAndId { Id = group.brandID, Number = group.Count })*/
                .Join(this.Brands.GetAll(), group => group.brandID, brand => brand.BrandId, (group, brand) => new BrandAndNumber { Brand = brand, Number = group.Count })
                .ToList();
        }
    }
}
